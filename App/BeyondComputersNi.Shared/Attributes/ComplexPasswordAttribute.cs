using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.Attributes;

public class ComplexPasswordAttribute : ValidationAttribute
{
    private List<string> allValidationConstraints = new()
    {
        "8 characters",
        "one lowercase character",
        "one uppercase character",
        "one number",
        "one symbol (@#$%&*+ etc.)"
    };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;

        if (string.IsNullOrEmpty(password)) return base.IsValid(value, validationContext);

        var validationConstraints = new List<string>(allValidationConstraints);

        if (password.Length >= 8)
        {
            validationConstraints.RemoveAll(x => x.Contains("8 characters"));
        }

        if (password.Any(char.IsLower))
        {
            validationConstraints.RemoveAll(x => x.Contains("lowercase"));
        }

        if (password.Any(char.IsUpper))
        {
            validationConstraints.RemoveAll(x => x.Contains("uppercase"));
        }

        if (password.Any(char.IsNumber))
        {
            validationConstraints.RemoveAll(x => x.Contains("number"));
        }

        if (password.Any(c => !char.IsLetterOrDigit(c)))
        {
            validationConstraints.RemoveAll(x => x.Contains("symbol"));
        }

        if (validationConstraints.Count == 0) return ValidationResult.Success;

        var errorMessage = $"Password must also have at least; {string.Join(", ", validationConstraints)}.";
        return new ValidationResult(errorMessage);
    }
}
