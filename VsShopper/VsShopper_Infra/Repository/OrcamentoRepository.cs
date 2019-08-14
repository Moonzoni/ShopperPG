using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{
    public class OrcamentoRepository : BaseRepository<OrcamentoEntity>, IOrcamentoRepository
    {
        public OrcamentoRepository(VsShopperContext vsShopperContext) : base(vsShopperContext) { }

        public virtual IEnumerable<OrcamentoEntity> FindByCompra(int cod_compra)
        {
            return _vsShopperContext.Set<OrcamentoEntity>().Where(x => x.COD_COMPRAS == cod_compra).ToList();
        }
    }
}
