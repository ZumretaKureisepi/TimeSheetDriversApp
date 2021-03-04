using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.Web.Helpers
{
    public static class FileExportHelper
    {
        public static SaveOptions GetSaveOptions(string format)
        {
            switch (format.ToUpper())
            {
                case "XLSX":
                    return SaveOptions.XlsxDefault;
                case "PDF":
                    return SaveOptions.PdfDefault;

                default:
                    throw new NotSupportedException();
            }
        }

    }
}
