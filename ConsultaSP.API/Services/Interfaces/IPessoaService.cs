using ConsultaSP.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<List<Pessoa>> GetPessoas();
    }
}
