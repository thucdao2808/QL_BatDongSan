using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Main.WebHelper
{
    public class ExcelSheetData
    {
        // định nghĩa thuộc tính 
        public string CopyFromSheetName { get; set; }// tên của sheet mã dữ liệu sẽ được sao chép từ đó 

        public string SHEETNAME { get; set; } // Tên của sheet hiện tại trong tệp Excel

        public DataTable dt { get; set; } // một dối tượng DataTable chứa dữ liệu sẽ được hiển thị trong sheet này

        public List<int> ListColumnRemoveIndex { get; set; } // danh sách các chit số cột cần ẩn trong sheet này 

        public List<ExcelEntity> listColumn { get; set; }// danh sách các cột trong sheet  được định nghĩ bởi đối tượng ExcelEntity


    }
}