using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.DTO
{
    public class UsuarioDTO
    {
        public int cod_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public PerfilEntity perfil { get; set; }
    }
}
