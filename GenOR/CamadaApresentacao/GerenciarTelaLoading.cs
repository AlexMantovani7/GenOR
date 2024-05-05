using System;
using System.Threading;
using System.Windows.Forms;

namespace GenOR
{
    public class GerenciarTelaLoading
    {
        private FormTelaLoading formTelaLoading;
        private Thread carregarThread;

        public void Abrir(Form formAtual, FormTelaLoading formTL, Thread thread)
        {
            try
            {
                formTelaLoading = formTL;
                carregarThread = thread;

                carregarThread = new Thread(new ParameterizedThreadStart(ProcessoCarragamento));
                carregarThread.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ProcessoCarragamento(object formulario)
        {
            try
            {
                Form novoFormulario = formulario as Form;

                formTelaLoading = new FormTelaLoading(novoFormulario);
                formTelaLoading.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Fechar()
        {
            try
            {
                Thread.Sleep(50);
                formTelaLoading.BeginInvoke(new ThreadStart(formTelaLoading.FecharLoading));
                formTelaLoading = null;
                carregarThread = null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
