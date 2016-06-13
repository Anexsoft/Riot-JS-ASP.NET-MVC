namespace Model
{
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Profesion")]
    public partial class Profesion
    {
        public Profesion()
        {
            Empleado = new List<Empleado>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public ICollection<Empleado> Empleado { get; set; }

        public List<Profesion> Todo()
        {
            using (var ctx = new DatabaseContext())
            {
                return ctx.Profesion.OrderBy(x => x.Nombre).ToList();
            }
        }
    }
}
