namespace myMLApp.DataMappings
{
    public static class VersePatterns
    {
        public const string bibleVersePattern = "\\d?\\s?\\w+\\s?\\d+,\\d+\\-?\\d?";
        public const string bibleVerseSplit = "( )(,)(-)";
    }
}
