using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace VsShopper_Infra.Entity
{
    [Table("usuario")]
    public class usuarioEntity
    {
        [Key]
        public int cod_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public int cod_perfil { get; set; }
    }
}
