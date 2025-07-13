using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess.Entities
{
    public class ExcelHeaderEntity
    {

        public ExcelHeaderEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string name { get; set; }
        public int colM { get; set; } // số luonjwgj cột mà tiêu đề chiếm 
        public int rowM { get; set; } // số lượng dòng mà tiêu đề chiếm 
        public int? parentIndex { get; set; }// chỉ số của phần tử cha , nếu có , dùng cho tiều đề phân cấp 
        public string colName { get; set; }
        public bool Bold { get; set; }
        public XLColor Color { get; set; }
        public List<ExcelHeaderEntity> lstChild { get; set; } // danh sách các tiêu đề con , dùng trong quan hệ phân tầng
        public int rowIndex { get; set; } // vị trí dòng bắt đầu
        public double? fontSize { get; set; }
        public double? width { get; set; }
        public double? height { get; set; }
        public XLAlignmentHorizontalValues? Align { get; set; }
        public HorizontalAlignment? AlignNPOI { get; set; }

        public int cellIndex { get; set; }// vị trí cột bắt đầu 
        public string cellText { get; set; }// nội dung của ô khi xuất ra
        public bool IS_BOLD { get; set; }  // có in đậm chữ hay không
        public bool IS_ITALIC { get; set; } //có in nghiêng hay không
        public short RowHeight { get; set; }
    }
}

