using System;
using System.Collections.Generic;
using System.Text;
using VsShopper_Infra.Entity;

namespace VsShopper_Infra.DTO
{
    public class ComprasDTO
    {
        public int COD_COMPRAS { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public DateTime Data_Abertura { get; set; }
        public DateTime? Data_Finalizado { get; set; }
        public StatusEntity STATUS { get; set; }
        public CategoriaEntity CATEGORIA { get; set; }
        public UsuarioEntity USUARIO { get; set; }
        public List<OrcamentoEntity> OrcamentoDTOs { get; set; }
    }
}
