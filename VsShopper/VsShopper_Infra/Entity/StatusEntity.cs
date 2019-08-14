using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VsShopper_Infra.Entity
{
    [Table("STATUSS")]
    public class StatusEntity
    {
        [Key]
        public int COD_STATUS { get; set; }
        public string NOME { get; set; }
    }
}
