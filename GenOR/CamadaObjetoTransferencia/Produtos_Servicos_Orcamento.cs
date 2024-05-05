using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Produtos_Servicos_Orcamento
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<decimal> quantidade { get; set; }
        public Nullable<decimal> valor_total { get; set; }
        public Produto_Servico Produto_Servico { get; set; }
        public Orcamento Orcamento { get; set; }
    }

    public class ListaProdutos_Servicos_Orcamento : List<Produtos_Servicos_Orcamento> { }
}
