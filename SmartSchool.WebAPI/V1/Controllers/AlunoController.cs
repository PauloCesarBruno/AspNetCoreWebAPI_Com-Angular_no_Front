using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// Versão 01 do meu controlador de Alunos.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {

        public IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método responsável por retornar apenas um Aluno(a) por meio do Código Id, da Versão 01.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper; // Feito a Injeção do AutoMapper
            _repo = repo;
        }
        
        /// <summary>
        /// Método responsável para retornar todos os Alunos, da Versão 01.
        /// </summary>
        /// <returns></returns>
       // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpGet]        
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);  

            var alunoResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunoResult);
        }

        /// <summary>
        /// Método responsável por retornar apenas um(a) Aluno(a)Dto, da Versão 01.
        /// </summary>
        /// <returns></returns>
        // ATENÇÃO (DEIXAR ESSE GETREGISTER POR ENQUANTO)
        //========================================================
        [HttpGet("getregister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }
        //========================================================

        /// <summary>
        /// Método responsável por retonar apenas um único AlunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplinaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);

            return Ok(result);
        }

        /// <summary>
        ///  Método responsável por retornar apenas um Aluno(a) por meio do Código Id, da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/Aluno
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpGet("{id}")] // QueryString: Ex.: http://localhost:5000/api/Aluno/3
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // Aqui abaixo se eu colocar  (id, true) Vem tudo que esta em Join lá no Repository
            var aluno = await _repo.GetAlunoByIdAsync(id, false);
            if (aluno == null) return BadRequest("Aluno(a) de codigo " + id + " não foi encontrado !!!");

            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);

            return Ok(alunoDto);
        }        
        
        /// <summary>
        /// Metodo responsável por novo Registro de um(a) Aluno(a), da Versão 01.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // api/Aluno
         // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPost]
        public async Task<IActionResult> Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Falha ao Registrar aluno(a) !!!");
        }

        /// <summary>
        /// Método Responsalvel por Alteração do Registro de algum(a) Aluno(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // api/Aluno/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        /// <summary>
        ///  Método Responsalvel por Alteração (Parcial) do Registro de algum(a) Aluno(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        // api/Aluno/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpPatch("{id}")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public async Task<IActionResult> Patch(int id, AlunoPatchDto model)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
            }
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        // api/Aluno/Id{id}/trocarEstado
        [HttpPatch("{id}/trocarEstado")] // [HttpPatch("{id}")] -> Atualiza Parcialmente
        public async Task<IActionResult> trocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno == null) BadRequest("Aluno(a) não Encontrado !!!");

            aluno.Ativo = trocaEstado.Estado;

            _repo.Update(aluno);
            if (await _repo.SaveChangesAsync())
            {
                var msn = aluno.Ativo ? "Ativado" : "Desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso !"});
            }
            return BadRequest("Falha ao atualizar o  registro do aluno(a) !!!");
        }

        /// <summary>
        ///  MétodoResponsalvel por Exclusão de Registro de um(a) Aluno(a), da Versão 01.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/Aluno/Id
        // ESTE MÉTODO É ASSINCRONO PARA GANHO DE PERFORMANCE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // OBVIAMENTE O DELETE NÃO PRECISA DE MAPEAMENTO (AUTO-MAPPER).
            var aluno = await _repo.GetAlunoByIdAsync(id);

            if (aluno == null) BadRequest("Aluno(a) não encontrado !!!");

            _repo.Delete(aluno);

            if (await _repo.SaveChangesAsync())
            {
                return Ok("Aluno(a) deletado(a) com sucesso !!!");
            }
            return BadRequest("Falha ao deletar o registro do aluno(a) !!!");
        }
    }
}