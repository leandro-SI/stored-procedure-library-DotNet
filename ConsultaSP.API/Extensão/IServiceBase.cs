using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Extensão
{
    public interface IServiceBase : IDisposable
    {
        DbContext DbContextService { get; }
    }
}
