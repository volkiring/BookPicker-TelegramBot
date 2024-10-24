namespace BookPicker_TelegramBot
{
    public record class Filter(FilterType Type, string Value);

    public enum FilterType
    {
        Genre,
        Author
    }
}
