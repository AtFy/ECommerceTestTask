namespace Application.Extensions;

public static class StringExtensions
{
    public static (DateTime dateStart, DateTime dateFinish) GetDatesFromUserInput(this string input)
    {
        var dateStart = DateTime.Parse(input.Split('|')[1].Split('-')[0]);
        var dateFinish = DateTime.Parse(input.Split('|')[1].Split('-')[1]);

        if (dateStart >= dateFinish)
        {
            throw new ArgumentException();
        }

        return (dateStart, dateFinish);
    }
}