using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
/*using System.Reflection;*/ /*P. DESENVOLVIMENTO(trocar)*/
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Ionic.Zip; /*DotNetZip*/

namespace CamadaAcessoDados
{
    public class AcessoDadosMySqlServer
    {
        #region Variaveis

        private MySqlParameterCollection parametros;
        private string path_InternoSistema;

        #endregion

        public AcessoDadosMySqlServer()
        {
            try
            {
                this.parametros = new MySqlCommand().Parameters;
                this.path_InternoSistema = Path.GetFullPath(Path.Combine(Application.StartupPath, @"Resources\")); /*P. DESENVOLVIMENTO(trocar)*/ /*Path.GetFullPath(Assembly.GetExecutingAssembly().Location + @"\..\..\..\Resources\");*/
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Connexão e Interação BD

        private MySqlConnection CriarConexao(bool semConectarUmBD)
        {
            try
            {
                BDSC bdscCripy = new BDSC();
                bdscCripy = DBXml_Deserializar();

                BDSC bdscDescripy = new BDSC()
                {
                    svr = Decrypt(bdscCripy.svr),
                    db = Decrypt(bdscCripy.db),
                    id = Decrypt(bdscCripy.id),
                    pw = Decrypt(bdscCripy.pw)
                };

                if (semConectarUmBD)
                    return new MySqlConnection($"server=" + bdscDescripy.svr + "; Uid=" + bdscDescripy.id + "; pwd=" + bdscDescripy.pw + ";");
                else
                    return new MySqlConnection($"server=" + bdscDescripy.svr + "; database=" + bdscDescripy.db + "; Uid=" + bdscDescripy.id + "; pwd=" + bdscDescripy.pw + ";");
            }
            catch (Exception exception)
            {
                throw new Exception("Falha ao criar conexão com o banco de dados.", exception);
            }
        }

        public bool TestarConexao(bool semConectarUmBD)
        {
            try
            {
                using (MySqlConnection conexao = CriarConexao(semConectarUmBD))
                {
                    conexao.Open();
                }
                
                return semConectarUmBD;
            }
            catch (Exception)
            {
                if (semConectarUmBD)
                    return false;
                else
                    return true;
            }
        }

        public void AdicionarParametro(string nomeParametro, object valor)
        {
            try
            {
                this.parametros.Add(new MySqlParameter(nomeParametro, valor));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LimparParametros()
        {
            try
            {
                this.parametros.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AdicionarParametroComando(MySqlCommand comando)
        {
            try
            {
                comando.Parameters.Clear();

                foreach (MySqlParameter parametro in this.parametros)
                {
                    comando.Parameters.Add(new MySqlParameter(parametro.ParameterName, parametro.Value));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object ExecutarScalar(string comandoTexto, CommandType tipoComando)
        {
            try
            {
                using (MySqlConnection conexao = CriarConexao(false))
                {
                    conexao.Open();
                    MySqlCommand comando = conexao.CreateCommand();
                    comando.CommandTimeout = 7200;
                    comando.CommandText = comandoTexto;
                    comando.CommandType = tipoComando;
                    AdicionarParametroComando(comando);

                    return comando.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ObterDataTable(string comandoTexto, CommandType tipoComando)
        {
            try
            {
                using (MySqlConnection conexao = CriarConexao(false))
                {
                    conexao.Open();

                    MySqlCommand comando = conexao.CreateCommand();
                    comando.CommandTimeout = 7200;
                    comando.CommandText = comandoTexto;
                    comando.CommandType = tipoComando;
                    AdicionarParametroComando(comando);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(comando);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    return dataSet.Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Criptografia e Arquivo de Conexão

        private string Encrypt(string informacao)
        {
            try
            {
                string informacaoParaCriptografar = "";
                
                byte[] chavePublicaByte = { };
                chavePublicaByte = Encoding.UTF8.GetBytes(BDKs.K_Pub);

                byte[] chavePrivadaByte = { };
                chavePrivadaByte = Encoding.UTF8.GetBytes(BDKs.K_Pri);
                
                byte[] listaByte = Encoding.UTF8.GetBytes(informacao);
                
                MemoryStream memoryStream = null;
                CryptoStream cryptoStream = null;
                using (DESCryptoServiceProvider cryptoSP = new DESCryptoServiceProvider())
                {
                    memoryStream = new MemoryStream();

                    cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateEncryptor(chavePublicaByte, chavePrivadaByte), CryptoStreamMode.Write);
                    cryptoStream.Write(listaByte, 0, listaByte.Length);
                    cryptoStream.FlushFinalBlock();

                    informacaoParaCriptografar = Convert.ToBase64String(memoryStream.ToArray());
                }
                return informacaoParaCriptografar;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private string Decrypt(string informacao)
        {
            try
            {
                string informacaoDescriptografada = "";

                byte[] chavePublicaByte = { };
                chavePublicaByte = Encoding.UTF8.GetBytes(BDKs.K_Pub);

                byte[] chavePrivadaByte = { };
                chavePrivadaByte = Encoding.UTF8.GetBytes(BDKs.K_Pri);
                
                byte[] listaByte = new byte[informacao.Replace(" ", "+").Length];
                listaByte = Convert.FromBase64String(informacao.Replace(" ", "+"));
                
                MemoryStream memoryStream = null;
                CryptoStream cryptoStream = null;
                using (DESCryptoServiceProvider descryptoSP = new DESCryptoServiceProvider())
                {
                    memoryStream = new MemoryStream();

                    cryptoStream = new CryptoStream(memoryStream, descryptoSP.CreateDecryptor(chavePublicaByte, chavePrivadaByte), CryptoStreamMode.Write);
                    cryptoStream.Write(listaByte, 0, listaByte.Length);
                    cryptoStream.FlushFinalBlock();

                    Encoding encoding = Encoding.UTF8;
                    informacaoDescriptografada = encoding.GetString(memoryStream.ToArray());
                }

                return informacaoDescriptografada;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }

        public string LocalizarXML(string nomeArquivo)
        {
            try
            {
                return Path.Combine(path_InternoSistema, @"Documentos\" + nomeArquivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DBXml_Serializar(string servidor, string baseDados, string idUsuario, string senha)
        {
            string xmlBDSC_Patch = LocalizarXML("BDSC.xml");

            try
            {
                bool semConectarUmBD = true;

                if (!File.Exists(xmlBDSC_Patch))
                {
                    BDSC bdcs = new BDSC
                    {
                        svr = Encrypt(servidor),
                        db = Encrypt(baseDados),
                        id = Encrypt(idUsuario),
                        pw = Encrypt(senha)
                    };

                    using (StreamWriter stream = new StreamWriter(xmlBDSC_Patch))
                    {
                        XmlSerializer xmlSerializar = new XmlSerializer(typeof(BDSC));
                        xmlSerializar.Serialize(stream, bdcs);
                        stream.Close();
                    }

                    if (TestarConexao(semConectarUmBD))
                    {
                        if (TestarConexao(!semConectarUmBD))
                        {
                            using (MySqlConnection conexao = CriarConexao(semConectarUmBD))
                            {
                                string query_CriandoBD = "CREATE DATABASE IF NOT EXISTS " + baseDados + ";";
                                using (MySqlCommand mySqlCommand = new MySqlCommand(query_CriandoBD, conexao))
                                {
                                    conexao.Open();
                                    mySqlCommand.ExecuteNonQuery();
                                    conexao.Close();
                                }
                            }

                            using (MySqlConnection conexao = CriarConexao(!semConectarUmBD))
                            {
                                using (MySqlCommand mySqlCommand = new MySqlCommand())
                                {
                                    using (MySqlBackup mySqlBackup = new MySqlBackup(mySqlCommand))
                                    {
                                        string pathMySql_BackupCriacaoBD = Path.Combine(path_InternoSistema, "GenOR - Criar BD.sql");

                                        mySqlCommand.Connection = conexao;
                                        conexao.Open();
                                        mySqlBackup.ImportFromFile(pathMySql_BackupCriacaoBD);
                                        conexao.Close();
                                    }
                                }
                            }
                        }

                        return true;
                    }

                    if (File.Exists(xmlBDSC_Patch))
                        File.Delete(xmlBDSC_Patch);

                    return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                if (File.Exists(xmlBDSC_Patch))
                    File.Delete(xmlBDSC_Patch);

                return false;
            }
        }

        private BDSC DBXml_Deserializar()
        {
            try
            {
                string xmlBDSC_Patch = LocalizarXML("BDSC.xml");

                if (File.Exists(xmlBDSC_Patch))
                {
                    BDSC bdsc = new BDSC();
                    using (FileStream stream = new FileStream(xmlBDSC_Patch, FileMode.Open))
                    {
                        XmlSerializer xmlSerializar = new XmlSerializer(typeof(BDSC));
                        bdsc = (BDSC)xmlSerializar.Deserialize(stream);
                        stream.Close();
                    }

                    return bdsc;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Backup/Restore MYSQL

        public bool Backup_BD(string path_Destino)
        {
            try
            {
                using (MySqlConnection conexao = CriarConexao(false))
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        using (MySqlBackup mySqlBackup = new MySqlBackup(mySqlCommand))
                        {
                            string pathMySql_Backup = Path.Combine(path_InternoSistema, "MySql_Backup.sql");
                            
                            mySqlCommand.Connection = conexao;
                            conexao.Open();
                            mySqlBackup.ExportToFile(pathMySql_Backup);
                            conexao.Close();

                            using (ZipFile zip = new ZipFile())
                            {
                                zip.AddDirectory(path_InternoSistema);
                                zip.Save(path_Destino);
                            }

                            File.Delete(pathMySql_Backup);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Restore_BD(string path_ArquivoBackupZip)
        {
            try
            {
                ZipFile zipFile = ZipFile.Read(path_ArquivoBackupZip);
                if (zipFile.Any(arquivo => arquivo.FileName.Contains("MySql_Backup.sql")))
                {
                    using (MySqlConnection conexao = CriarConexao(false))
                    {
                        using (MySqlCommand mySqlCommand = new MySqlCommand())
                        {
                            using (MySqlBackup mySqlBackup = new MySqlBackup(mySqlCommand))
                            {
                                using (zipFile = new ZipFile(path_ArquivoBackupZip))
                                    zipFile.ExtractAll(path_InternoSistema, ExtractExistingFileAction.OverwriteSilently);
                                
                                string pathMySql_Backup = Path.Combine(path_InternoSistema, "MySql_Backup.sql");

                                mySqlCommand.Connection = conexao;
                                conexao.Open();
                                mySqlBackup.ImportFromFile(pathMySql_Backup);
                                conexao.Close();

                                File.Delete(pathMySql_Backup);
                            }
                        }
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
