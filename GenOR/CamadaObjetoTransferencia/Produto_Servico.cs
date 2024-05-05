using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Produto_Servico
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<DateTime> ultima_atualizacao { get; set; }
        public string imagem { get; set; }
        public string descricao { get; set; }
        public Nullable<decimal> altura { get; set; }
        public Nullable<decimal> largura { get; set; }
        public Nullable<decimal> comprimento { get; set; }
        public Nullable<decimal> valor_total_materiais { get; set; }
        public Nullable<decimal> maoObra { get; set; }
        public Nullable<decimal> valor_maoObra { get; set; }
        public Nullable<decimal> valor_total { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
        public Grupo_Unidade Unidade { get; set; }
        public Grupo_Unidade Grupo { get; set; }
    }

    public class ListaProduto_Servico : List<Produto_Servico> { }
}
