using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class LOG
    {
        public Nullable<int> codigo { get; set; }
        public Nullable<DateTime> data_registro { get; set; }
        public string operacao { get; set; }
        public string registro { get; set; }
        public string informacoes_registro { get; set; }
    }
    
    public class ListaLOG : List<LOG> { }
}
