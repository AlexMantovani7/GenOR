using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Telefone
    {
        public Nullable<int> codigo { get; set; }
        public string ddd { get; set; }
        public string numero { get; set; }
        public string observacao { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
        public Pessoa Pessoa { get; set; }
    }

    public class ListaTelefone : List<Telefone> { }
}
