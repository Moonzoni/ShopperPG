using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;
using System.Linq;

namespace VsShopper_Infra.Repository
{
    public class ComprasRepository : BaseRepository<ComprasEntity>, IComprasRepository
    {
        public bool PodeExcluir(int id)
        {
            var verificaCompras = _vsShopperContext.Set<OrcamentoEntity>().Any(x => x.COD_COMPRAS == id);
            return !verificaCompras;
        }
        public ComprasRepository(VsShopperContext vsShopperContext) : base(vsShopperContext){ }

        public virtual IEnumerable<ComprasEntity> FindByStatus(int id)
        {

            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.COD_STATUS == id);
        }
        public virtual IEnumerable<ComprasEntity> FindByTitulo(string Titulo)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.TITULO.Contains(Titulo));  
        }
        public virtual IEnumerable<ComprasEntity> FindByCategoria(int cod_categoria)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.COD_CATEGORIA == cod_categoria);
        }
        public virtual IEnumerable<ComprasEntity> FindByDescricao(string Descricao)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.DESCRICAO.Contains(Descricao));
        }
    }
}
