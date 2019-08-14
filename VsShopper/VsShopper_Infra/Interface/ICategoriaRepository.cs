using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface ICategoriaRepository: IBaseRepository<CategoriaEntity>
    {
        bool PodeExcluir(int id);

        CategoriaEntity GetNoTracking(int id);

    }
}
