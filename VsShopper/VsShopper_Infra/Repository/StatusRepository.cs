using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{
   public class statusRepository : BaseRepository<statusEntity>, IstatusRepository
    {
        public statusRepository (VsShopperContext vsShopperContext) : base(vsShopperContext) { }

        public bool PodeExcluir(int id)
        {
            var verificastatus = _vsShopperContext.Set<ComprasEntity>().Any(x => x.cod_status == id);
            return !verificastatus;
        }
    }
}
