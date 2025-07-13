using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess.Entities
{
    public class ExcelEntity

    {
        public ExcelEntity() {
            Color = XLColor.Black;
            Align = XLAlignmentHorizontalValues.Left;
        }
        public string Name { get; set; }// tên của cột hiên thị trong excel
        public string Type { get; set; }// loại dữ liệu 
        public string NumberFormat { get; set; }// định dạng số hoặc ngày tháng 
        public bool? IsGroup { get; set; } // xác định đây có phải nhóm chính không
        public bool? IsGroupChild { get; set; }// xác đĩnh xem đây có phải cột con nằm trong nhóm nào đó hay không
        public XLColor Color { get; set; } // màu sắc của Ô hoặc chữ 
        public XLAlignmentHorizontalValues Align { get; set; } // căn lề của chữ trong ô
        public HorizontalAlignment AlignNPOI { get; set; } // căn lề khi sử dụng thư viện NPOI
        public bool ShrinkToFit { get; set; } // nếu đúng nội dung của ô sẽ được co lại để vừa với độ rộng của cột 
    }

    public class DataExcel
    {
        public DataExcel()
        {


        }
        public string Name { get; set; }
        public int rowStart { get; set; }// dòng bắt đầu 
        public int? indexSheet { get; set; }// số thứ tự 
        public string colStart { get; set; }// cột bắt đầu 
        public string colEnd { get; set; }// cột kết thúc 
        public DataTable dt { get; set; } // bảng dữ liệu
        public List<ExcelEntity> lstColumn { get; set; }// danh sách các cột trong bảng dữ liệu
        public bool have_stt { get; set; }// biến kiểm tra xem có 1 trạng thái cụ thể nào không cụ thể là có cột stt nào hay không


    }
    public class ExcelTMPEntity
    {

        public ExcelTMPEntity()
        {
            listCell = new List<ExcelHeaderEntity>();
            dt = new DataTable();
            lstHeader = new List<ExcelHeaderEntity>();
            lstColumn = new List<ExcelEntity>();
        }
        public int indexSheet { get; set; }
        public List<ExcelHeaderEntity> listCell { get; set; }
        public int rowHeaderStart { get; set; }
        public int rowStart { get; set; }
        public string colStart { get; set; }
        public string colEnd { get; set; }
        public DataTable dt { get; set; }
        public List<ExcelHeaderEntity> lstHeader { get; set; }
        public List<ExcelEntity> lstColumn { get; set; }
        public bool have_stt { get; set; }
    }

}
