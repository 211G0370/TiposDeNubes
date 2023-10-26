using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiposDeNubes.Models
{
    public class NubeModel
    {
        public int Id { get; set; }
        public string? Genero { get; set; }
        public string? Especie { get; set; }
        public string? Nivel { get; set; }
        public string? Caracteristicas { set; get; }
    }
}
