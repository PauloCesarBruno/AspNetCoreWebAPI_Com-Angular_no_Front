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
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

       [HttpGet]
        public IActionResult Get()
        {
             var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        // api/Professor
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Professores/3
        public IActionResult GetById(int id)
        {
           var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor(a) de codigo " + id + " não foi encontrado !!!");

            return Ok(prof);
        }
        
        // api/Professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }   
            return BadRequest("Falha no Registro do professor(a) !!!"); 
        }

        // api/Professor/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
             var prof = _repo.GetProfessorById(id, false);

            if(prof == null) BadRequest("Professor(a) não encontrado !!!");

            _repo.Update(professor);

            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

         // api/Professor/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, Professor professor)
        {
             var prof = _repo.GetProfessorById(id, false);

            if(prof == null) BadRequest("Professor(a) não encontrado !!!");

            _repo.Update(professor);

            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

         // api/Professor/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorById(id, false); // Aqui se eu colocar  (true) Vem tudo ...

            if(prof == null) BadRequest("Professor(a) não encontrado !!!");

            _repo.Delete(prof);

            if (_repo.SaveChanges())
            {
                return Ok(prof);
            }                      
            return BadRequest("Falha ao Deletar Professor(a) !!!");
        }
    }
}