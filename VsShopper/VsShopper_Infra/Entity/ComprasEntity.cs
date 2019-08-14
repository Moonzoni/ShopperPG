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
        public int COD_COMPRAS { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public int  COD_STATUS { get; set; }
        public int COD_CATEGORIA { get; set; }
        public int COD_USUARIO { get; set; }
        public DateTime DATA_ABERTURA { get; set; }
        public DateTime? DATA_FINALIZACAO { get; set; }
    }    
}
