namespace Birthdays.Abstract;

public interface IMessagingProvider
{
    Task SendMessageAsync(string message, string address);
}