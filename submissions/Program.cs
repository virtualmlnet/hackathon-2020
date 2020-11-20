using myMLApp.DataMappings;
using myMLApp.DataModels;
using MyMLAppML.Model;
using System;
using System.Text.RegularExpressions;

namespace myMLApp
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter your Bible verse: ");

            // Add input data
            var input = new ModelInput();
            var bibleVerse = Console.ReadLine();
            string bibleVerseCheck;

            // Check Bible Verse for correct format and valid abbreviations
            var match = Regex.Match(bibleVerse, VersePatterns.bibleVersePattern);
            bibleVerseCheck = match.Success ? "Bible verse is valid" : "Bible verse is invalid";
            Console.Write(bibleVerseCheck);
            if (!match.Success) return;

            // Obtain the Bible Version from the user.
            var versionKeys = new BibleVersionKeys();
            Console.Write("\n\nEnter the bible version you are planning to use (ASV, BBE, DARBY, KJV, WBT WEB, YLT): ");
            var bibleVersion = Console.ReadLine();
            BibleVersionInfo? dbVersionInfo = versionKeys.GetVersionInfo(bibleVersion);
            if (dbVersionInfo == null)
            {
                Console.Write("Invalid Bible Version");
                return;
            }

            // Retrieve the bible verse and display it back to the console
            var verseKeys = new BibleVerseKeys();
            BookAbbreviationInfo dbVerseInfo = verseKeys.GetVerseInfo(bibleVerse);
            VersicleInfo dbVersicleInfo = BibleVerseKeys.GetVersicleInfo(bibleVerse);

            BibleText bText = new BibleText((BibleVersion)Enum.Parse(typeof(BibleVersion), bibleVersion, true),
                                            dbVerseInfo,
                                            dbVersicleInfo);
            string verseText = bText.GetBibleVerse();
            Console.WriteLine();

            // Perform analysis on the said Bible verse as to sentiment (for now)
            input.SentimentText = verseText;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            Console.WriteLine($"Text: {input.SentimentText}\nIs Toxic: {((result.Prediction == "1")?"true":"false")}");
        }
    }
}
