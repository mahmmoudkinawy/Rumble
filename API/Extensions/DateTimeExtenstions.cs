namespace API.Extensions;
public static class DateTimeExtenstions
{
    public static int CalculateAge(this DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddDays(-age)) age--;

        return age;
    }
}
