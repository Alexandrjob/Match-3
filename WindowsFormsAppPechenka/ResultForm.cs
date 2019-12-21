using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    public partial class ResultForm : Form
    {
        private readonly Form _mainForm;
        private readonly Form _parent;

        public ResultForm(Form mainForm, Form parent,int gamepoint)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _parent = parent;
            labelgamepoint.Text = gamepoint.ToString();
        }

        private void GameAgainButton(object sender, EventArgs e)
        {
            Playspace playspace = new Playspace(_mainForm);//Ссылачка на игровое пространство
            this.Close();
            this.Dispose();
            _parent.Close();
            _parent.Dispose();
            _mainForm.Visible = false;
            playspace.Show();
        }

        private void ExitToMainFormButton(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            _parent.Close();
            _parent.Dispose();
            _mainForm.Visible = true;
        }
    }
}
