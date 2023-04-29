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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Cook_Main_Screen.xaml
	/// </summary>
	public partial class Cook_Main_Screen : Window
	{
		string Cook_ID;
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

		}
	}
}
