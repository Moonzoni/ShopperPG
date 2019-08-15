using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IStatusRepository: IBaseRepository<StatusEntity>
    {
        bool PodeExcluir(int id);
    }
}
