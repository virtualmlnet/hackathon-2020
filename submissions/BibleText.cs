using myMLApp.DataModels;
using System;
using System.Linq;

namespace myMLApp.DataMappings
{
    internal class BibleText
    {
        private string _pathToExcelFile;
        private ConnectiontoExcel _conxObject;
        private BookAbbreviationInfo _book;
        private VersicleInfo _versicle;

        public BibleText(BibleVersion version, BookAbbreviationInfo book, VersicleInfo versicle)
        {
            _book = book;
            _versicle = versicle;
            switch (version)
            {
                case BibleVersion.ASV:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_asv.csv"; break; // American Standard-ASV1901
                case BibleVersion.BBE:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_bbe.csv"; break; // Bible in Basic English
                case BibleVersion.DARBY: _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_dby.csv"; break; // Darby English Bible
                case BibleVersion.KJV:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_kjv.csv"; break; // King James Version
                case BibleVersion.WBT:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_wbt.csv"; break; // Webster's Bible
                case BibleVersion.WEB:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_web.csv"; break; // World English Bible
                case BibleVersion.YLT:   _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_ylt.csv"; break; // Young's Literal Translation
                default: _pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\t_asv.csv"; break;  // By defaul is the American standard version
            }
            _conxObject = new ConnectiontoExcel(_pathToExcelFile);
        }

        public string GetBibleVerse()
        {
            if (_conxObject != null)
            {
                //Query a worksheet with a header row  
                IQueryable<BibleTextRecord> b = from a in _conxObject.UrlConnexion.Worksheet<BibleTextRecord>() where a.BookID == _book.BookID &&
                                                                                                                      a.Chapter == _versicle.BookChapter &&
                                                                                                                      a.Versicle == _versicle.StartVersicle
                                                select a;

                return b.First().Text;
            }
            else return null;
        }

        ~BibleText() { _conxObject.UrlConnexion.Dispose(); }
    }
}
