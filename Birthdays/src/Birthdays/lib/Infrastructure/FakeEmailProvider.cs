
using Birthdays.Abstract;

namespace Birthdays;

public class FakeEmailProvider : IMessagingProvider {
    public Task SendMessageAsync(string message, string address)
    {
        Console.WriteLine($"Fake email sent to {address}: {message}");
        return Task.CompletedTask;
    }
}
