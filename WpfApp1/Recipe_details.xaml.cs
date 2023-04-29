using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Recipe_details.xaml
	/// </summary>
	public partial class Recipe_details : Window
	{
		string Person_ID;
		string Recipe_ID;
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";


		public Recipe_details(string person_ID, string recipe_ID)
		{
			//deleteButton.Background = Background = new SolidColorBrush(Colors.Red);

			InitializeComponent();
			Person_ID = person_ID;
			Recipe_ID = recipe_ID;
			Reload_DataGrid();


			//take name of the recipe
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();

			string query = $"select name from Recipes where ID = {Recipe_ID}";

			SqlCommand command = new SqlCommand(query, sqlCon);

			var a = command.ExecuteScalar();
			recipeName.Text = a.ToString();
			// neshto    

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


		private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var row = (DataRowView)dataGrid.SelectedItem;
			try
			{
				var value1 = row[1];
				string query = $"delete from Ingedients where ID = {value1.ToString()}";
				SqlConnection sqlCon = new SqlConnection(connection);
				sqlCon.Open();
				SqlCommand command = new SqlCommand(query, sqlCon);

				command.ExecuteNonQuery();

			}
			catch(System.NullReferenceException ex) {  }


			Reload_DataGrid();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(ingredientName.Text == null)
			{
				return;
			}
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();

			string query = $"insert into Ingedients ([Name], Recipe_ID) values ('{ingredientName.Text}', {Recipe_ID})";
			//try
			//{
				SqlCommand command = new SqlCommand(query, sqlCon);
				command.ExecuteNonQuery();
			Reload_DataGrid();
			//}
			// (Exception ex) { MessageBox.Show(ex.ToString()); }

		}

		public void Display_Instructions()
		{
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			string query = $"select instrcution from Recipes where ID = {Recipe_ID}";

			SqlCommand command = new SqlCommand(query, sqlCon);
			instructions.Text = command.ExecuteScalar().ToString();
		}

		private void instructions_TextChanged(object sender, TextChangedEventArgs e)
		{
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			string query = $"update Recipes set instrcution = '{instructions.Text}' where ID = {Recipe_ID}";

			SqlCommand command = new SqlCommand(query, sqlCon);
			command.ExecuteNonQuery();

			sqlCon.Close();
		}

		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			string query = $"delete from Ingedients where Recipe_ID = {Recipe_ID}; delete from Recipes where ID = {Recipe_ID}";
			SqlCommand command = new SqlCommand(query, sqlCon);
			command.ExecuteNonQuery();

			new Cook_Main_Screen(Person_ID).Show();
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			new Cook_Main_Screen(Person_ID).Show();
			Close();
		}
	}
}
