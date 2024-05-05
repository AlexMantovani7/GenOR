using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Materiais_Orcamento
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<decimal> quantidade_total { get; set; }
        public Nullable<decimal> valor_total { get; set; }
        public Produtos_Servicos_Orcamento Produtos_Servicos_Orcamento { get; set; }
        public Materiais_Produto_Servico Materiais_Produto_Servico { get; set; }
    }

    public class ListaMateriais_Orcamento : List<Materiais_Orcamento> { }
}
