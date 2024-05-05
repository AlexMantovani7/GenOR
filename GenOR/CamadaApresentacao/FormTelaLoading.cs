using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenOR
{
    public partial class FormTelaLoading : Form
    {
        public FormTelaLoading()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
        }

        public FormTelaLoading(Form formulario)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public void FecharLoading()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

            if (lb_ImagemCarregando.Image != null)
                lb_ImagemCarregando.Image.Dispose();
        }

    }
}
