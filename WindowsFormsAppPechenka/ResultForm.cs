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
        bool isBottonAgain;

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
            CloseResultForm(true);
            //_mainForm.Visible = false;
            playspace.Show();
        }

        private void ExitToMainFormButton(object sender, EventArgs e)
        {
            CloseResultForm();
        }
        private void CloseResultForm(bool isBottonAgain = false)
        {
            if (isBottonAgain)
            {    
                this.Close();
                this.Dispose(); 
            }
            //Из за того, что закрывается Playspace обрабатывается событие в Playspace, которое показывает форму
            _parent.Close();
            _parent.Dispose();
        }

        private void ResuilForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseResultForm();
        }
    }
}
