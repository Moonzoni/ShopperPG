using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.DTO
{
    public class UsuarioDTO
    {
        public int COD_USUARIO { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public PerfilEntity PERFIL { get; set; }
    }
}
