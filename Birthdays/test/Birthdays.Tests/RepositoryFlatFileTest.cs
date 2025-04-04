using System.IO.Enumeration;
using System.Threading.Tasks;
using Birthdays;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace Birthdays.Tests;

public class BirthdayRepositoryFlatFileTest : IDisposable
{
    private const string TestFilePath = "./TestData.csv";
    private const string BadFilePath = "./Bad.csv";

    public BirthdayRepositoryFlatFileTest()
    {
        // Create a test file with sample data
        using var writer = new StreamWriter(TestFilePath);
        writer.WriteLine("Buddy,A,2023-10-01,a@dev.null");
        writer.WriteLine("Buddy,B,2023-10-02,b@dev.null");
        writer.WriteLine("Buddy,C,2023-10-02,c@dev.null");
        writer.WriteLine("Brother,Me,2023-10-03,bro@dev.null,Happy Birthday Brother Dear!");

        using var badWriter = new StreamWriter(BadFilePath);
        badWriter.WriteLine("Invalid,Data");
    }

    public void Dispose()
    {
        File.Delete(TestFilePath);
        File.Delete(BadFilePath);
    }

    [Fact]
    public void CorrectlyLoadsTestData()
    {
        var repository = new BirthdayRepositoryFlatFile(TestFilePath);
    }

    [Fact]
    public async Task ConstructorFailsWithEmptyFileName()
    {        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(new BirthdayRepositoryFlatFile(String.Empty)));
    }

    [Fact]
    public async Task ConstructorFailsWithBadFileName()
    {        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => Task.FromResult(new BirthdayRepositoryFlatFile("nonsense")));
    }

    [Fact]
    public async Task GetTodaysBirthdaysAsyncReturnsCorrectBirthday()
    {
        var timeProvider = new FakeTimeProvider();
        var repository = new BirthdayRepositoryFlatFile(TestFilePath, ',', timeProvider);
        timeProvider.SetUtcNow(new DateTimeOffset(2023, 10, 1, 12, 0, 0, TimeSpan.Zero));
        var birthdays = await repository.GetTodaysBirthdaysAsync();

        // Assert
        Assert.NotNull(birthdays);
        Assert.Single(birthdays);
        Assert.Contains(birthdays, b => b.Surname == "A");
    }

    [Fact]
    public async Task GetTodaysBirthdaysAsyncReturnsCorrectBirthdays()
    {
        var timeProvider = new FakeTimeProvider();
        var repository = new BirthdayRepositoryFlatFile(TestFilePath, ',', timeProvider);
        timeProvider.SetUtcNow(new DateTimeOffset(2023, 10, 2, 12, 0, 0, TimeSpan.Zero));
        var birthdays = await repository.GetTodaysBirthdaysAsync();

        // Assert
        Assert.NotNull(birthdays);
        Assert.Equal(2, birthdays.Length);
        Assert.Contains(birthdays, b => b.Surname == "B");
        Assert.Contains(birthdays, b => b.Surname == "C");
    }

    [Fact]
    public async Task GetTodaysBirthdayAsyncReturnsCustomGreetingWhenSpecified()
    {
        var timeProvider = new FakeTimeProvider();
        var repository = new BirthdayRepositoryFlatFile(TestFilePath, ',', timeProvider);
        timeProvider.SetUtcNow(new DateTimeOffset(2023, 10, 3, 12, 0, 0, TimeSpan.Zero));
        var birthdays = await repository.GetTodaysBirthdaysAsync();

        // Assert
        Assert.NotNull(birthdays);
        Assert.Single(birthdays);
        Assert.Equal("Happy Birthday Brother Dear!", birthdays[0].CustomGreeting);
    }

    [Fact]
    public async Task ShouldIgnoreInvalidLinesInCsv()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(new BirthdayRepositoryFlatFile(BadFilePath)));
    }
}