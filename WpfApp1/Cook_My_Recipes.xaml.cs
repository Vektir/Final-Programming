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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Cook_My_Recipes.xaml
	/// </summary>
	public partial class Cook_My_Recipes : Window
	{
		string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\viktor\\source\\repos\\Final Marrian\\WpfApp1\\Database1.mdf\";Integrated Security=True";
		string Cook_ID;


		public Cook_My_Recipes(string Cook_ID)
		{
			this.Cook_ID = Cook_ID;

			InitializeComponent();


			string query = $"select name, ID from Recipes where Cook_ID = {Cook_ID}";
			SqlConnection sqlCon = new SqlConnection(connection);
			sqlCon.Open();

			//SqlCommand command = new SqlCommand(query, sqlCon);
			//command.CommandType = CommandType.Text;


			//SqlDataReader reader = command.ExecuteReader();


			//List<string> recipes= new List<string>();
			//while( reader.Read())
			//{
			//	recipes.Add(reader[0].ToString());
			//}

			//MessageBox.Show(recipes[1]);


			DataTable dataTable = new DataTable();

			SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlCon);
			dataAdapter.Fill(dataTable);

			dataGrid.ItemsSource = dataTable.DefaultView;



			//MessageBox.Show((string)command.ex());
			//foreach (var i in recipes)
			//	Combo_Names.Items.Add(i);

		}

		private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var row = (DataRowView)dataGrid.SelectedItem;
			var value1 = row[1];

            //System.Windows.MessageBox.Show(value1.ToString());

			new Recipe_details(Cook_ID, value1.ToString()).Show();
			Close();

		}
	}
}
