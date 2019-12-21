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
        public ResultForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuForm MenuForm = new MenuForm();//Ссылачка на меню
            Playspace playspace = new Playspace();//Ссылачка на игровое пространство
            this.Close();
            this.Dispose();
            playspace.Close();
            playspace.Dispose();
            MenuForm.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Playspace playspace = new Playspace();//Ссылачка на игровое пространство
            this.Close();
            this.Dispose();
            playspace.Close();
            playspace.Dispose();
            playspace.Show();
        }
    }
}
