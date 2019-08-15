using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;
using System.Linq;

namespace VsShopper_Infra.Interface
{
    public interface IStatusRepository: IBaseRepository<StatusEntity>
    {
        bool PodeExcluir(int id);
        StatusEntity GetStatusByName(string name);
    }
    
    

}
