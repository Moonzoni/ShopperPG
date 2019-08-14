using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra
{
    public class VsShopperContext: DbContext
    {

        public VsShopperContext(DbContextOptions<VsShopperContext> dbContextOptions) : base(dbContextOptions){ }

        public void SendChanges()
        {
            ChangeTracker.DetectChanges();
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerfilEntity>()
                    .HasKey(x => x.COD_PERFIL);

            modelBuilder.Entity<CategoriaEntity>()
                .HasKey(x => x.COD_CATEGORIA);

            modelBuilder.Entity<StatusEntity>()
                .HasKey(x => x.COD_STATUS);

            modelBuilder.Entity<UsuarioEntity>()
                    .HasKey(x => x.COD_USUARIO);

            modelBuilder.Entity<ComprasEntity>()
                .HasKey(x => x.COD_COMPRAS);

            modelBuilder.Entity<OrcamentoEntity>()
                .HasKey(x => x.COD_ORCAMENTO);
        }
    }
}
