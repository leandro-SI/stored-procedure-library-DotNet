using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Extensão
{
    public abstract class PageResultBase
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int FistRowOnPage => (this.CurrentPage - 1) * this.PageSize + 1;

        public int LastRowOnPage => Math.Min(this.CurrentPage * this.PageSize, this.RowCount);
    }
}
