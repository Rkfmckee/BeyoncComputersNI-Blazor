using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects.Build;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace BeyondComputersNi.Services.Services;

public class BuildService(IRepository<Build> buildRepository, IConfiguration configuration, IMapper mapper) : IBuildService
{
    public Task<bool> BuildExistsAsync(int id)
    {
        return buildRepository.Get().AnyAsync(b => b.Id == id);
    }

    public Task<string?> GetBuildNumberAsync(int id)
    {
        return buildRepository.Get().Where(b => b.Id == id).Select(b => b.BuildNumber).SingleOrDefaultAsync();
    }

    public async Task<int?> CreateBuildAsync()
    {
        var identifier = await GetIdentifier();

        var build = new Build
        {
            BuildNumber = CurrentBuildNumber.Replace("identifier", $"{identifier}"),
        };

        await buildRepository.AddAsync(build);
        await buildRepository.SaveChangesAsync();

        return build.Id;
    }

    public async Task<IEnumerable<string>> GetComponentsAsync(string componentName, string? value = null)
    {
        // Get values from db eventually

        var components = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            components.Add($"{componentName} {i}");
        }

        return string.IsNullOrWhiteSpace(value) ?
            components :
            components.Where(c => c.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<bool> AddComponentsAsync(BuildComponentsDto buildComponents)
    {
        var build = await buildRepository.Get().SingleOrDefaultAsync(b => b.Id == buildComponents.Id);
        if (build == null) return false;

        mapper.Map(buildComponents, build);
        await buildRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPeripheralsAsync(BuildPeripheralsDto buildPeripherals)
    {
        var build = await buildRepository.Get().SingleOrDefaultAsync(b => b.Id == buildPeripherals.Id);
        if (build == null) return false;

        mapper.Map(buildPeripherals, build);
        await buildRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> FinishBuildAsync(BuildFinishDto buildFinish)
    {
        var build = await buildRepository.Get().SingleOrDefaultAsync(b => b.Id == buildFinish.Id);
        if (build == null) return false;

        mapper.Map(buildFinish, build);
        await buildRepository.SaveChangesAsync();

        return true;
    }

    #region Helpers

    private const string regexCapture = "(.*)";
    private string CurrentDay => $"{DateTime.UtcNow.Day.ToString():dd}";
    private string CurrentMonth => $"{DateTime.UtcNow.Month.ToString():MM}";
    private string CurrentYear => $"{DateTime.UtcNow.Year.ToString():yyyy}";

    private string BuildNumberFormat =>
        configuration["Config:BuildNumberFormat"] ??
        throw new InvalidOperationException("BuildNumberFormat not configured");

    private string CurrentBuildNumber =>
        BuildNumberFormat
        .Replace("year", CurrentYear)
        .Replace("month", CurrentMonth)
        .Replace("day", CurrentDay);

    private string BuildNumberRegex =>
        $"^{BuildNumberFormat}$"
        .Replace("year", regexCapture)
        .Replace("month", regexCapture)
        .Replace("day", regexCapture)
        .Replace("identifier", regexCapture);

    private async Task<int> GetIdentifier()
    {
        var defaultIdentifier = 1;
        var mostRecentBuildNumber = await buildRepository.Get().OrderByDescending(b => b.CreatedDate).Select(b => b.BuildNumber).FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(mostRecentBuildNumber)) return defaultIdentifier;

        var match = Regex.Match(mostRecentBuildNumber, BuildNumberRegex);
        if (!match.Success) return defaultIdentifier;

        if (!match.Groups[1].Value.Equals(CurrentYear) ||
            !match.Groups[2].Value.Equals(CurrentMonth) ||
            !match.Groups[3].Value.Equals(CurrentDay))
            return defaultIdentifier;

        var previousIdentifier = match.Groups[4].Value;
        if (string.IsNullOrEmpty(previousIdentifier)) return defaultIdentifier;

        return int.Parse(previousIdentifier) + 1;
    }

    #endregion
}
