using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{
   public class StatusRepository : BaseRepository<StatusEntity>, IStatusRepository
    {
        public StatusRepository (VsShopperContext vsShopperContext) : base(vsShopperContext) { }

        public bool PodeExcluir(int id)
        {
            var verificaStatus = _vsShopperContext.Set<ComprasEntity>().Any(x => x.COD_STATUS == id);
            return !verificaStatus;
        }
    }
}
