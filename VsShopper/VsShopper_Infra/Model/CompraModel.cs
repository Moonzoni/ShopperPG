using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.Model
{
    public class CompraModel
    {
        public int Cod_compra { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int cod_categoria { get; set; }
        
        public int cod_usuario { get; set; }
        public List<OrcamentoEntity> orcamentodtos { get; set; }

    }
}
