using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Extensão
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public List<T> Results { get; set; }

        public PageResult() => this.Results = new List<T>();
    }
}
