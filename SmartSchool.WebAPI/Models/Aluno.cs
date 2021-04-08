using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Aluno
    {
        public Aluno() { }

        public Aluno(int id, string nome, string sobrenome, string telefone)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = Sobrenome;
            this.Telefone = Telefone;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        

        // EXEMPLO ABAIXO: RELAÇÃO UM PARA MUITOS
        // Referencia de Aluno e Disciplina -> (Diversas Disciplinas Sendo Cursadas por Diversos Alunos)
        // ALUNOS DISCIPLINAS
        //==========================

        // AlunoId = 1 - DisciplinaId =1
        // AlunoId = 1 - DisciplinaId =2
        // AlunoId = 2 - DisciplinaId =2
        // AlunoId = 2 - DisciplinaId =1
        // AlunoId = 3 - DisciplinaId =2
        // AlunoId = 4 - DisciplinaId =1
        // AlunoId = 4 - DisciplinaId =3
        // AlunoId = 4 - DisciplinaId =5
        // AlunoId = 5 - DisciplinaId =4
        // AlunoId = 4 - DisciplinaId =4
        // E ASSIM VAI...
        public IEnumerable <AlunoDisciplina> AlunosDisciplinas { get; set; }        
    }
}