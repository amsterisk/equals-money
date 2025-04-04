
using Birthdays.Abstract;

namespace Birthdays;

public class BirthdayRepositoryFlatFile : IBirthdayRepository
{
    private readonly TimeProvider _timeProvider;

    private readonly string _filePath;
    private readonly char _delimiter;

    private Birthday[] _data;

    public BirthdayRepositoryFlatFile(string filePath, char delimiter = ',', TimeProvider timeProvider = null!)
    {
        _timeProvider = timeProvider ?? TimeProvider.System;
        _delimiter = delimiter;
        _filePath = filePath;

        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");
        _data = File.ReadAllLines(_filePath)
            .Select(line => FromCsv(line, this._delimiter))
            .ToArray();
    }

    public Task<Birthday[]> GetTodaysBirthdaysAsync()
    {
        var today = _timeProvider.GetUtcNow().Date;
        var birthdays = _data
            .Where(b => b.match(DateOnly.FromDateTime(today)))
            .ToArray();

        return Task.FromResult(birthdays);
    }

    private static Birthday FromCsv(string csvLine, char delimiter)
    {
        var values = csvLine.Split(delimiter);
        if(values.Length < 4)
        {
            throw new ArgumentException("CSV line must contain at least 4 values.");
        }
        var birthday = new Birthday
        (
            values[0],
            values[1],
            DateOnly.Parse(values[2]),
            values[3]
        );
        // If we have specified a custom greeting then load it
        if(values.Length > 4)
        {
            birthday.CustomGreeting = values[4];
        }
        return birthday;
    }
}