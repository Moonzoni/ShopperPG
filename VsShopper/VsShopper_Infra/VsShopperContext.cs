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
                    .HasKey(x => x.cod_perfil);

            modelBuilder.Entity<CategoriaEntity>()
                .HasKey(x => x.cod_categoria);

            modelBuilder.Entity<statusEntity>()
                .HasKey(x => x.cod_status);

            modelBuilder.Entity<usuarioEntity>()
                    .HasKey(x => x.cod_usuario);

            modelBuilder.Entity<ComprasEntity>()
                .HasKey(x => x.cod_compra);

            modelBuilder.Entity<OrcamentoEntity>()
                .HasKey(x => x.cod_orcamento);
        }
    }
}
