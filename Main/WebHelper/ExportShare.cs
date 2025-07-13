using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.WebHelper
{
    public class ExportShare
    {
        public void ExportToExcelHelper(string fileName, GridView grv)
        {
            fileName = fileName.EndsWith(".xlsx") ? fileName : fileName + ".Xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                var workSheet = wb.Worksheets.Add("Sheet3");

                List<int> hiddenCol = new List<int> { 0 };
                // lấy ra tên file điền vào ô 1 để làm tiêu đề 
                workSheet.Cell(1, 1).Value = Path.GetFileNameWithoutExtension(fileName);
                // tính toán số cột bỏ và số cột ẩn 
                int VisibleCol = grv.Columns.Count - hiddenCol.Count;

                // gộp ô tiêu đề  ngang bằng số cột hiển thị và định dạng cho ô tiêu đề 

                workSheet.Range(1, 1, 1, VisibleCol).Merge().Style.Font.SetBold().Font.SetFontSize(16).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                int colIndex = 1;
                for (int i = 0; i < grv.Columns.Count; i++)
                {
                    if (!hiddenCol.Contains(i)) // chỉ hiener thị cột không ẩn 
                    {
                        workSheet.Cell(2, colIndex).Value = grv.Columns[i].HeaderText;// ghi tiêu đề cột 
                        workSheet.Cell(2, colIndex).Style.Font.SetBold().Font.SetFontSize(20).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        colIndex++;
                    }
                }
                int rowIndex = 3;
                foreach (GridViewRow row in grv.Rows)
                {
                    colIndex = 1;
                    for (int i = 0; i < grv.Columns.Count; i++)
                    {
                        if (!hiddenCol.Contains(i))
                        {
                            TableCell cell = row.Cells[i];
                            var ddl = cell.Controls.OfType<DropDownList>().FirstOrDefault();

                            foreach (System.Web.UI.Control control in cell.Controls)
                            {
                                if (control is Label lbl)
                                {
                                    cell.Text = lbl.Text;
                                }
                            }

                            if (ddl != null)
                            {
                                workSheet.Cell(rowIndex, colIndex).Value = ddl.SelectedItem.Text;
                            }
                            else
                            {


                                var img = cell.Controls.OfType<Image>().FirstOrDefault();
                                workSheet.Cell(rowIndex, colIndex).Value = img != null ? (img.Visible ? "true" : "false") : HttpUtility.HtmlDecode(cell.Text);
                            }
                            colIndex++;
                        }
                    }
                    rowIndex++;
                }
                workSheet.Columns().AdjustToContents();

                using (MemoryStream mr = new MemoryStream())
                {
                    wb.SaveAs(mr);
                    byte[] byteArr = mr.ToArray();

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename ={fileName}");
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.BinaryWrite(byteArr);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }

            }

        }

        public void ExportToExcelFromSelectedRows(string fileName, GridView grv)
        {
            fileName = fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ? fileName : fileName + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                var workSheet = wb.Worksheets.Add("Sheet1");

                List<int> hiddenCol = new List<int> { 0 }; // ví dụ: cột 0 là checkbox, không export
                workSheet.Cell(1, 1).Value = Path.GetFileNameWithoutExtension(fileName);

                int visibleCol = grv.Columns.Count - hiddenCol.Count;
                workSheet.Range(1, 1, 1, visibleCol)
                         .Merge()
                         .Style.Font.SetBold()
                                    .Font.SetFontSize(16)
                                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Header
                int colIndex = 1;
                for (int i = 0; i < grv.Columns.Count; i++)
                {
                    if (!hiddenCol.Contains(i))
                    {
                        workSheet.Cell(2, colIndex).Value = grv.Columns[i].HeaderText;
                        workSheet.Cell(2, colIndex).Style.Font.SetBold().Font.SetFontSize(12)
                                               .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        colIndex++;
                    }
                }

                int rowIndex = 3;
                foreach (GridViewRow row in grv.Rows)
                {
                    // Giả sử checkbox nằm ở cột 0
                    CheckBox chk = row.Cells[0].FindControl("chkExp") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        colIndex = 1;
                        for (int i = 0; i < grv.Columns.Count; i++)
                        {
                            if (!hiddenCol.Contains(i))
                            {
                                TableCell cell = row.Cells[i];
                                var ddl = cell.Controls.OfType<DropDownList>().FirstOrDefault();

                                foreach (System.Web.UI.Control control in cell.Controls)
                                {
                                    if (control is Label lbl)
                                        cell.Text = lbl.Text;
                                }

                                if (ddl != null)
                                {
                                    workSheet.Cell(rowIndex, colIndex).Value = ddl.SelectedItem.Text;
                                }
                                else
                                {
                                    var img = cell.Controls.OfType<Image>().FirstOrDefault();
                                    workSheet.Cell(rowIndex, colIndex).Value = img != null ? (img.Visible ? "true" : "false") : HttpUtility.HtmlDecode(cell.Text);
                                }
                                colIndex++;
                            }
                        }
                        rowIndex++;
                    }
                }

                workSheet.Columns().AdjustToContents();

                using (MemoryStream mr = new MemoryStream())
                {
                    wb.SaveAs(mr);
                    byte[] byteArr = mr.ToArray();

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.BinaryWrite(byteArr);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }



    }
}