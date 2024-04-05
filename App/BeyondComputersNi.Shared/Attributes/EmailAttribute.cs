using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.Attributes;
internal class EmailAttribute : RegularExpressionAttribute
{
    public EmailAttribute() : base(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$")
    {
        ErrorMessage = "Email address is not in a valid format. It should match user@example.com.";
    }
}
