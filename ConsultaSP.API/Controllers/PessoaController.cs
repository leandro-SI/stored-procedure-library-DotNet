using ConsultaSP.API.Entities;
using ConsultaSP.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost]
        public async Task<ActionResult<List<Pessoa>>> Get()
        {
            var pessoas = await _pessoaService.GetPessoas();

            if (pessoas == null) return NoContent();

            return Ok(pessoas);

        }

    }
}
