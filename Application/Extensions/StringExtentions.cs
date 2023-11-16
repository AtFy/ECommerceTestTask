namespace Application.Extensions;

public static class StringExtensions
{
    public static (DateOnly dateStart, DateOnly dateFinish) GetDatesFromUserInput(this string input)
    {
        var dateStart = DateOnly.Parse(input.Split('|')[1].Split('-')[0]);
        var dateFinish = DateOnly.Parse(input.Split('|')[1].Split('-')[1]);

        if (dateStart >= dateFinish)
        {
            throw new ArgumentException();
        }

        return (dateStart, dateFinish);
    }
}