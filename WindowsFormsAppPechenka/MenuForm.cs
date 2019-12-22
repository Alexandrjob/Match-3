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
            StartGame();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            StartGame();
        }
            

        private void StartGame()
        {
            this.Visible = false;
            var playspace = new PlayForm(this);
            playspace.Show();
        }
    }
}
