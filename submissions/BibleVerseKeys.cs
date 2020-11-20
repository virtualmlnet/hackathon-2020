using myMLApp.DataModels;
using System;
using System.Linq;

namespace myMLApp.DataMappings
{
    /// <summary>
    /// This class serves to keep a connection to the repository that contains the bible version keys and to return the version info back.
    /// </summary>
    internal class BibleVerseKeys
    {
        private readonly string pathToBookAbbreviations = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\key_abbreviations_english.csv";
        private ConnectiontoExcel _conxObject;

        public BibleVerseKeys() { _conxObject = new ConnectiontoExcel(pathToBookAbbreviations); }

        public static VersicleInfo GetVersicleInfo(string verse)
        {
            // Extract verse components: Book Name, Chapter and Versicle
            char[] separators = new char[3] { ' ', ',', '-' };
            var q = verse.Split(" ").Length;
            // Reduce q if we have a space (false positive) after a comma.
            if (verse.Split(", ").Length > 0) q--;

            var p = verse.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // Assign the value for the book abbreviation since this is the value we will be searching for.
            string bookAbbreviation = String.Empty;
            int chapter = 0, startvers = 0;
            int? endvers = null;
            if (q == 2)
            {
                bookAbbreviation = p[0] + ' ' + p[1];
                chapter = Convert.ToInt32(p[2]);
                startvers = Convert.ToInt32(p[3]);
                if (p.Length > 4) endvers = Convert.ToInt32(p[4]);
            }
            else if (q == 3)
            {
                bookAbbreviation = p[0] + ' ' + p[1] + ' ' + p[2];
                chapter = Convert.ToInt32(p[3]);
                startvers = Convert.ToInt32(p[4]);
                if (p.Length > 5) endvers = Convert.ToInt32(p[5]);
            }
            else if (q == 1)
            {
                bookAbbreviation = p[0];
                chapter = Convert.ToInt32(p[1]);
                startvers = Convert.ToInt32(p[2]);
                if (p.Length > 3) endvers = Convert.ToInt32(p[3]);
                else throw new Exception("Verse Format is Incorrect");
            }

            return new VersicleInfo(bookAbbreviation, chapter, startvers, endvers);
        }

        public BookAbbreviationInfo GetVerseInfo(string verse)
        {
            var a = GetVersicleInfo(verse);

            // Now extract the abbreviation data
            if (_conxObject == null)
                return null;
            else
            {
                var c = _conxObject.UrlConnexion.Worksheet<BookAbbreviationInfo>();
                foreach (BookAbbreviationInfo b in c.ToList())
                {
                    if (b.Abbreviation.ToUpper() == a.BookAbbreviation.ToUpper()) return b;
                }

                return null;
                //Query a worksheet with a header row  
                // FOR SOME STRANGE REASON THIS LINQ IS NOT WORKING FOR ME!
                //return (from a in c where a.VersionAbbreviation.ToUpper() == version.ToUpper() select a).SingleOrDefault();
            }
        }

        ~BibleVerseKeys() { _conxObject.UrlConnexion.Dispose(); }
    }
}
