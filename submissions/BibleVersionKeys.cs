using myMLApp.DataModels;
using System.Linq;

namespace myMLApp.DataMappings
{
    /// <summary>
    /// This class serves to keep a connection to the repository that contains the bible version keys and to return the version info back.
    /// </summary>
    internal class BibleVersionKeys
    {
        private readonly string pathToExcelFile = @"C:\Users\anton\source\repos\myMLApp\myMLApp.Data\CSV\bible_version_key.csv";
        private ConnectiontoExcel _conxObject;

        public BibleVersionKeys() { _conxObject = new ConnectiontoExcel(pathToExcelFile); }

        public BibleVersionInfo? GetVersionInfo(string version)
        {
            if (_conxObject == null)
                return null;
            else
            {
                var c = _conxObject.UrlConnexion.Worksheet<BibleVersionInfo>();
                foreach (BibleVersionInfo b in c.ToList())
                {
                    if (b.VersionAbbreviation.ToUpper() == version.ToUpper()) return b;
                }

                return null;
                //Query a worksheet with a header row  
                // FOR SOME STRANGE REASON THIS LINQ IS NOT WORKING FOR ME!
                //return (from a in c where a.VersionAbbreviation.ToUpper() == version.ToUpper() select a).SingleOrDefault();
            }
        }

        ~BibleVersionKeys() { _conxObject.UrlConnexion.Dispose(); }
    }
}
