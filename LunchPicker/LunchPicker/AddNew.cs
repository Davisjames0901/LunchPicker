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
    public partial class AddNew : Form
    {
        private Form parent;
        private RestaurantRepo repo;
        public AddNew(Form parent, RestaurantRepo repo)
        {
            this.parent = parent;
            this.repo = repo;
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            repo.AddRestaurant(txtNewText.Text);
            Close();
            parent.Show();
        }
    }
}
