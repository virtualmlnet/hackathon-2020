using System;
using System.Collections.Generic;
using System.Text;

namespace myMLApp.DataModels
{
    public class BookAbbreviationInfo
    {
        public int BookAbbreviationID { get; set; }

        public string Abbreviation { get; set; }

        public int BookID { get; set; }

        public int MainBookAbbreviation { get; set; }

    }
}
