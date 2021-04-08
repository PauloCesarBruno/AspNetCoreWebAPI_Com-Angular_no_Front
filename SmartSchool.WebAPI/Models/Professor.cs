using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor() { }
        public Professor(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }

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