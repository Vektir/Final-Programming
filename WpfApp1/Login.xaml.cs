using System;
using System.Collections.Generic;
using System.Data;
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
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";

		public Login()
		{
			InitializeComponent();
		}

		private void New_Account_Click(object sender, RoutedEventArgs e)
		{
			new New_Account_Creation().Show();
			Close();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			string query = $"select count(1) from Profiles where Username = '{username.Text}' and \"Password\" = '{password.Password}'";
			SqlConnection sqlCon = new SqlConnection(connection);
			try
			{
				sqlCon.Open();
				SqlCommand command = new SqlCommand(query, sqlCon);
				command.CommandType = CommandType.Text;

				int a = Convert.ToInt32(command.ExecuteScalar());

				if (a == 1)
				{
					query = $"select \"role\" from Profiles where Username = '{username.Text}'";
					command = new SqlCommand(query, sqlCon);
					command.CommandType = CommandType.Text;
					//MessageBox.Show(Convert.ToString(command.ExecuteScalar()));
					if (Convert.ToString(command.ExecuteScalar()) == "Cook")
					{
						query = $"select ID from Profiles where Username = '{username.Text}'";
						command = new SqlCommand(query, sqlCon);
						new Cook_Main_Screen(command.ExecuteScalar().ToString()).Show();
						Close();
					}
					else
					{
						query = $"select ID from Profiles where Username = '{username.Text}'";
						command = new SqlCommand(query, sqlCon);
						new GuestMain(command.ExecuteScalar().ToString()).Show();
						Close();
					}


				}
				else
				{
					MessageBox.Show("Wrong Password");
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);

			}
		}
	}
}
