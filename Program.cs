using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//Set connection
			//var connection = new HubConnection("http://streamviewer.devint.dev-r5ead.net/Home/Details?StreamId=9334e38b-ff4f-467a-b46d-03b059c04c60&ProviderName=NCI-BRC&NameSpace=BRC01");
			var connection = new HubConnection("http://streamviewer.devint.dev-r5ead.net/signalr/hubs");
			
			//Make proxy to hub based on hub name on server
			var myHub = connection.CreateHubProxy("streamHub");
			
			myHub.On<string,string>("addNewMessageToPage", (streamDetail,msg) => {

				Console.WriteLine($"received at {DateTime.Now} got: {streamDetail} of: {msg}");
			});

			//Start connection
			connection.Start().ContinueWith(task => {
				if (task.IsFaulted)
				{
					Console.WriteLine("There was an error opening the connection:{0}",
						task.Exception.GetBaseException());
				}
				else
				{
					Console.WriteLine("Connected");
				}

			}).Wait();


			//connection.StateChanged += connection_StateChanged;

			//myHub.Invoke("Send", "HELLO World ").ContinueWith(task => {
			//	if (task.IsFaulted)
			//	{
			//		Console.WriteLine("There was an error calling send: {0}", task.Exception.GetBaseException());
			//	}
			//	else
			//	{
			//		Console.WriteLine("Send Complete.");
			//	}
			//});

			Console.ReadLine();

			connection.Stop();
		}
	}
}
