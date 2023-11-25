using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiWeb3C.Shared.Modelos
{
    public class Jugador
    {
        [Key] // Esto indica que la propiedad Nickname será la clave primaria
        public int Nickname { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre")]
        [StringLength(100)]
        public string? Posicion { get; set; }

        [Required(ErrorMessage = "Debe escribir la posicion")]
        [StringLength(20)]
        public string? Nacionalidad { get; set; }

        [Required(ErrorMessage = "Debe escribir la nacionalidad")]
        [StringLength(50)]
        public string? Correo { get; set; }

        public int ClasificacionId { get; set; }
        public Equipo? Clasificacion { get; set; }
        public ICollection<Habito>? Habitos { get; set; }
    }
}
