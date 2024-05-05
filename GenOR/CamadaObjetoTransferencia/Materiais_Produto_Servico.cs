using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Materiais_Produto_Servico
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<decimal> quantidade { get; set; }
        public Nullable<decimal> valor_total { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
        public Material Material { get; set; }
        public Produto_Servico Produto_Servico { get; set; }
    }

    public class ListaMateriais_Produto_Servico : List<Materiais_Produto_Servico> { }
}
