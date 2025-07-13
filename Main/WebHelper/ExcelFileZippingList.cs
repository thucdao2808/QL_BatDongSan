using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.WebHelper
{
    public class ExcelFileZippingList
    {
        // khởi tạo để lưu trữ thông tin liên quan đến xử lí và quản lí tệp Excel , đặc biệt trong bối cảnh (zipping) hoặc tạo báo cáo 

        public string EXCEL_TEMP_OLD_PATH { get; set; } // đường dẫn tạm thời của tệp Excel cũ 
        public string EXCEL_TEMP_NEW_PATH { get; set; } // đường dẫn tạm thời của tệp Excel mới
        public string EXCEL_TEMP_LOCAL_SERVER_PATH { get; set; }// đường dẫn tạm thời trên máy chủ cho tệp Excel mới
        public string EXCEL_NAME_FILE_OUTPUT { get; set; }//Tên tệp đầu ra 

        public bool IS_HAVE_STT { get; set; } // biến kiểm tra xem có 1 trạng thái cụ thể nào không
        public string TEN_LOP { get; set; } // tên lớp
        public string TEN_BAO_CAO_IN_SO { get; set; } // tên báo cáo in sổ

        public List<ExcelSheetData> ListExcelSheetData { get; set; } // danh sách các đối tượng ExcelSheetData , mỗi đối tượng đại diện cho 1 sheet trong tệp Excel.Các sheet này có thể chứa thông tin như tên sheet , dữ liệu bảng (DataTable), danh sách cột ẩn , thông tin header và các thông tin khác liên quan đến việc định dạng và trình bày dữ liệu trong tệp Excel.
    }
}
