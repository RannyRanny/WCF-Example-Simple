using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ServiceReference1.Service1Client client;
		public MainWindow()
		{
			InitializeComponent();
			DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
			FindCriteria find = new FindCriteria(typeof(WcfService1.IService1));
			//find.Duration = new System.TimeSpan(0, 0, 3);
			FindResponse res= discoveryClient.Find(find);
			discoveryClient.Close();
			client = new ServiceReference1.Service1Client();
			Collection<EndpointDiscoveryMetadata> services = res.Endpoints;
				if (services.Count >0)
				{
				client.Endpoint.Address = services[0].Address;
				}
				
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			client.GetString((sender as Button).Content.ToString());
		}
	}
}
