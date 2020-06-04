using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YsProject.Utility
{
    public class EPPlusManager : IDisposable
    {
        private ExcelPackage _excel;

        public EPPlusManager(FileInfo file)
        {
            _excel = new ExcelPackage(file);
        }

        /// <summary>
        /// Add Sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet AddSheet(string sheetName)
        {
            return _excel.Workbook.Worksheets.Add(sheetName);
        }

        /// <summary>
        /// Get Sheet
        /// </summary>
        /// <returns></returns>
        public ExcelWorksheet GetSheet(string sheetName)
        {
            return _excel.Workbook.Worksheets[sheetName];
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _excel.Save();
        }

        /// <summary>
        /// Save As
        /// </summary>
        /// <param name="file"></param>
        public void SaveAs(FileInfo file)
        {
            _excel.SaveAs(file);
        }

        public void Dispose()
        {
            if(_excel!=null) _excel.Dispose();
        }

        private void Test()
        {
            ExcelWorksheet worksheet = _excel.Workbook.Worksheets[""];
            worksheet.Cells["B1"].Value = "111";
            worksheet.Cells["B1"].Style.Numberformat.Format = "#,##0";
            worksheet.Cells["A1:B3"].Style.Numberformat.Format = "#,##0";
            worksheet.Cells[1,1,3,2].Style.Numberformat.Format = "#,##0";
            worksheet.Cells["A:B"].Style.Numberformat.Format = "#,##0";
            worksheet.Cells["1:1,A:A,C3"].Style.Font.Bold = true;
            worksheet.Cells["A:XFD"].Style.Font.Name = "Arial";
            worksheet.Cells.Style.Font.Name = "Arial";

            var excelData = _excel.GetAsByteArray();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "MyWorkbook.xlsx";
            //var file =  File(excelData, contentType, fileName);


        }
    }
}
