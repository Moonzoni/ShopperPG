using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.DTO
{
    public class ComprasDTO
    {
        public int cod_compra { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime data_abertura { get; set; }
        public DateTime? data_finalizada { get; set; }
        public statusEntity status { get; set; }
        public CategoriaEntity categoria { get; set; }
        public usuarioEntity usuario { get; set; }
        public List<OrcamentoEntity> orcamentodtos { get; set; }
    }
}
