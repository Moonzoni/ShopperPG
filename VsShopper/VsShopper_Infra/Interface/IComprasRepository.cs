using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.DTO;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IComprasRepository : IBaseRepository<ComprasEntity>
    {
        IEnumerable<ComprasEntity> FindByStatus(int id);
        IEnumerable<ComprasEntity> FindByCategoria(int id);
        IEnumerable<ComprasEntity> FindByTitulo(string Titulo);
        IEnumerable<ComprasEntity> FindByDescricao(string Descricao);
        bool PodeExcluir(int id);
    }
}
