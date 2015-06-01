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
			fx2 = (fx = (Width - 16)) / 2; fy2 = (fy = (Height - 38)) / 2;
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
			int b = 5, w = 120, lx = fx - w - 3 * b, ly = fy - w - 3 * b;
			gb.FillRectangle(Brushes.DarkRed, b, b, lx, ly); // outp
			gb.FillRectangle(Brushes.DarkGreen, lx + 2 * b, b, w, fy - 2 * b); // varp
			gb.FillRectangle(Brushes.DarkBlue, b, ly + 2 * b, lx, w); // inp

			//KNTest();
			Text = fx + ", " + fy;


			gf.DrawImage(gi, 0, 0);
		}


		public void KNTest() {
			int a = 60128, b = 6128, c = 0;
			i256 ia = a, ib = b, ic = c;
			System.Numerics.BigInteger ba = a, bb = b, bc = c;
			System.Numerics.Complex ca = a, cb = b, cc = c;
			Complex ka = a, kb = b, kc = c;
			DateTime st = DateTime.Now;
			for(int q = 0 ; q < 1000000 ; q++) { c = a + b; }
			gb.DrawString("int + int: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 0, 0);
			st = DateTime.Now;
			for(int q = 0 ; q < 1000000 ; q++) { ic = ia + ib; }
			gb.DrawString("i256+i256: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 0, 12);
			st = DateTime.Now;
			for(int q = 0 ; q < 1000000 ; q++) { bc = ba + bb; }
			gb.DrawString("BigI+BigI: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 0, 24);
			st = DateTime.Now;
			for(int q = 0 ; q < 1000000 ; q++) { cc = ca + cb; }
			gb.DrawString("Sabi+Sabi: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 0, 36);
			st = DateTime.Now;
			for(int q = 0 ; q < 1000000 ; q++) { kc = ka + kb; }
			gb.DrawString("Kabi+Kabi: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 0, 48);



			//gb.DrawString(ia.ToString() + "\n" + ia.ToString(true), Font, Brushes.White, 20, 20);
			//gb.DrawString(ib.ToString() + "\n" + ib.ToString(true), Font, Brushes.White, 300, 20);
			//gb.DrawString(((int)(ia + ib+7000)).ToString() + "\n" + (ia + ib+7000).ToString() + "\n" + (ia + ib).ToString(true), Font, Brushes.White, 200, 230);


		}
	}
}
