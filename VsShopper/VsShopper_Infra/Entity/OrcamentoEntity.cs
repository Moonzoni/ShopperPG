using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VsShopper_Infra.Entity
{
    [Table("ORCAMENTO")]
    public class OrcamentoEntity
    {
        [Key]
        public int COD_ORCAMENTO { get; set; }
        public string NOME { get; set; }
        public string LINK { get; set; }
        public string OBSERVACAO{ get; set; }
        public int COD_COMPRAS { get; set; }
    }
}
