using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{
   public class PerfilRepository : BaseRepository<PerfilEntity>, IPerfilRepository
    {
        public PerfilRepository(VsShopperContext vsShopperContext) : base(vsShopperContext) { }

        public PerfilEntity GetNoTracking(int id)
        {
            return this._vsShopperContext.Set<PerfilEntity>().AsNoTracking<PerfilEntity>().FirstOrDefault(x => x.cod_perfil == id);

        }

        public bool PodeExcluir(int id)
        {
            var verificastatus = _vsShopperContext.Set<UsuarioEntity>().Any(x => x.cod_perfil == id);
            return !verificastatus;
        }

    }
}
