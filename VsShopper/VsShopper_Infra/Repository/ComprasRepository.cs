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
            var verificaCompras = _vsShopperContext.Set<OrcamentoEntity>().Any(x => x.cod_compra == id);
            return !verificaCompras;
        }
        public ComprasRepository(VsShopperContext vsShopperContext) : base(vsShopperContext){ }

        public virtual IEnumerable<ComprasEntity> FindBystatus(int id)
        {

            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.cod_status == id);
        }
        public virtual IEnumerable<ComprasEntity> FindBytitulo(string titulo)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.titulo.Contains(titulo));  
        }
        public virtual IEnumerable<ComprasEntity> FindByCategoria(int cod_categoria)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.cod_categoria == cod_categoria);
        }
        public virtual IEnumerable<ComprasEntity> FindByDescricao(string descricao)
        {
            return _vsShopperContext.Set<ComprasEntity>().Where(x => x.descricao.Contains(descricao));
        }
    }
}
