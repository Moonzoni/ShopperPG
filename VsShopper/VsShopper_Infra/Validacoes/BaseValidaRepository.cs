using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Validacoes
{
    public class BaseValidaRepository: IBaseValida
    {

        public bool ValidaCampoNull(params string[] param)
        {
            string teste = "";
            foreach (var item in param)
            {
                if (item == teste)
                {
                    return true; 
                }
            }
            return false;
        }
        public bool ValidaEmail(string email)
        {
            Regex validaEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!validaEmail.IsMatch(email))
                return true;
            return false;
        }
        public bool ValidaLink(string link)
        {
            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex ValidaLink = new Regex(pattern);
            if (!ValidaLink.IsMatch(link))
            {
                return true;
            }
            return false;
        }
        public bool ValidaNome(string nome)
        {
            if (!Regex.IsMatch(nome, @"^[ a-zA-Z záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]*$"))
                return true;
            return false;
        }
    }
}
