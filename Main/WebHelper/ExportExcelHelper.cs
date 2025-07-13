using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Main.WebHelper
{
    public class ExportExcelHelper
    {

        public string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }
    }
}