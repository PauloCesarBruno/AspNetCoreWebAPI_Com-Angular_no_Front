using System;

namespace SmartSchool.WebAPI.Models
{
    public class AlunoDisciplina // Classe Intermediária entre Aluno e Disciplina
    {                            // Essa Classe Associa os Id´s de Alunos e Disciplinas
                                 // Muitos para Muitos
        public AlunoDisciplina() { }
       
        public AlunoDisciplina(int alunoId, int disciplinaId) 
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;               
        }
        public DateTime DataIni { get; set; } = DateTime.Now; // Quando cadastrar já vem com a data preenchida.
        public DateTime? DataFim { get; set; } = null; // Pode ser Nulo (posso não ter terminado essa disciplina).
        public int? Nota { get; set; } = null; // Nota pode ser nula.
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } // Ja retorna a Classe populada e não apenas o Id
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } // Ja retorna a Classe populada e não apenas o Id
    }
}

// ALUNOS DISCIPLINAS
// VÁRIOS ALUNOS PODEM FAZER VARIAS DISCIPLINAS E VÁRIAS DISCIPLINAS PODEM TER VÁRIOS ALUNOS
// RELACÃO MUITOS PARA MUITOS
//===========================================================================================

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