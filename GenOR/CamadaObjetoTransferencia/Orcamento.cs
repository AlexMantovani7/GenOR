using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Orcamento
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<DateTime> ultima_atualizacao { get; set; }
        public Nullable<DateTime> validade { get; set; }
        public string prazo_entrega { get; set; }
        public string observacao { get; set; }
        public Nullable<decimal> total_produtos_servicos { get; set; }
        public Nullable<decimal> desconto { get; set; }
        public Nullable<decimal> total_orcamento { get; set; }
        public string descricao_pagamento { get; set; }
        public Nullable<decimal> valor_entrada { get; set; }
        public Nullable<int> quantidade_parcelas { get; set; }
        public Nullable<decimal> valor_parcela { get; set; }
        public Nullable<decimal> juros { get; set; }
        public Nullable<decimal> valor_juros { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
        public Pessoa Usuario { get; set; }
        public Pessoa Cliente { get; set; }
    }

    public class ListaOrcamento : List<Orcamento> { }
}
