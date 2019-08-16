using System;
using System.Collections.Generic;
using System.Text;

namespace VsShopper_Infra.Interface
{
    public interface IBaseValida
    {
        bool ValidaEmail(string email);
        bool ValidaCampoNull(params string[] param);
<<<<<<< HEAD
        bool ValidaLink(string link);
        bool ValidaVazio(string Str);
=======
        bool ValidaLink(string link);       
        bool ValidaVazio(string Str);        
>>>>>>> d11d0c08b9582d64a5d8093ed70b5e2829018876
        bool ValidaString(string nome);
        bool ValidaInt(int valor);
    }
}
