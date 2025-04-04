public class Birthday
{

    public Birthday(string fistName, string surname, DateOnly dateOfBirth, string email)
    {
        FirstName = fistName;
        Surname = surname;
        DateOfBirth = dateOfBirth;
        Email = email;
        CustomGreeting = null!;
    }

    public string FirstName { get; set; }

    public string Surname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string CustomGreeting { get; set; }

    public string Greeting { get => this.GenerateGreeting(); }

    public bool LeapBaby { get => this.DateOfBirth.Month == 2 && this.DateOfBirth.Day == 29; }

    public bool match(DateOnly date)
    {
        if (LeapBaby && !DateTime.IsLeapYear(date.Year))
        {
            return date.Month == 2 && date.Day == 28;
        }
        return date.Month == DateOfBirth.Month && date.Day == DateOfBirth.Day;
    }

    private string GenerateGreeting()
    {
        if (CustomGreeting != null)
        {
            return CustomGreeting;
        }
        if (LeapBaby)
        {
            return $"Happy Birthday {FirstName}! You are a leap baby!";
        }
        return $"Happy Birthday {FirstName}!";

    }
}