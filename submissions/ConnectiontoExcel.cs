using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Text;

namespace myMLApp
{
    class ConnectiontoExcel
    {
        public string _pathExcelFile;
        public ExcelQueryFactory _urlConnexion;
        public ConnectiontoExcel(string path)
        {
            this._pathExcelFile = path;
            this._urlConnexion = new ExcelQueryFactory(_pathExcelFile);
        }
        public string PathExcelFile
        {
            get
            {
                return _pathExcelFile;
            }
        }
        public ExcelQueryFactory UrlConnexion
        {
            get
            {
                return _urlConnexion;
            }
        }
    }
}
