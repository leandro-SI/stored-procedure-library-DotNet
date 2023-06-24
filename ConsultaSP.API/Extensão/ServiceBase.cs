using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Extensão
{
    public abstract class ServiceBase : IServiceBase, IDisposable
    {
        private readonly DbContext _context;

        public ServiceBase(DbContext context) => this._context = context;

        public DbContext DbContextService => this._context;

        public void Dispose() => this._context.Dispose();
    }
}
