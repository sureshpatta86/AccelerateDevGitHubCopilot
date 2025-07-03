using System.ComponentModel;
using System.Reflection;

namespace Library.ApplicationCore.Enums;

public static class EnumHelper
{
    // Dictionary mappings for each enum type - eliminates reflection overhead
    private static readonly Dictionary<LoanExtensionStatus, string> LoanExtensionDescriptions = new()
    {
        { LoanExtensionStatus.Success, "Book loan extension was successful." },
        { LoanExtensionStatus.LoanNotFound, "Loan not found." },
        { LoanExtensionStatus.LoanExpired, "Cannot extend book loan as it already has expired. Return the book instead." },
        { LoanExtensionStatus.MembershipExpired, "Cannot extend book loan due to expired patron's membership." },
        { LoanExtensionStatus.LoanReturned, "Cannot extend book loan as the book is already returned." },
        { LoanExtensionStatus.Error, "Cannot extend book loan due to an error." }
    };

    private static readonly Dictionary<LoanReturnStatus, string> LoanReturnDescriptions = new()
    {
        { LoanReturnStatus.Success, "Book was successfully returned." },
        { LoanReturnStatus.LoanNotFound, "Loan not found." },
        { LoanReturnStatus.AlreadyReturned, "Cannot return book as the book is already returned." },
        { LoanReturnStatus.Error, "Cannot return book due to an error." }
    };

    private static readonly Dictionary<MembershipRenewalStatus, string> MembershipRenewalDescriptions = new()
    {
        { MembershipRenewalStatus.Success, "Membership renewal was successful." },
        { MembershipRenewalStatus.PatronNotFound, "Patron not found." },
        { MembershipRenewalStatus.TooEarlyToRenew, "It is too early to renew the membership." },
        { MembershipRenewalStatus.LoanNotReturned, "Cannot renew membership due to an outstanding loan." },
        { MembershipRenewalStatus.Error, "Cannot renew membership due to an error." }
    };

    // Type-specific overloads for better performance and type safety
    public static string GetDescription(LoanExtensionStatus status)
    {
        return LoanExtensionDescriptions.TryGetValue(status, out string? description) 
            ? description 
            : status.ToString();
    }

    public static string GetDescription(LoanReturnStatus status)
    {
        return LoanReturnDescriptions.TryGetValue(status, out string? description) 
            ? description 
            : status.ToString();
    }

    public static string GetDescription(MembershipRenewalStatus status)
    {
        return MembershipRenewalDescriptions.TryGetValue(status, out string? description) 
            ? description 
            : status.ToString();
    }

    // Fallback method for other enum types using reflection (maintains backward compatibility)
    public static string GetDescription(Enum value)
    {
        if (value == null)
            return string.Empty;

        // Try type-specific methods first for better performance
        return value switch
        {
            LoanExtensionStatus les => GetDescription(les),
            LoanReturnStatus lrs => GetDescription(lrs),
            MembershipRenewalStatus mrs => GetDescription(mrs),
            _ => GetDescriptionUsingReflection(value)
        };
    }

    // Helper method using reflection for unsupported enum types
    private static string GetDescriptionUsingReflection(Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString())!;

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return value.ToString();
        }
    }
}