using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Almeida",
                Telefone = "99463-8956"
            },
             new Aluno(){
                Id = 2,
                Nome = "Marta",
                Sobrenome = "Kent",
                Telefone = "72436-0522"
            },
             new Aluno(){
                Id = 3,
                Nome = "Laura",
                Sobrenome = "Maria",
                Telefone = "96532-2705"
            },
        };
        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // api/Aluno/byID
        [HttpGet("byId/{id}")] // QueryString: Ex.: http://localhost:5000/api/Aluno/byId?id=3
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno de Codigo " + id + " não foi encontrado !!!");

            return Ok(aluno);
        }

         // api/Aluno/ByName
        [HttpGet("ByName")] // QueryString: Ex.: http://localhost:5000/api/Aluno/byName?nome=Marta&sobrenome=Kent
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a =>
                 a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("Aluno de Nome " + nome + " e sobrenome "  + sobrenome +  " não foi encontrado !!!");

            return Ok(aluno);
        }

        // api/Aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {

            return Ok(aluno);
        }

        // api/Aluno/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
                        
            return Ok(aluno);
        }

         // api/Aluno/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, Aluno aluno)
        {
                        
            return Ok(aluno);
        }

        // api/Aluno/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
                        
            return Ok();
        }
    }
}