/*
 * Created by SharpDevelop.
 * User: Akila
 * Date: 11/24/2016
 * Time: 2:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TANK02
{
	/// <summary>
	/// Description of Client.
	/// </summary>
	public class Client
	{
		public static string receivedMsg="";
		
		public static TcpListener listener=null;
		
		public static Action<string> update=null;
		
		public Client()
		{
		}
		
		public static void sendToServer(string msg)
		{
			try
			{
				using(var sender = new TcpClient("127.0.0.1", 6000))
				{
					//Convert msg to ByteArray
					var d = Encoding.UTF8.GetBytes(msg);
					
					//send the array
					sender.GetStream().Write(d, 0, d.Length);
				}
			}
			catch (Exception e)
			{
			}
		}
		
		public static void startListening()
		{
			while(true)
			{
				try
				{	
					if(listener==null)
					{
						listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
						listener.Start();
					}
					
					//wait for msg from the server
					using(var ns = listener.AcceptTcpClient().GetStream())
					{
						//server msg to String
						using(var memStream = new MemoryStream())
						{
							ns.CopyTo(memStream);
							receivedMsg = Encoding.UTF8.GetString(memStream.ToArray());
						}
					}
					
					//Got the server msg
					update(receivedMsg);
					
				}
				catch (Exception e)
				{
				}
			}
		}
		
	}
}
