using System;
using System.IO;
using CamadaAcessoDados;

namespace CamadaProcessamento
{
    public class ProcBD
    {
        private AcessoDadosMySqlServer acessoDadosMySqlServer = new AcessoDadosMySqlServer();
        
        public bool Check_PrimeiraBDConnection()
        {
            try
            {
                string xmlBDSC = acessoDadosMySqlServer.LocalizarXML("BDSC.xml");

                if (!File.Exists(xmlBDSC))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool Cadastrar_BDConnection(string svr, string db, string id, string pw)
        {
            try
            {
                return acessoDadosMySqlServer.DBXml_Serializar(svr, db, id, pw);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Testar_BDConnection()
        {
            try
            {
                bool semConectarUmBD = false;
                return (!acessoDadosMySqlServer.TestarConexao(semConectarUmBD));
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool Executar_BackupBD(string path_Destino)
        {
            try
            {
                return acessoDadosMySqlServer.Backup_BD(path_Destino);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Executar_RestoreBD(string path_ArquivoBackupZip)
        {
            try
            {
                return acessoDadosMySqlServer.Restore_BD(path_ArquivoBackupZip);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
