using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        // O Que for passado como parâmetro vai ser do Tipo(T) classe que vai ser trabalhado no Add,Update e Delete.
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();


         // CONSULTAS:
         
        //Aluno
         Aluno[] GetAllAlunos(bool includeProfessor = false);

         Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);

         Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

         // =============================================================================================

         // Professor
         Professor[] GetAllProfessores(bool includeAlunos = false);

         Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

         Professor GetProfessorById(int professorId, bool includeProfessor = false);
    }
}