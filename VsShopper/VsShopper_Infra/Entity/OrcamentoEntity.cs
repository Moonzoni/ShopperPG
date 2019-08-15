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
        public int cod_orcamento { get; set; }
        public string nome { get; set; }
        public string link { get; set; }
        public string observacao{ get; set; }
        public int cod_compra { get; set; }
    }
}
