using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        
        public IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        // api/Aluno
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Aluno/3
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno(a) de codigo " + id + " não foi encontrado !!!");

            return Ok(aluno);
        }
        
        // api/Aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);

            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }   
            return BadRequest("Falha ao Registrar aluno(a) !!!");        
        }

        // api/Aluno/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _repo.Update(aluno);
            
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }   
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");  
        }

        // api/Aluno/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);

            if (alu == null) BadRequest("Aluno(a) não Encontrado !!!");

            _repo.Update(aluno);
            
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }   
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        // api/Aluno/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id);

            if (alu == null) BadRequest("Aluno(a) não encontrado !!!");

            _repo.Delete(alu);
            
            if (_repo.SaveChanges())
            {
                return Ok("Aluno(a) deletado(a) com sucesso !!!");
            }   
            return BadRequest("Falha ao deletar o registro do aluno(a) !!!");
        }
    }
}