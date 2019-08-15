using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IstatusRepository: IBaseRepository<statusEntity>
    {
        bool PodeExcluir(int id);
    }
}
