using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        // O Que for passado como parâmetro vai ser do Tipo(T) classe que vai ser trabalhado no Add,Update e Delete.
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task <bool> SaveChangesAsync();


         // CONSULTAS COM OPÇÕES PARA OUTROS PROGRAMADORES ESCOLHEREM ENTRE ASSINCRONO OU NÃO:
         //________________________________________________________________________________________
         
        //Aluno (De Forma ASSINCRONA)
         Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);

        // Opção para Forma Sincrona
         Aluno[] GetAllAlunos(bool includeProfessor = false);

        //Aluno (De Forma ASSINCRONA)
         Task<PageList<Aluno>> GetAllAlunosByDisciplinaIdAsync(PageParams pageParams, int disciplinaId, bool includeProfessor = false);

        // Opção para Forma Sincrona
         Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);

         //Aluno (De Forma ASSINCRONA)
         Task <Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false);

        // Opção paramForma Sincrona
         Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

         // =============================================================================================

         // Professor (De Forma ASSINCRONA)
         Task<PageList<Professor>> GetAllProfessoresAsync(PageParams pageParams, bool includeAlunos = false);
        
        // Opção para Forma Sincrona
         Professor[] GetAllProfessores(bool includeAlunos = false);

        // Professor (De Forma ASSINCRONA)
         Task <PageList<Professor>> GetAllProfessoresByDisciplinaIdAsync(int disciplinaId, bool includeAlunos = false);

        // Opção para Forma Sincrona
         Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        // Professor (De Forma ASSINCRONA)
         Task <Professor> GetProfessorByIdAsync(int professorId, bool includeProfessor = false);

        // Opção para Forma Sincrona
        Professor GetProfessorById(int professorId, bool includeProfessor = false);
    }
}