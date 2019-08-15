using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VsShopper_Infra.Entity
{
    [Table("CATEGORIA")]
    public class CategoriaEntity
    {
        [Key]
        public int cod_categoria { get; set; }
        public string nome { get; set; }
    }
}
