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
using Microsoft.Data.SqlClient;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for New_Account_Creation.xaml
	/// </summary>
	public partial class New_Account_Creation : Window
	{
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";
		public New_Account_Creation()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string query = $"insert into Profiles (Username,\"Password\", \"Role\") values ('{username.Text}', '{password.Password}', 'User')";
			SqlConnection sqlCon = new SqlConnection(connection);
			try
			{
				sqlCon.Open();
				SqlCommand command = new SqlCommand(query, sqlCon);
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);

			}


		}
	}
}
