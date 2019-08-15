using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VsShopper_Infra.Entity
{
    [Table("COMPRAS")]
    public class ComprasEntity
    {
        [Key]
        public int cod_compra { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int  cod_status { get; set; }
        public int cod_categoria { get; set; }
        public int cod_usuarios { get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime? data_finalizada { get; set; }
    }    
}
