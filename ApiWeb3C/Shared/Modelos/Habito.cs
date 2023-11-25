using System.ComponentModel.DataAnnotations;

namespace ApiWeb3C.Shared.Modelos
{
    public class Habito
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe capturar el nombre")]
        public string? Nombre { get; set; }
        public virtual ICollection<Jugador>? Personas { get; set; }
    }
}
