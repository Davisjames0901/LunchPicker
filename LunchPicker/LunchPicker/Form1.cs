using LunchPicker.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunchPicker
{
    public partial class Form1 : Form
    {
        private RestaurantRepo repo;
		private int seed = 0;
		public Form1()
        {
            InitializeComponent();
            repo = new RestaurantRepo();

        }
        
        private void manageRestuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void button1_Click(object sender, EventArgs e)
		{
            var suggestion = string.Empty;
            try
            {
                suggestion = repo.GetRandom(seed);
            }
            catch 
            {
                MessageBox.Show("No Resturants!");
                return;
            }
            if(MessageBox.Show(suggestion+"?","Confirm", MessageBoxButtons.YesNo)== DialogResult.Yes)
            {
                repo.LockInWinner(suggestion);
            }
            else
            {
	            seed++;
            }
        }

		private void addNewToolStripMenuItem1_Click(object sender, EventArgs e)
		{
            var form = new AddNew(this, repo);
            Hide();
            form.Show();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repo.UpdateResturants();
            Environment.Exit(0);
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            repo.UpdateResturants();
            Environment.Exit(0);
        }
    }
}
