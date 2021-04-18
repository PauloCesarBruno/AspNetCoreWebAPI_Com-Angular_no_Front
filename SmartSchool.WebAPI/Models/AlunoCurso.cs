using System;

namespace SmartSchool.WebAPI.Models
{
    public class AlunoCurso
    {
         public AlunoCurso() { }
       
        public AlunoCurso(int alunoId, int cursoId) 
        {
            this.AlunoId = alunoId;
            this.CursoId = cursoId;               
        }
        public DateTime DataIni { get; set; } = DateTime.Now; // Quando cadastrar já vem com a data preenchida.
        public DateTime? DataFim { get; set; } = null; 
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } // Ja retorna a Classe populada e não apenas o Id
        public int CursoId { get; set; }
        public Curso Curso { get; set; } // Ja retorna a Classe populada e não apenas o Id

    }
}