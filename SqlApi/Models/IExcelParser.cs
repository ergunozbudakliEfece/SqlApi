using System.Collections.Generic;

namespace SqlApi.Models
{
    public interface IExcelParser
    {
        IEnumerable<T> Parser<T>(ExcelParserOptions options) where T:new();
    }
}
