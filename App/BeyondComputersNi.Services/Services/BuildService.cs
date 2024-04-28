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
    public Task<bool> BuildExists(int id)
    {
        return buildRepository.Get().AnyAsync(x => x.Id == id);
    }

    public async Task<int?> CreateBuild()
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

    public async Task<bool> AddComponents(BuildComponentsDto buildComponents)
    {
        var build = await buildRepository.Get().SingleOrDefaultAsync(b => b.Id == buildComponents.Id);
        if (build == null) return false;

        mapper.Map(buildComponents, build);
        await buildRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddPeripherals(BuildPeripheralsDto buildPeripherals)
    {
        var build = await buildRepository.Get().SingleOrDefaultAsync(b => b.Id == buildPeripherals.Id);
        if (build == null) return false;

        mapper.Map(buildPeripherals, build);
        await buildRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> FinishBuild(BuildFinishDto buildFinish)
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
