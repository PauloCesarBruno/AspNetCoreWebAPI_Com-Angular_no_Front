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


         // CONSULTAS
         // =====================================================================================================================

         // Aluno(s) 
         Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        
         Task<PageList<Aluno>> GetAllAlunosByDisciplinaIdAsync(PageParams pageParams, int disciplinaId, bool includeProfessor = false);        
         
         Task <Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false);

        
         // =============================================================================================
         

         // Professor(es)
         Task<PageList<Professor>> GetAllProfessoresAsync(PageParams pageParams, bool includeAlunos = false);       
       
         Task <PageList<Professor>> GetAllProfessoresByDisciplinaIdAsync(int disciplinaId, bool includeAlunos = false);       
       
         Task <Professor> GetProfessorByIdAsync(int professorId, bool includeProfessor = false);        
    }
}