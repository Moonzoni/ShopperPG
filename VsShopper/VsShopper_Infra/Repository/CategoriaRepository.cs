using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{
    public class CategoriaRepository : BaseRepository<CategoriaEntity>, ICategoriaRepository
    {
        public CategoriaRepository(VsShopperContext baseContext) : base(baseContext) { }

        public bool PodeExcluir(int id)
        {
            var verificaCompras = _vsShopperContext.Set<ComprasEntity>().Any(x => x.cod_categoria == id);
            return !verificaCompras;
        }

        public CategoriaEntity GetNoTracking(int id)
        {
            return this._vsShopperContext.Set<CategoriaEntity>().AsNoTracking<CategoriaEntity>()
                       .FirstOrDefault(x => x.cod_categoria == id);
        }

    }
     
}


