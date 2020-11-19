using System.Collections.Generic;

namespace myMLApp.DataModels
{
    /// <summary>
    /// This class contains the records for the bible version in abbreviated and full text.
    /// </summary>
    public class BibleVersionInfo
    {
        public int VersionID { get; set; }

        public string VersionFileName { get; set; }

        public string VersionAbbreviation { get; set; }

        public string VersionLanguage { get; set; }

        public string VersionName { get; set; }

        public string VersionDocumentation { get; set; }

        public string VersionDomain { get; set; }
    }

    internal enum BibleVersion
    {
        ASV,   // American Standard-ASV1901
        BBE,   // Bible in Basic English
        DARBY, // Darby English Bible
        KJV,   // King James Version
        WBT,   // Webster's Bible
        WEB,   // World English Bible
        YLT    // Young's Literal Translation
    }
}