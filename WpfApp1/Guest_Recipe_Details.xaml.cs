using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Guest_Recipe_Details.xaml
	/// </summary>
	public partial class Guest_Recipe_Details : Window
	{
		string Guest_ID;
		string Recipe_ID;
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";

		public Guest_Recipe_Details(string guest_ID, string recipe_ID)
		{
			InitializeComponent();
			Guest_ID = guest_ID;
			Recipe_ID = recipe_ID;


			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();

			string query = $"select name from Recipes where ID = {Recipe_ID}";

			SqlCommand command = new SqlCommand(query, sqlCon);

			var a = command.ExecuteScalar();
			recipeName.Text = a.ToString();


			Reload_DataGrid();

			Display_Instructions();



		}

		public void Reload_DataGrid()
		{
			string query = $"select Name, ID from Ingedients where Recipe_ID = {Recipe_ID}";
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
				MessageBox.Show(ex.ToString());
			}

		}

		public void Display_Instructions()
		{
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			string query = $"select instrcution from Recipes where ID = {Recipe_ID}";

			SqlCommand command = new SqlCommand(query, sqlCon);
			instructions.Text = command.ExecuteScalar().ToString();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			new GuestMain(Guest_ID).Show();
			Close();
		}
	}
}
