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
    public class AlunoController : ControllerBase
    {

        public IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper; // Feito a Injeção do AutoMapper
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);            

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        // ATENÇÃO (DEIXAR ESSE GETREGISTER POR ENQUANTO)
        //========================================================
         [HttpGet("getregister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }
        //========================================================

        // api/Aluno
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Aluno/3
        public IActionResult GetById(int id)
        {
            // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno(a) de codigo " + id + " não foi encontrado !!!");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        // api/Aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Falha ao Registrar aluno(a) !!!");
        }

        // api/Aluno/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        // api/Aluno/Id
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        // api/Aluno/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // OBVIAMENTE O DELETE NÃO PRECISA DE MAPEAMENTO (AUTO-MAPPER).
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null) BadRequest("Aluno(a) não encontrado !!!");

            _repo.Delete(aluno);

            if (_repo.SaveChanges())
            {
                return Ok("Aluno(a) deletado(a) com sucesso !!!");
            }
            return BadRequest("Falha ao deletar o registro do aluno(a) !!!");
        }
    }
}