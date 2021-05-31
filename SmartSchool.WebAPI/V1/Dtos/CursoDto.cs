using System.Collections;
using System.Collections.Generic;

namespace SmartSchool.WebAPI.V1.Dtos
{
    public class CursoDto
    {        
        public int Id { get; set; }
        public string Nome { get; set; }

        // Disciplinas que esse Curso tem...
        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }
    }
}