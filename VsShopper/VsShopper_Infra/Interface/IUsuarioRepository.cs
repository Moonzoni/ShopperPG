using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IusuarioRepository: IBaseRepository<usuarioEntity>
    {
        bool PodeExcluir(int id);
        usuarioEntity GetNoTracking(int id);
        usuarioEntity FindByEmail(string email);
    }
}
