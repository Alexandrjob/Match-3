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
        /*Какие именно различия между тем если я тут это напишу или в кнопочках/спросить у Андрея
        MenuForm MenuForm = new MenuForm();
        Playspace playspace = new Playspace();
        */
        private readonly Form _mainForm;
        private readonly Form _parent;

        public ResultForm(Form mainForm, Form parent)
        {
            _mainForm = mainForm;
            _parent = parent;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            _parent.Close();
            _parent.Dispose();
            _mainForm.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Playspace playspace = new Playspace(_mainForm);//Ссылачка на игровое пространство
            this.Close();
            this.Dispose();
            _parent.Close();
            _parent.Dispose();
            _mainForm.Visible = false;
            playspace.Show();
        }
    }
}
