using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SqlApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private IExcelParser parser;
       public ExcelController() {
            parser=new ExcelParser();
        }
        [HttpGet]
        public IEnumerable GetAll()
        {
            var myDocumentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var myExcelPath = Path.Combine(myDocumentFolder, "personelbilgi.xlsx");
            var options = new ExcelParserOptions(myExcelPath)
            {
                HeaderRow = 4,
                WorkSheetName = "Sheet1"

            };
            var result = parser.Parser<ExcelPersonel>(options);
            return result.ToList();
        }

    }

    public class ExcelPersonel
    {
        [Excel("TC KİMLİK NO")]
        public string TCKN { get; set; }
        [Excel("ISIM")]
        public string NAME { get; set; }
        [Excel("SOYISIM")]
        public string SURNAME { get; set; }
        [Excel("İŞE GİRİŞ TARİHİ")]
        public string DATE { get; set; }
    }

    public class ExcelParser : IExcelParser
    {
        public IEnumerable<T> Parser<T>(ExcelParserOptions options) where T : new()
        {
            var fileInfo=new FileInfo(options.FilePath);
            using var package = new ExcelPackage(fileInfo);
            var list=new List<T>();
            var worksheet = package.Workbook.Worksheets.First(x => x.Name==options.WorkSheetName);
            var headers = worksheet.Cells.Where(x => x.Start.Row == options.HeaderRow).Select(x => new ColumnInfo{
                Name=x.Address,
                Column=x.Start.Column,
                Value=x.Value.ToString().Trim().TrimEnd().TrimStart()
            }).ToList();

            var cells=worksheet.Cells.ToList();
            var groupCells = cells.Where(x => x.Start.Row > options.HeaderRow).GroupBy(x => x.Start.Row, arg => arg, (row, columns) => new{
                Row=row,
                Items=columns.ToList()
            }).ToList();
            var rowOrder = options.HeaderRow + 1;
            foreach(var cell in groupCells)
            {
                var itemModel = new T();
                var properties = itemModel.GetType().GetProperties().Select(x => new
                {
                    PropertyInfo = x,
                    Attr = x.GetCustomAttributes(false).Select(y => (ExcelAttribute)y).FirstOrDefault()
                }).ToArray();
                foreach(var property in properties)
                {
                    var propName= property.Attr != null ? property.Attr.Name : property.PropertyInfo.Name;
                    var columnInfo = headers.FirstOrDefault(x => x.Value.ToString() == propName);
                    if(columnInfo== null) {

                        continue;
                    }
                    var value=cell.Items.FirstOrDefault(x=>x.Start.Column==columnInfo.Column && x.Start.Row==rowOrder);
                    if(value!=null && !string.IsNullOrWhiteSpace(value.Text))
                    {
                        property.PropertyInfo.SetValue(itemModel, Convert.ChangeType(value.Text,property.PropertyInfo.PropertyType),null);
                    }
                }
                rowOrder++;
                list.Add(itemModel);
            }
            return list;
        }
    }
}
