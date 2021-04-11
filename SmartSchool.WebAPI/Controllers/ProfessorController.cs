using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
         private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

       [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        // api/Professor/byID
        [HttpGet("byId/{id}")] // QueryString: Ex.: http://localhost:5000/api/Professores/byId/3
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor(a) de Codigo " + id + " não foi encontrado !!!");

            return Ok(professor);
        }

        // api/Professor/ByName
        [HttpGet("ByName")] // QueryString: Ex.: http://localhost:5000/api/Professor/byName?nome=Marta&sobrenome=Kent
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(p =>
                 p.Nome.Contains(nome));
            if (professor == null) return BadRequest("Professor(a) de Nome " + nome + " não foi encontrado !!!");

            return Ok(professor);
        }

        // api/Professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        // api/Professor/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
             var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if(prof == null) BadRequest("Professor(a) não Encontrado !!!");

            _context.Update(professor);
            _context.SaveChanges();            
            return Ok(professor);
        }

         // api/Professor/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if(prof == null) BadRequest("Professor(a) não Encontrado !!!");

            _context.Update(professor);
            _context.SaveChanges();            
            return Ok(professor);
        }

         // api/Professor/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);

            if(professor == null) BadRequest("Professor(a) " + id + " não Encontrado !!!");

            _context.Remove(professor);
            _context.SaveChanges();           
            return Ok();
        }
    }
}