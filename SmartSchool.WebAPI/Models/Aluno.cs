using System;
using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Aluno
    {
        public Aluno() { }

        public Aluno(int id,
                     int matricula,
                     string nome,
                     string sobrenome,
                     string telefone,
                     DateTime dataNasc)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.DataNasc = dataNasc;
        }
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true; // Se não for atribuido vai ser sempre true(Ativo)
        

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