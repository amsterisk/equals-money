using Xunit;

namespace Birthdays.Tests;
public class BirthdayTest {
    [Fact]
    public void TestNonLeapBabiesAreCorrectlyIdentified () {
        
        var birthday = new Birthday("Test","Friend", new DateOnly(1990, 1, 1), "dev@null.com");

        Assert.False(birthday.LeapBaby);
    }

    [Fact]
    public void TestLeapBabiesAreCorrectlyIdentified () {
        
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 29), "dev@null.com");

        Assert.True(birthday.LeapBaby);
    }
    [Fact]
    public void ValidateMatch() {
        
        var birthday = new Birthday("Test","Friend", new DateOnly(1990, 1, 1), "dev@null.com");

        Assert.True(birthday.match(new DateOnly(1990, 1, 1)));
    }
    [Fact]
    public void ValidateMatchForLeapBabyInNonLeapYear() {
        
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 29), "dev@null.com");

        Assert.True(birthday.match(new DateOnly(2001, 2, 28)));
    }
    [Fact]
    public void ValidateMatchForLeapBabyInLeapYear() {
        
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 29), "dev@null.com");

        Assert.True(birthday.match(new DateOnly(2004, 2, 29)));
    }

    [Fact]
    public void GeneratesStandardGreeting() {
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 21), "dev@null.com");
        Assert.Equal("Happy Birthday Test!", birthday.Greeting);
    }

    [Fact]
    public void GeneratesSpecialGreetingForLeapBabies() {
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 29), "dev@null.com");
        Assert.Equal("Happy Birthday Test! You are a leap baby!", birthday.Greeting);
    }

    [Fact]
    public void GeneratesCustomGreeting() {
        var birthday = new Birthday("Test","Friend", new DateOnly(2000, 2, 29), "dev@null.com"){
            CustomGreeting = "This is a special one!"
        };
        Assert.Equal("This is a special one!", birthday.Greeting);
    }
}