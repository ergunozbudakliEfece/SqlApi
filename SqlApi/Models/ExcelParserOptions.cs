namespace SqlApi.Models
{
    public class ExcelParserOptions
    {
        public string FilePath { get;}
        public string WorkSheetName { get; set; }
        public int HeaderRow { get; set; }
        public ExcelParserOptions(string filePath) {
            FilePath = filePath;
        }
    }
}
