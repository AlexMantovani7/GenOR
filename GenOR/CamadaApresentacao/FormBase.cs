using GenOR.Properties;
using CamadaObjetoTransferencia;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
/*using System.Reflection;*/ /*P. DESENVOLVIMENTO*/
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormBase : Form
    {

        #region Instanciamento Rápido de Objetos

        public void InstanciamentoRapida_Material(Material material)
        {
            try
            {
                material.Unidade = new Grupo_Unidade();
                material.Grupo = new Grupo_Unidade();
                material.Fornecedor = new Pessoa();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InstanciamentoRapida_ProdutoServico(Produto_Servico produto_Servico)
        {
            try
            {
                produto_Servico.Unidade = new Grupo_Unidade();
                produto_Servico.Grupo = new Grupo_Unidade();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void InstanciamentoRapida_Materiais_ProdutoServico(Materiais_Produto_Servico materiais_ProdutoServico)
        {
            try
            {
                materiais_ProdutoServico.Material = new Material();
                InstanciamentoRapida_Material(materiais_ProdutoServico.Material);
                
                materiais_ProdutoServico.Produto_Servico = new Produto_Servico();
                InstanciamentoRapida_ProdutoServico(materiais_ProdutoServico.Produto_Servico);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void InstanciamentoRapida_Orcamento(Orcamento orcamento)
        {
            try
            {
                orcamento.Usuario = new Pessoa();
                orcamento.Cliente = new Pessoa();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InstanciamentoRapida_ProdutosServicos_Orcamento(Produtos_Servicos_Orcamento produtosServicos_Orcamento)
        {
            try
            {
                produtosServicos_Orcamento.Produto_Servico = new Produto_Servico();
                InstanciamentoRapida_ProdutoServico(produtosServicos_Orcamento.Produto_Servico);
                
                produtosServicos_Orcamento.Orcamento = new Orcamento();
                InstanciamentoRapida_Orcamento(produtosServicos_Orcamento.Orcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InstanciamentoRapida_Materiais_Orcamento(Materiais_Orcamento materiais_Orcamento)
        {
            try
            {
                materiais_Orcamento.Produtos_Servicos_Orcamento = new Produtos_Servicos_Orcamento();
                InstanciamentoRapida_ProdutosServicos_Orcamento(materiais_Orcamento.Produtos_Servicos_Orcamento);
                
                materiais_Orcamento.Materiais_Produto_Servico = new Materiais_Produto_Servico();
                InstanciamentoRapida_Materiais_ProdutoServico(materiais_Orcamento.Materiais_Produto_Servico);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Localização de Arquivos/Imagens
        
        public string Imagem_ZoomIn_ZoomOut(string imagem_ButtomZoom, Button btn_ZoomImagem, Button btn_ReverterImagem, PictureBox pb_Imagem)
        {
            try
            {
                if (imagem_ButtomZoom.Equals("ZOOM-IN"))
                {
                    pb_Imagem.Location = new Point(Convert.ToInt32(pb_Imagem.Location.X / 2), Convert.ToInt32(pb_Imagem.Location.Y / 7));
                    pb_Imagem.Size = new Size(Convert.ToInt32(pb_Imagem.Size.Width * 4), Convert.ToInt32(pb_Imagem.Size.Height * 3));

                    btn_ZoomImagem.Location = new Point(pb_Imagem.Location.X, pb_Imagem.Location.Y);
                    btn_ZoomImagem.Size = new Size(Convert.ToInt32(btn_ZoomImagem.Size.Width * 2), Convert.ToInt32(btn_ZoomImagem.Size.Height * 2));

                    btn_ReverterImagem.Size = new Size(Convert.ToInt32(btn_ReverterImagem.Size.Width * 2), Convert.ToInt32(btn_ReverterImagem.Size.Height * 2));
                    btn_ReverterImagem.Location = new Point((pb_Imagem.Size.Width + 12) - btn_ReverterImagem.Size.Width, pb_Imagem.Location.Y);

                    btn_ZoomImagem.BackgroundImage = Resources.Zoom_Out;
                    imagem_ButtomZoom = "ZOOM-OUT";
                }
                else if (imagem_ButtomZoom.Equals("ZOOM-OUT"))
                {
                    pb_Imagem.Location = new Point(Convert.ToInt32(pb_Imagem.Location.X * 2), Convert.ToInt32(pb_Imagem.Location.Y * 7));
                    pb_Imagem.Size = new Size(Convert.ToInt32(pb_Imagem.Size.Width / 4), Convert.ToInt32(pb_Imagem.Size.Height / 3));

                    btn_ZoomImagem.Location = new Point(pb_Imagem.Location.X, pb_Imagem.Location.Y);
                    btn_ZoomImagem.Size = new Size(Convert.ToInt32(btn_ZoomImagem.Size.Width / 2), Convert.ToInt32(btn_ZoomImagem.Size.Height / 2));

                    btn_ReverterImagem.Size = new Size(Convert.ToInt32(btn_ReverterImagem.Size.Width / 2), Convert.ToInt32(btn_ReverterImagem.Size.Height / 2));
                    btn_ReverterImagem.Location = new Point(pb_Imagem.Size.Width, pb_Imagem.Location.Y);

                    btn_ZoomImagem.BackgroundImage = Resources.Zoom_In;
                    imagem_ButtomZoom = "ZOOM-IN";
                }

                return imagem_ButtomZoom;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool ValidarImagem_OpenFile(string patchImagem)
        {
            try
            {
                if (patchImagem.Contains(".png") || patchImagem.Contains(".jpeg") || patchImagem.Contains(".jpg"))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Localizar_Imagem_Documento(string nomeArquivo, bool arquivoImagem)
        {
            try
            {
                if (arquivoImagem)
                    return Arquivo_Imagem(nomeArquivo);
                else
                    return Arquivo_Documento(nomeArquivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string Arquivo_Documento(string nomeArquivo)
        {
            try
            {
                return Path.GetFullPath(Path.Combine(Application.StartupPath, @"Resources\Documentos\" + nomeArquivo)); /*P. DESENVOLVIMENTO*/ /*Assembly.GetExecutingAssembly().Location + @"\..\..\..\Resources\Documentos\" + nomeArquivo);*/
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string Arquivo_Imagem(string nomeArquivo)
        {
            try
            {
                return Path.GetFullPath(Path.Combine(Application.StartupPath, @"Resources\Imagens\" + nomeArquivo)); /*P. DESENVOLVIMENTO*/ /*Assembly.GetExecutingAssembly().Location + @"\..\..\..\Resources\Imagens\" + nomeArquivo);*/
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Gerenciamento DataGridView

        public int MudarLinha_DGV(DataGridView dataGridView, bool avançarLinha)
        {
            try
            {
                int linha = 0;
                if (avançarLinha)
                    linha = dataGridView.CurrentRow.Index + 1;
                else
                    linha = dataGridView.CurrentRow.Index - 1;
                
                return linha;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ImagemDGV_RightClick(PictureBox pb_ExibindoImagemDGV, Button btn_FecharImagemDGV, PictureBox pb_ImagemReferencia, Point localizacao_Inicial)
        {
            try
            {
                pb_ExibindoImagemDGV.Size = new Size(pb_ImagemReferencia.Width * 2, pb_ImagemReferencia.Height * 2);

                pb_ExibindoImagemDGV.Location = new Point(localizacao_Inicial.X, localizacao_Inicial.Y);
                btn_FecharImagemDGV.Location = new Point(localizacao_Inicial.X, localizacao_Inicial.Y);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Gerenciamento de Campos
        
        public void PintarBackground_CampoValido_Invalido(TextBox textBox, bool campoValido, bool limparCampo)
        {
            try
            {
                if (campoValido)
                    textBox.BackColor = Color.White;
                else
                    textBox.BackColor = Color.RosyBrown;

                if (limparCampo)
                    textBox.Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CampoMoeda_KeyPress(object sender, KeyPressEventArgs e, bool formatacao_NumeroInteiro)
        {
            try
            {
                if (sender.ToString().Contains("/"))
                {
                    if (char.IsDigit(e.KeyChar) && !e.KeyChar.Equals((char)Keys.Back))
                        e.Handled = true;
                }
                else
                {
                    TextBox campoMoeda = (TextBox)sender;
                    
                    if (formatacao_NumeroInteiro.Equals(true))
                    {
                        if (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals((char)Keys.Back))
                            e.Handled = true;
                    }
                    else
                    {
                        if (char.IsDigit(e.KeyChar) && campoMoeda.TextLength.Equals(13) && !campoMoeda.Text.Contains(','))
                        {
                            campoMoeda.Text = campoMoeda.Text + ",";
                            campoMoeda.Select(campoMoeda.Text.Length, 0);
                        }

                        if (char.IsDigit(e.KeyChar) && campoMoeda.TextLength > 13 && campoMoeda.Text.Contains(',') && campoMoeda.SelectedText.Trim().IndexOf(",").Equals(0))
                        {
                            int espaçamento = campoMoeda.SelectionLength;
                            campoMoeda.Text = campoMoeda.Text.Remove(campoMoeda.SelectionStart, espaçamento);

                            campoMoeda.Text = campoMoeda.Text + ",";
                            campoMoeda.Select(campoMoeda.Text.Length, 0);
                        }

                        if (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
                        {
                            if (e.KeyChar.Equals(','))
                                e.Handled = (campoMoeda.Text.Contains(','));
                            else
                                e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void CampoMoeda_FormatacaoDecimal(TextBox campoMoeda, bool formatacao_Dinheiro)
        {
            try
            {
                if (campoMoeda.Text.Trim().Equals("") || campoMoeda.Text.Trim().Equals(","))
                    campoMoeda.Text = "0";

                if (formatacao_Dinheiro)
                    campoMoeda.Text = decimal.Parse(campoMoeda.Text).ToString("C2", new CultureInfo("pt-BR"));
                else
                    campoMoeda.Text = decimal.Parse(campoMoeda.Text).ToString("N2", new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal CampoMoeda_RemoverFormatacao(TextBox campoMoeda)
        {
            try
            {
                string modificarTexto = campoMoeda.Text.Trim();

                modificarTexto = modificarTexto.Replace("R$", "").Trim();
                modificarTexto = modificarTexto.Replace(".", "").Trim();
                modificarTexto = modificarTexto.Replace(" %", "").Trim();
                modificarTexto = modificarTexto.Replace("/", "").Trim();
                modificarTexto = modificarTexto.Replace("-", "").Trim();
                modificarTexto = modificarTexto.Replace("_", "").Trim();
                modificarTexto = modificarTexto.Replace("(", "").Trim();
                modificarTexto = modificarTexto.Replace(")", "").Trim();

                campoMoeda.Text = modificarTexto.Trim();

                return decimal.Parse(campoMoeda.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CampoMaskedTxt_RemoverFormatacao(MaskedTextBox campoMoeda)
        {
            try
            {
                string modificarTexto = campoMoeda.Text.Trim();

                modificarTexto = modificarTexto.Replace("R$", "").Trim();
                modificarTexto = modificarTexto.Replace(".", "").Trim();
                modificarTexto = modificarTexto.Replace(" %", "").Trim();
                modificarTexto = modificarTexto.Replace("/", "").Trim();
                modificarTexto = modificarTexto.Replace("-", "").Trim();
                modificarTexto = modificarTexto.Replace("_", "").Trim();
                modificarTexto = modificarTexto.Replace("(", "").Trim();
                modificarTexto = modificarTexto.Replace(")", "").Trim();

                return modificarTexto.Trim();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CampoFormatacao_Porcentagem(TextBox campoMoeda)
        {
            try
            {
                campoMoeda.Text = campoMoeda.Text + " %";
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void Campo_PreenchimentoObrigatorio(Label campoLabel, string tipagemCampo)
        {
            try
            {
                ToolTip mensagemMouseEmcima = new ToolTip();

                if (tipagemCampo.Equals("numerico"))
                    mensagemMouseEmcima.SetToolTip(campoLabel, "CAMPO OBRIGATÓRIO: O campo não pode conter um valor zerado, negativo ou em branco !");
                else if (tipagemCampo.Equals("texto"))
                    mensagemMouseEmcima.SetToolTip(campoLabel, "CAMPO OBRIGATÓRIO: O campo não pode estar em branco !");
                else if (tipagemCampo.Equals("outros"))
                    mensagemMouseEmcima.SetToolTip(campoLabel, "CAMPO OBRIGATÓRIO !");
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool Enter_FocusButton(Button botaoFoco, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)Keys.Enter))
                {
                    botaoFoco.Focus();
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

        public bool Enter_FocusTxtb(TextBox txtbFoco, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)Keys.Enter))
                {
                    txtbFoco.Focus();
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

        public bool Enter_FocusMaskedTxtb(MaskedTextBox mtxtbFoco, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)Keys.Enter))
                {
                    mtxtbFoco.Focus();
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
        
        public void DefinirEixoXY_Calendario(Button botaoCalendario, MonthCalendar calendario)
        {
            try
            {
                int localizacao_X = (botaoCalendario.Location.X + botaoCalendario.Size.Width) - 4;
                int localizacao_Y = (botaoCalendario.Location.Y + botaoCalendario.Size.Height) - 4;
                
                calendario.Location = new Point(localizacao_X, localizacao_Y);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Gerenciamento Geral Formulario
        
        public void AlterarLogo_ModoPesquisa(Label lb_LogoJanela, string textoLogo, int tamanhoFonte)
        {
            try
            {
                lb_LogoJanela.Text = textoLogo;
                lb_LogoJanela.Font = new Font("Microsoft Tai Le", tamanhoFonte, FontStyle.Bold);
                lb_LogoJanela.ForeColor = Color.MidnightBlue;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void Gerenciamento_FormVisivel(bool visivel)
        {
            try
            {
                if (visivel)
                {
                    this.Visible = visivel;
                    this.Focus();
                }
                else
                    this.Visible = visivel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Formulario_ModoModificacao(GroupBox gb_ListagemGeral, GroupBox gb_DadosGerais, TabControl tbc_Principal, TabControl tbc_Secundario,
            Button btn_Cadastrar, Button btn_Alterar, Button btn_Deletar, Button btn_Imprimir, Button btn_VoltarGrid, Button btn_Pesquisar, Button btn_AvancarGrid,
            Button btn_RetornarModoPesquisa)
        {
            try
            {
                int index_PaginaPrincipal = 1;
                int index_PaginaSecundario = 0;

                gb_ListagemGeral.Enabled = false;
                gb_DadosGerais.Enabled = true;

                tbc_Principal.SelectTab(index_PaginaPrincipal);
                tbc_Secundario.SelectTab(index_PaginaSecundario);

                btn_Cadastrar.Enabled = false;
                btn_Alterar.Enabled = false;
                btn_Deletar.Enabled = false;
                btn_Imprimir.Enabled = false;
                btn_VoltarGrid.Enabled = false;
                btn_Pesquisar.Enabled = false;
                btn_AvancarGrid.Enabled = false;
                btn_RetornarModoPesquisa.Enabled = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Formulario_InativarAbaSecundaria(TabControl tbc_Secundario, TabPage tbp_Secundaria)
        {
            try
            {
                tbc_Secundario.TabPages.Remove(tbp_Secundaria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Formulario_ModoConsulta(GroupBox gb_ListagemGeral, GroupBox gb_DadosGerais, DataGridView dgv_Geral,
            Button btn_Cadastrar, Button btn_Alterar, Button btn_Deletar, Button btn_Imprimir, Button btn_VoltarGrid, Button btn_Pesquisar, Button btn_AvancarGrid, Button btn_RetornarModoPesquisa)
        {
            try
            {
                gb_ListagemGeral.Enabled = true;
                gb_DadosGerais.Enabled = false;

                btn_Cadastrar.Enabled = true;
                btn_Alterar.Enabled = true;
                btn_Deletar.Enabled = true;
                btn_Imprimir.Enabled = true;
                btn_VoltarGrid.Enabled = true;
                btn_Pesquisar.Enabled = true;
                btn_AvancarGrid.Enabled = true;
                btn_RetornarModoPesquisa.Enabled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Formulario_AtivarAbaSecundaria(TabControl tbc_Secundario, TabPage tbp_Secundaria, int indexPosicao)
        {
            try
            {
                tbc_Secundario.TabPages.Insert(indexPosicao, tbp_Secundaria);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void GerenciarBotoes_ListagemGeral(bool ativaInativa, Button btn_DadosGerais_Alterar, Button btn_DadosGerais_Deletar, Button btn_DadosGerais_Imprimir, Button btn_DadosGerais_AvancarGrid,
            Button btn_DadosGerais_Pesquisar, Button btn_DadosGerais_VoltarGrid)
        {
            try
            {
                btn_DadosGerais_Alterar.Enabled = ativaInativa;
                btn_DadosGerais_Deletar.Enabled = ativaInativa;
                btn_DadosGerais_Imprimir.Enabled = ativaInativa;
                btn_DadosGerais_AvancarGrid.Enabled = ativaInativa;
                btn_DadosGerais_Pesquisar.Enabled = ativaInativa;
                btn_DadosGerais_VoltarGrid.Enabled = ativaInativa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void GerenciarBotoes_ListagemSecundaria(bool ativaInativa, bool apenasCadastro, Button btn_DadosGerais_Cadastrar, Button btn_DadosGerais_Alterar, Button btn_DadosGerais_Deletar, Button btn_DadosGerais_Imprimir)
        {
            try
            {
                if (apenasCadastro)
                    btn_DadosGerais_Cadastrar.Enabled = true;
                else
                    btn_DadosGerais_Cadastrar.Enabled = ativaInativa;
                
                btn_DadosGerais_Alterar.Enabled = ativaInativa;
                btn_DadosGerais_Deletar.Enabled = ativaInativa;
                btn_DadosGerais_Imprimir.Enabled = ativaInativa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

    }
}
