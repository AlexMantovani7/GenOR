using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Endereco
    {
        public Nullable<int> codigo { get; set; }
        public string endereco { get; set; }
        public string complemento { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string observacao { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
        public Pessoa Pessoa { get; set; }
    }

    public class ListaEndereco : List<Endereco> { }
}
