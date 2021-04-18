using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {        
        
        private readonly IRepository _repo;
         private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

       [HttpGet]
        public IActionResult Get()
        {
             var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        // ATENÇÃO (DEIXAR ESSE GETREGISTER POR ENQUANTO)
        //========================================================
         [HttpGet("getregister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }
        //========================================================

        // api/Professor
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Professor/3
        public IActionResult GetById(int id)
        {
           // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository 
           var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor(a) de codigo " + id + " não foi encontrado !!!");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }
        
        // api/Professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {

            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }   
            return BadRequest("Falha no Registro do professor(a) !!!"); 
        }

        // api/Professor/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
             var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

         // api/Professor/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

         // api/Professor/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository
            var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _repo.Delete(professor);

            if (_repo.SaveChanges())
            {
                return Ok("Professor(a) deletado(a) com sucesso !!!");
            }                      
            return BadRequest("Falha ao Deletar Professor(a) !!!");
        }
    }
}