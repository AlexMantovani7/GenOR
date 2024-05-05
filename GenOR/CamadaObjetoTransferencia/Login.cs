using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Login
    {
        public Nullable<int> codigo { get; set; }
        public string nome_usuario { get; set; }
        public string senha { get; set; }
        public Pessoa Usuario { get; set; }
    }

    public class ListaLogin : List<Login> { }
}
