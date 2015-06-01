using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NihilKri;

namespace Calculator {
	public partial class Form1 : Form {
		#region Variables
		#region Graphics
		static Graphics gb,gf; static Bitmap gi;
		static int fx, fy, fx2, fy2;
		#endregion Graphics
		string inp = "", outp = "";
		#endregion Variables
		public Form1() {InitializeComponent();	}
		private void Form1_Load(object sender, EventArgs e) {
			fx2 = (fx = Width) / 2; fy2 = (fy = Height) / 2;
			gi = new Bitmap(fx, fy);
			gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			Calc();

			DateTime st = DateTime.Now;
			System.Diagnostics.Debug.Print("Pfact = " + NihilKri.KN.pfact(137));
			System.Diagnostics.Debug.Print("Etime = " + (DateTime.Now - st).TotalMilliseconds + "ms");
			System.Diagnostics.Debug.Flush();
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); break;
				case Keys.Enter: Calc(); break;

				default: break;
			}

		}
		private void Form1_Click(object sender, EventArgs e) {

		}
		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }

		public void Calc() {
			outp += "\n" + inp; inp = "";

			Draw();
		}
		public void Draw() {
			gb.Clear(Color.Black);
			gb.DrawString("Test:\n" + KN.Test(), Font, Brushes.White, 20, 20);



			gf.DrawImage(gi, 0, 0);
		}



	}
}
