using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Infrastucture.DtoModels
{
    public class FilterRuleDtoModel
    {
        public int FilterType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public FilterRuleDtoModel(int filterType, string name, string value)
        {
            FilterType = filterType;
            Name = name;
            Value = value;
        }
    }
}
