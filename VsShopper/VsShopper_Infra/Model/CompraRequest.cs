using System;
using System.Collections.Generic;
using System.Text;

namespace VsShopper_Infra.Model
{
    public class CompraRequest
    {
        public int Cod_compra { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
    }
}
