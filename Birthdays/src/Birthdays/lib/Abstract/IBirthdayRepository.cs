namespace Birthdays.Abstract;

public interface IBirthdayRepository
{
    Task<Birthday[]> GetTodaysBirthdaysAsync();
}