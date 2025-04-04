using Birthdays.Abstract;

public class SendGreetingsCommand
{
    private readonly IMessagingProvider _emailSender;
    private readonly IBirthdayRepository _birthdayRepository;

    public SendGreetingsCommand(IMessagingProvider emailSender, IBirthdayRepository birthdayRepository)
    {
        _emailSender = emailSender;
        _birthdayRepository = birthdayRepository;
    }

    public async Task Execute()
    {
        var birthdays = await _birthdayRepository.GetTodaysBirthdaysAsync();
        foreach (var birthday in birthdays)
        {
            await _emailSender.SendMessageAsync(birthday.Greeting, birthday.Email);
        }
    }
}