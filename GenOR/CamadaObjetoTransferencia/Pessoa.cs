using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Pessoa
    {
        public Nullable<int> codigo { get; set; }
        public string tipo_pessoa { get; set; }
        public string nome_razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public string cpf_cnpj { get; set; }
        public string inscricao_estadual { get; set; }
        public string email { get; set; }
        public string observacao { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
    }

    public class ListaPessoa : List<Pessoa> { }
}
