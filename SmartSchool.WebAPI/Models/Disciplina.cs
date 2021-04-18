using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Disciplina
    {
        public Disciplina() { }

        public Disciplina(int id, string nome, int professorId, int cursoId)
        {
            this.Id = id;
            this.Nome = nome;
            this.ProfessorId = professorId;
            this.CursoId = cursoId;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }

        // EXEMPLO ABAIXO: RELAÇÃO UM PARA MUITOS
        // UM PROFESSOR PODE LECIONAR EM VÁRIAS DISCIPLINAS
        // EX:
        //============================================================
        // PROFESSOR -> MARCOS -> HISTÓRIA, PORTUGUÊS E MATEMÁTICA
        // PROFESSOR RODRIGO -> FISICA, MATEMÁTICA E CIÊNCIA
        // PROFESSOR CARLOS -> FISICA E QUIMICA
        // PROFESSOR PAULO -> PORTUGUÊS, QUIMICA E GEOGRAFIA
        // E ASSIM VAI...
        public int? PrerequsitoId { get; set; }  = null; // ? => Pode ser NULO
        public Disciplina Prerequsito { get; set; } // Ex.: Imagina se você vai fazer Matematica 2 se não fez a 1...
        public int ProfessorId { get; set; } //Uma Disciplina para um Professor
        public Professor Professor { get; set; }

        // Relacionamento Disciplina x Curso Muitos para Muitos
        public int CursoId { get; set; } //Uma Disciplina tem que ser cadastrada para um determinado Curso.
        public Curso Curso { get; set; }


        // EXEMPLO ABAIXO: RELAÇÃO MUITOS PARA MUITOS
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