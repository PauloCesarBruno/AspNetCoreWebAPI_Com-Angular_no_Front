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
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        // api/Aluno/byID
        [HttpGet("byId/{id}")] // QueryString: Ex.: http://localhost:5000/api/Aluno/byId/3
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno(a) de Codigo " + id + " não foi encontrado !!!");

            return Ok(aluno);
        }

        // api/Aluno/ByName
        [HttpGet("ByName")] // QueryString: Ex.: http://localhost:5000/api/Aluno/byName?nome=Marta&sobrenome=Kent
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a =>
                 a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("Aluno(a) de Nome " + nome + " e sobrenome "  + sobrenome +  " não foi encontrado !!!");

            return Ok(aluno);
        }

        // api/Aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // api/Aluno/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
             var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if(aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _context.Update(aluno);
            _context.SaveChanges();            
            return Ok(aluno);
        }

         // api/Aluno/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if(alu == null) BadRequest("Aluno(a) não Encontrado !!!");

            _context.Update(aluno);
            _context.SaveChanges();            
            return Ok(aluno);
        }

        // api/Aluno/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);

            if(aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _context.Remove(aluno);
            _context.SaveChanges();           
            return Ok();
        }
    }
}