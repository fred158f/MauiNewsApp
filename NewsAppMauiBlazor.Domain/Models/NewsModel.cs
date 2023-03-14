using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Domain.Models
{
    public class NewsModel
    {
        public string Title { get; set; } = string.Empty;
        public string SourceLink { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }

    }
}
