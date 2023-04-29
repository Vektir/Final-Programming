using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for GuestMain.xaml
	/// </summary>
	public partial class GuestMain : Window
	{
		string Guest_ID;
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";


		public GuestMain(string guest_ID)
		{
			InitializeComponent();
			Guest_ID = guest_ID;



			string query = "select [name],Username, Recipes.ID\r\nfrom Recipes\r\nLeft Join Profiles on Recipes.Cook_ID = Profiles.ID";
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			try
			{
				DataTable dataTable = new DataTable();

				SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlCon);
				dataAdapter.Fill(dataTable);

				dataGrid.ItemsSource = dataTable.DefaultView;

			}
			catch (Exception ex)
			{
                System.Windows.MessageBox.Show(ex.ToString());
			}
		}

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var row = (DataRowView)dataGrid.SelectedItem;
			var value1 = row[2];



			new Guest_Recipe_Details(Guest_ID, value1.ToString()).Show();
			Close();
		}
	}
}
