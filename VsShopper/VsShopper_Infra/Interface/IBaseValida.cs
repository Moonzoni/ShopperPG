using System;
using System.Collections.Generic;
using System.Text;

namespace VsShopper_Infra.Interface
{
    public interface IBaseValida
    {
        bool ValidaEmail(string email);
        bool ValidaCampoNull(params string[] param);
        bool ValidaLink(string link);
       
        bool ValidaVazio(string Str);
        bool ValidaUnique(string nome);
        bool ValidaString(string nome);
        bool ValidaInt(int valor);
    }
}
