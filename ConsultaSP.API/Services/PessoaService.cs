using ConsultaSP.API.Context;
using ConsultaSP.API.Entities;
using ConsultaSP.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly TesteContext _context;

        public PessoaService(TesteContext context)
        {
            _context = context;
        }

        public Task<List<Pessoa>> GetPessoas()
        {
            throw new NotImplementedException();
        }
    }
}
