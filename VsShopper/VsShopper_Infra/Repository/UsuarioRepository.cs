using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VsShopper_Infra.Repository
{
    public class UsuarioRepository : BaseRepository<UsuarioEntity>, IUsuarioRepository
    {

        public UsuarioRepository(VsShopperContext vsShopperContext) : base(vsShopperContext) { }

        public UsuarioEntity GetNoTracking(int id)
        {
            return this._vsShopperContext.Set<UsuarioEntity>().AsNoTracking<UsuarioEntity>().FirstOrDefault(x => x.cod_usuario == id);
        }

        public bool PodeExcluir(int id)
        {
            var verificaCompras = _vsShopperContext.Set<ComprasEntity>().Any(x => x.cod_usuario == id);
            return !verificaCompras; 
        }
        public UsuarioEntity FindByEmail(string email)
        {
            return this._vsShopperContext.Set<UsuarioEntity>().AsNoTracking<UsuarioEntity>().FirstOrDefault(x => x.email == email);
        }


    }
}
