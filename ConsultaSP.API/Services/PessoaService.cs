using ConsultaSP.API.Context;
using ConsultaSP.API.Entities;
using ConsultaSP.API.Extensão;
using ConsultaSP.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Services
{
    public class PessoaService : ServiceBase, IPessoaService
    {
        private readonly TesteContext _context;

        public PessoaService(TesteContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> GetPessoas()
        {
            DbCommand _commandFaq = _context.LoadStoredProcedure("SP_GetPessoas")
               .WithParameterIn("@Nome", "Leandro");

               //.WithParameterOut("@CountRows", typeof(int), 0, 0, 0, (parameter) => { parameter.DbType = System.Data.DbType.Int32; });

            IList<Pessoa> pessoaList = await _commandFaq.ExecuteAsync<Pessoa>();

            return pessoaList.ToList();
        }
    }
}
