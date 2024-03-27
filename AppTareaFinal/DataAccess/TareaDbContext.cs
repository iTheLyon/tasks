using AppTareaFinal.Models;
using AppTareaFinal.Utils;
using Microsoft.EntityFrameworkCore;

namespace AppTareaFinal.DataAccess
{
    public class TareaDbContext :  DbContext
    {
        public DbSet<Tarea> Tareas { get;set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string pathConnection = $"Filename={DBConnection.getPath("tasks.db")}";
            optionsBuilder.UseSqlite(pathConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(column => column.IdTarea);
                entity.Property(column => column.IdTarea).IsRequired().ValueGeneratedOnAdd();
            });            
        }
    }
}
