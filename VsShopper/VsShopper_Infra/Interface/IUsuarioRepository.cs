using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Interface
{
    public interface IusuarioRepository: IBaseRepository<UsuarioEntity>
    {
        bool PodeExcluir(int id);
        UsuarioEntity GetNoTracking(int id);
        UsuarioEntity FindByEmail(string email);
    }
}
