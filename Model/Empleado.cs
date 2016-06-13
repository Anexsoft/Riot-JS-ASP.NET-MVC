namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("Empleado")]
    public partial class Empleado
    {
        public int id { get; set; }

        public int? Profesion_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        public decimal Sueldo { get; set; }

        public byte Sexo { get; set; }

        [Required]
        [StringLength(20)]
        public string FechaNacimiento { get; set; }

        [Required]
        [StringLength(20)]
        public string FechaRegistro { get; set; }

        public Profesion Profesion { get; set; }

        public AnexGRIDResponde Listar(AnexGRID agrid) 
        {
            try
            {
                using (var ctx = new DatabaseContext()) 
                {
                    agrid.Inicializar();

                    var query = ctx.Empleado.Include(x => x.Profesion)
                                            .Where( x => x.id > 0);

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.id)
                                                       : query.OrderBy(x => x.id);

                    if (agrid.columna == "Nombre") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.Nombre)
                                                       : query.OrderBy(x => x.Nombre);

                    if (agrid.columna == "Correo") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.Correo)
                                                       : query.OrderBy(x => x.Correo);

                    if (agrid.columna == "Sueldo") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.Sueldo)
                                                       : query.OrderBy(x => x.Sueldo);

                    if (agrid.columna == "FechaRegistro") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.FechaRegistro)
                                                       : query.OrderBy(x => x.FechaRegistro);

                    if (agrid.columna == "Profesion_id") query = agrid.columna_orden == "DESC"
                                                       ? query.OrderByDescending(x => x.Profesion_id)
                                                       : query.OrderBy(x => x.Profesion.Nombre);

                    // Filtrar
                    foreach (var f in agrid.filtros) 
                    {
                        if (f.columna == "Nombre")
                            query = query.Where(x => x.Nombre.StartsWith(f.valor));

                        if (f.columna == "Correo")
                            query = query.Where(x => x.Correo.StartsWith(f.valor));

                        if (f.columna == "Profesion_id" && f.valor != "")
                            query = query.Where(x => x.Profesion_id.ToString() == f.valor);

                        if (f.columna == "Sexo" && f.valor != "")
                            query = query.Where(x => x.Sexo.ToString() == f.valor);
                    }

                    var empleados = query.Skip(agrid.pagina)
                                         .Take(agrid.limite)
                                         .ToList();

                    agrid.SetData(
                            from e in empleados
                            select new
                            {
                                e.id,
                                e.Nombre,
                                e.Correo,
                                e.Sueldo,
                                e.Apellido,
                                e.Sexo,
                                e.FechaNacimiento,
                                e.FechaRegistro,
                                Profesion = new {
                                    id = e.Profesion.id,
                                    Nombre = e.Profesion.Nombre
                                }
                            }
                        ,
                        query.Count()
                    );
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        public EmpleadoModal Obtener(int id)
        {
            var model = new EmpleadoModal();

            try
            {
                using (var ctx = new DatabaseContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    model = ctx.Empleado.Where(x => x.id == id).Include(x => x.Profesion)
                               .Select(x => new EmpleadoModal {
                                   id = x.id,
                                   Nombre = x.Nombre,
                                   Apellido = x.Apellido,
                                   Sueldo = x.Sueldo,
                                   Correo = x.Correo,
                                   Profesion = x.Profesion.Nombre,
                                   Sexo = x.Sexo,
                                   FechaNacimiento = x.FechaNacimiento,
                                   FechaRegistro = x.FechaRegistro
                               }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return model;
        }

        public void Registrar(List<Empleado> model)
        {
            using (var ctx = new DatabaseContext())
            {
                foreach (var m in model) {
                    m.FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");
                    ctx.Entry(m).State = EntityState.Added;
                }

                ctx.SaveChanges();
            }
        }
    }

    public class EmpleadoModal {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public decimal Sueldo { get; set; }
        public string Profesion { get; set; }
        public byte Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaRegistro { get; set; }
    }
}
