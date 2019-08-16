using Microsoft.EntityFrameworkCore;
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
            var verificastatus = _vsShopperContext.Set<ComprasEntity>().Any(x => x.cod_status == id);
            return !verificastatus;
        }
        public StatusEntity GetStatusByName(string name)
        {
            return _vsShopperContext.Set<StatusEntity>().FirstOrDefault(x => x.nome.Contains(name));
        }

<<<<<<< HEAD
        public StatusEntity GetNoTracking(int id)
        {
            return this._vsShopperContext.Set<StatusEntity>().AsNoTracking<StatusEntity>()
                       .FirstOrDefault(x => x.cod_status == id);
        }
=======
      


>>>>>>> d11d0c08b9582d64a5d8093ed70b5e2829018876
    }
}
