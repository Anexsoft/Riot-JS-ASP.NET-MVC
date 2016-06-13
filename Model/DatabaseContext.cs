namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=AnexGRIDContext")
        {
        }

        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Profesion> Profesion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profesion>()
                .HasMany(e => e.Empleado)
                .WithOptional(e => e.Profesion)
                .HasForeignKey(e => e.Profesion_id);
        }
    }
}
