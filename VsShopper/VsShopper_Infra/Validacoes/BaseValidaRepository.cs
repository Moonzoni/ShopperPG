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

            foreach (var item in param)
            {
                
                if (string.IsNullOrWhiteSpace(item))
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
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public bool ValidaString(string nome)
        {
            if (!Regex.IsMatch(nome, @"^[ a-zA-Z záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]*$"))
            {
                return true;
            }
            return false;
        }

        

        

        public bool ValidaVazio(string Str)
        {
            if (Str != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidaInt(int valor)
        {
            var padrao = valor.ToString();
            if (!Regex.IsMatch(padrao, @"^[0-9]"))
            {
                return true;
            }
            return false;
        }
    }
}
