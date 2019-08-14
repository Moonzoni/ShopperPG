using System;
using System.Collections.Generic;
using System.Text;

namespace VsShopper_Infra.Interface
{
    public interface IBaseValida
    {
        bool ValidaEmail(string email);
        bool ValidaNome(string nome);
        bool ValidaCampoNull(params string[] param);
        bool ValidaLink(string link);
    }
}
