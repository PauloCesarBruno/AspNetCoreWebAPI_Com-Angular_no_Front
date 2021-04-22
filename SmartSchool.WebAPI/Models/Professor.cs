using System;
using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor() { }
        public Professor(int id,
                        int registro,
                        string nome,
                        string sobrenome)
        {
            this.Id = id;
            this.Registro = registro;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
        }
        public int Id { get; set; }
         public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
         public string Telefone { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now; // Quando cadastrar já vem com a data preenchida.
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true; // Se não for atribuido vai ser sempre true(Ativo)

        // Associação do Professor com a Disciplina (Um Professor pode lecionar para várias Disciplinas)
        // EXEMPLO ABAIXO: RELAÇÃO UM PARA MUITOS
        //============================================================
        // PROFESSOR -> MARCOS -> HISTÓRIA, PORTUGUÊS E MATEMÁTICA
        // PROFESSOR RODRIGO -> FISICA, MATEMÁTICA E CIÊNCIA
        // PROFESSOR CARLOS -> FISICA E QUIMICA
        // PROFESSOR PAULO -> PORTUGUÊS, QUIMICA E GEOGRAFIA
        // E ASSIM VAI...
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}