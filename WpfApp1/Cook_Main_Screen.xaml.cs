using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Cook_Main_Screen.xaml
	/// </summary>
	public partial class Cook_Main_Screen : Window
	{
		string Cook_ID;
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";

		public Cook_Main_Screen(string Cook_ID)
		{
			InitializeComponent();
			this.Cook_ID = Cook_ID;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//opens recipes,where the id is 
			new Cook_My_Recipes(Cook_ID).Show();
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();
			string query = $"insert into Recipes ([name], Cook_ID) values('{newRecipeName.Text}', {Cook_ID})";

			SqlCommand command = new SqlCommand(query, sqlCon);
			command.ExecuteNonQuery();

			query = $"select ID from Recipes where [name] = '{newRecipeName.Text}' and Cook_ID = {Cook_ID}";
			SqlCommand command2 = new SqlCommand(query, sqlCon);

			new Recipe_details(Cook_ID, command2.ExecuteScalar().ToString()).Show();
			Close();

		}

		private void newRecipeName_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}
