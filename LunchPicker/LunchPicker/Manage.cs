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
    public partial class Manage : Form
    {
        RestaurantRepo repo;
        Form form;
        public Manage(Form parent, RestaurantRepo repo)
        {
            form = parent;
            this.repo = repo;
            InitializeComponent();
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Last Vistited");
            DataRowBuilder drb = new DataRowBuilder();
            foreach(var item in repo.GetAllRestaurants(Enums.SortMethod.Date))
            {
                var dr = new DataRow(drb);
                dt.Rows.Add()
            }
        }

        private void Manage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.Show();
            Close();
        }
    }
}
