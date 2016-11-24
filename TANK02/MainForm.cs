/*
 * Created by SharpDevelop.
 * User: Akila
 * Date: 11/24/2016
 * Time: 2:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TANK02
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Thread listeningThread;
		
		System.Windows.Forms.PictureBox[][] pBox;
		
		int tankLocX;
		int tankLocY;
		int tankDirec;
		
		string [] directions = new string[] {"../../img/0.bmp", "../../img/1.bmp", "../../img/2.bmp", "../../img/3.bmp"};
			
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			listeningThread = new Thread(Client.startListening);
			
			pBox = new System.Windows.Forms.PictureBox[][] { new System.Windows.Forms.PictureBox[] {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10}, new System.Windows.Forms.PictureBox[] {p11, p12, p13, p14, p15, p16, p17, p18, p19, p20}, new System.Windows.Forms.PictureBox[] {p21, p22, p23, p24, p25, p26, p27, p28, p29, p30}, new System.Windows.Forms.PictureBox[] {p31, p32, p33, p34, p35, p36, p37, p38, p39, p40}, new System.Windows.Forms.PictureBox[] {p41, p42, p43, p44, p45, p46, p47, p48, p49, p50}, new System.Windows.Forms.PictureBox[] {p51, p52, p53, p54, p55, p56, p57, p58, p59, p60}, new System.Windows.Forms.PictureBox[] {p61, p62, p63, p64, p65, p66, p67, p68, p69, p70}, new System.Windows.Forms.PictureBox[] {p71, p72, p73, p74, p75, p76, p77, p78, p79, p80}, new System.Windows.Forms.PictureBox[] {p81, p82, p83, p84, p85, p86, p87, p88, p89, p90}, new System.Windows.Forms.PictureBox[] {p91, p92, p93, p94, p95, p96, p97, p98, p99, p100}};

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
		}
		
		void update(string msgReceived)
        {
            if (GUIUpdater.InvokeRequired)
                GUIUpdater.Invoke(new Action(() => update(msgReceived)));
            else
            {
                GUIUpdater.Text = msgReceived;
                
                //decode the msg
                if (msgReceived[0]=='I')
                {
                	string msg = msgReceived.Substring(5);
                	string[] categ = msg.Split(':');
                	
                	//locate tank
                	tankLocX = 0;
                	tankLocY = 0;
                	tankDirec = 0;
  
                	pBox[tankLocY][tankLocX].Image = Image.FromFile(@directions[tankDirec]);
          
                	//Bricks 
                	foreach (string coordinate in categ[0].Split(';'))
			        {
                		string[] xy = coordinate.Split(',');
                		int x = (int) Int32.Parse(xy[0]);
                		int y = (int) Int32.Parse(xy[1]);
                		pBox[y][x].BackColor = Color.SandyBrown;
			        }
                	
                	//Stone  
                	foreach (string coordinate in categ[1].Split(';'))
			        {
                		string[] xy = coordinate.Split(',');
                		int x = (int) Int32.Parse(xy[0]);
                		int y = (int) Int32.Parse(xy[1]);
                		pBox[y][x].BackColor = Color.DimGray;
			        }
                	
                	//Water  
                	foreach (string coordinate in categ[2].Substring(0, categ[2].Length-1).Split(';'))
			        {
                		string[] xy = coordinate.Split(',');
                		int x = (int) Int32.Parse(xy[0]);
                		int y = (int) Int32.Parse(xy[1]);
                		pBox[y][x].BackColor = Color.DeepSkyBlue;
			        }
                }
                else if(msgReceived[0]=='G')
                {
                	string msg = msgReceived.Substring(2);
                	string[] categ = msg.Split(':');
                	
                	foreach (string playerDetails in categ)
			        {
                		if(playerDetails.Substring(0, 2)=="P0")
                		{
                			string[] xy = playerDetails.Substring(3).Split(';');
                			
                			//geting the location
                			string [] loc = xy[0].Split(',');
                			int x = (int) Int32.Parse(loc[0]);
                			int y = (int) Int32.Parse(loc[1]);
                			
                			//getting the direction
                			int direc = (int) Int32.Parse(xy[1]);
                			
							//Reset the previous location
							pBox[tankLocY][tankLocX].Image = null;
                			
                			tankLocX = x;
                			tankLocY = y;
                			tankDirec = direc;
                			
                			pBox[tankLocY][tankLocX].Image = Image.FromFile(@directions[tankDirec]);
                		}
			        }
                }
            }
        }
	
		void JoinClick(object sender, EventArgs e)
		{
			Client.update = update;
			
			listeningThread.Start();
			
			Client.sendToServer("JOIN#");
		}
		
		void RightClick(object sender, EventArgs e)
		{
			Client.sendToServer("RIGHT#");
		}
		
		void LeftClick(object sender, EventArgs e)
		{
			Client.sendToServer("LEFT#");
		}
		
		void DownClick(object sender, EventArgs e)
		{
			Client.sendToServer("DOWN#");
		}
		
		void UpClick(object sender, EventArgs e)
		{
			Client.sendToServer("UP#");
		}
		
		
		void P1Click(object sender, EventArgs e)
		{
			
		}
	}
}
