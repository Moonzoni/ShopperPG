﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VsShopper_Infra.Entity
{
    [Table ("PERFIL")]
    public class PerfilEntity
    {
        [Key]
        public int cod_perfil { get; set; }
        public string nome { get; set; }
    }
}
