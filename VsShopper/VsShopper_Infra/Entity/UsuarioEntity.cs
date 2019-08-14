using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace VsShopper_Infra.Entity
{
    [Table("USUARIO")]
    public class UsuarioEntity
    {
        [Key]
        public int COD_USUARIO { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public int COD_PERFIL { get; set; }
    }
}
