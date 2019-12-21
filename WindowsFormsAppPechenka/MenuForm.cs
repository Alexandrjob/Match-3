using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsAppPechenka
{
    public partial class MenuForm : Form
    {
        //Playspace playspace;
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var playspace = new Playspace();
            playspace.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var playspace = new Playspace();
            playspace.Show();
        }
    }
}
