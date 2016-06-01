using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			BasicHttpBinding tcpBinding =
			   new BasicHttpBinding();

			string locAddr = "";
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					locAddr = ip.ToString();
				}
			}
			ServiceHost server = new ServiceHost(typeof(WcfService1.Service1), new Uri("http://" + locAddr));
			server.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
			server.Description.Endpoints.Add(new UdpDiscoveryEndpoint());

			server.AddServiceEndpoint(typeof(WcfService1.IService1), tcpBinding, "http://" + locAddr);
			server.Open();
			Console.ReadLine();
		}
	}
}
