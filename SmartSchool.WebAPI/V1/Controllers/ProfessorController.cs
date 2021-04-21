using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// Versão 01 do meu controlador de Professores
    /// </summary>
    [ApiController]    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper; // Feito a Injeção do AutoMapper
            _repo = repo;
        }

        /// <summary>
        /// Método responsável para retornar todos os Professores, da Versão 01.
        /// </summary>
        /// <returns></returns>
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpGet]
        public async Task <IActionResult> Get()
        {
             var professor = await _repo.GetAllProfessoresAsync(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        /// <summary>
        /// Método responsável por retornar apenas um(a) Professor(a)Dto, da Versão 01.
        /// </summary>
        /// <returns></returns>
        // ATENÇÃO (DEIXAR ESSE GETREGISTER POR ENQUANTO)
        //========================================================
        [HttpGet("getregister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }
        //========================================================

        /// <summary>
        ///  Método responsável por retornar apenas um Professor(a) por meio do Código Id, da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/Professor
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Professor/3
        public async Task <IActionResult> GetByIdAsync(int id)
        {
           // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository 
           var professor = await _repo.GetProfessorByIdAsync(id, false);
            if (professor == null) return BadRequest("Professor(a) de codigo " + id + " não foi encontrado !!!");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        /// <summary>
        /// Metodo responsável por novo Registro de um(a) Professor(a), da Versão 01.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // api/Professor
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPost]
        public async Task <IActionResult> Post(ProfessorRegistrarDto model)
        {

            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            
            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }   
            return BadRequest("Falha no Registro do professor(a) !!!"); 
        }

        /// <summary>
        /// Método Responsalvel por Alteração do Registro de algum(a) Professor(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // api/Professor/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPut("{id}")]
        public async Task <IActionResult> Put(int id, ProfessorRegistrarDto model)
        {
             var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

        /// <summary>
        ///  Método Responsalvel por Alteração (Parcial) do Registro de algum(a) Professor(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // api/Professor/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public async Task <IActionResult> Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }                      
            return BadRequest("Falha ao Atualizar Professor(a) !!!");
        }

        /// <summary>
        ///  MétodoResponsalvel por Exclusão de Registro de um Professor(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/Professor/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository
            var professor = _repo.GetProfessorById(id);

            if(professor == null) BadRequest("Professor(a) não encontrado !!!");

            _repo.Delete(professor);

            if (await _repo.SaveChangesAsync())
            {
                return Ok("Professor(a) deletado(a) com sucesso !!!");
            }                      
            return BadRequest("Falha ao Deletar Professor(a) !!!");
        }
    }
}