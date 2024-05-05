using System;
using System.Collections.Generic;

namespace CamadaObjetoTransferencia
{
    public class Grupo_Unidade
    {
        public Nullable<int> codigo { get; set; }
        public string sigla { get; set; }
        public string descricao { get; set; }
        public Nullable<char> material_ou_produto { get; set; }
        public Nullable<bool> ativo_inativo { get; set; }
    }

    public class ListaGrupo_Unidade : List<Grupo_Unidade> { }
}
