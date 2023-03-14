using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Toolbox.Interfaces
{
    public interface IFilter<T>
    {
        bool Catch(T model);
    }
}
