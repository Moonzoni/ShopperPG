using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IPerfilRepository: IBaseRepository<PerfilEntity>
    {
        bool PodeExcluir(int id);
        PerfilEntity GetNoTracking(int id);
    }
}
