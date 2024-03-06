using System;

namespace SqlApi.Models
{
    public class ExcelAttribute:Attribute
    {
        public string Name { get; set; }
        public ExcelAttribute(string name)
        {
            Name = name;
        }
    }
}
