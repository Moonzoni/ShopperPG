using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IOrcamentoRepository: IBaseRepository<OrcamentoEntity>
    {
        IEnumerable<OrcamentoEntity> FindByCompra(int id);
    }
}
