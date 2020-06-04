using OfficeOpenXml;
using OfficeOpenXml.Table;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace YsProject.Utils
{
    /// <summary>
    /// Excel操作
    /// </summary>
    public class ExcelUtility : IDisposable
    {

        #region Common

        /// <summary>
        /// Excel
        /// </summary>
        private ExcelPackage _excel;

        /// <summary>
        /// Ctor
        /// </summary>
        public ExcelUtility(string excelName)
        {
            // License
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Excel
            _excel = new ExcelPackage(new FileInfo(excelName));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public ExcelUtility()
        {
            // License
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Excel
            _excel = new ExcelPackage();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_excel != null) _excel.Dispose();
        }

        #endregion

        /// <summary>
        /// Load DataTable Data
        /// </summary>
        /// <param name="data"></param>
        public void LoadDataTable(DataTable data)
            => this.GetSheet().Cells["A2"].LoadFromDataTable(data, false, TableStyles.Medium9);

        /// <summary>
        /// Load DataTable Data
        /// </summary>
        /// <param name="list"></param>
        public void LoadDataList<T>(List<T> list)
        {
            DataTable data = new DataTable();
            List<PropertyInfo> properties = typeof(T).GetProperties()
                .Where(x => x.GetCustomAttribute<DisplayAttribute>() != null).ToList()
                .OrderBy(x => x.GetCustomAttribute<DisplayAttribute>().Order).ToList();

            properties.ForEach(p => data.Columns.Add(p.Name));

            foreach (var item in list)
            {
                DataRow row = data.NewRow();
                int i = 0;
                properties.ForEach(p =>
                {
                    row[i++] = p.GetValue(item);
                });
                data.Rows.Add(row);
            }

            this.LoadDataTable(data);
        }

        /// <summary>
        /// Add Sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet AddSheet(string sheetName)
            => _excel.Workbook.Worksheets.Add(sheetName);

        /// <summary>
        /// Add Sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet GetSheet(int index = 0)
            => _excel.Workbook.Worksheets[index];

        /// <summary>
        /// Add Sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet GetSheet(string sheetName)
            => _excel.Workbook.Worksheets[sheetName];

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
            => _excel.Save();

        /// <summary>
        /// SaveAs
        /// </summary>
        /// <param name="file"></param>
        public void SaveAs(FileInfo file)
            => _excel.SaveAs(file);

        /// <summary>
        /// SaveAs
        /// </summary>
        /// <param name="file"></param>
        public void SaveAs(Stream file)
            => _excel.SaveAs(file);
    }

    public class PdfUtiliity
    {
        public static void ExcelToPdf(string file, string outPath)
        {
            using (Workbook workbook = new Workbook())
            {
                workbook.LoadFromFile(file);
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.SaveToPdf(outPath);
            }
        }

        public static void ExcelToPdf(Stream file, string outPath)
        {
            using (Workbook workbook = new Workbook())
            {
                workbook.LoadFromStream(file);
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.SaveToPdf(outPath);
            }
        }

        public static void ExcelToPdf(string file, Stream outPath)
        {
            using (Workbook workbook = new Workbook())
            {
                workbook.LoadFromFile(file);
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.SaveToPdfStream(outPath);
            }
        }

        public static void ExcelToPdf(Stream file, Stream outPath)
        {
            using (Workbook workbook = new Workbook())
            {
                workbook.LoadFromStream(file);
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.SaveToPdfStream(outPath);
            }
        }
    }
}