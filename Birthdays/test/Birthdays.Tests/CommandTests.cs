using Xunit;
using Moq;
using Birthdays.Abstract;

namespace Birthdays.Tests;

public class CommandTests
{

    [Fact]
    public void Simple() {
        Assert.True(true);
    }

    [Fact]
    public async Task SendBirthdayEmails_ShouldSendEmails()
    {   
        var mockRepository = new Mock<IBirthdayRepository>();
        mockRepository.Setup(r => r.GetTodaysBirthdaysAsync()).ReturnsAsync([new Birthday("Test", "User", new DateOnly(2000, 1, 1), "test@dev.null")]);
        var mockMessagingProvider = new Mock<IMessagingProvider>();

        var command = new SendGreetingsCommand(mockMessagingProvider.Object, mockRepository.Object);

        await command.Execute();

        mockMessagingProvider.Verify(m => m.SendMessageAsync("Happy Birthday Test!","test@dev.null"), Times.Once);
        mockRepository.Verify(r => r.GetTodaysBirthdaysAsync(), Times.Once);
    }
}