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
		string inp = "", outp = "", varp = "", tym = "";
		#endregion Variables
		public Form1() {InitializeComponent();	}
		private void Form1_Load(object sender, EventArgs e) {
			fx2 = (fx = (Width - 16)) / 2; fy2 = (fy = (Height - 38)) / 2;
			Text = fx + ", " + fy;
			gi = new Bitmap(fx, fy);
			gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			Parse();

			DateTime st = DateTime.Now;
			System.Diagnostics.Debug.Print("Pfact = " + NihilKri.KN.pfact(137));
			System.Diagnostics.Debug.Print("Etime = " + (DateTime.Now - st).TotalMilliseconds + "ms");
			System.Diagnostics.Debug.Flush();
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); break;
				case Keys.Enter: Parse(); break;
				case Keys.Back: if(inp.Length > 0) { inp = inp.Substring(0, inp.Length - 1); Chinp(); } break;

				case Keys.D0: inp += "0"; Chinp(); break;
				case Keys.D1: inp += "1"; Chinp(); break;
				case Keys.D2: inp += "2"; Chinp(); break;
				case Keys.D3: inp += "3"; Chinp(); break;
				case Keys.D4: inp += "4"; Chinp(); break;
				case Keys.D5: inp += "5"; Chinp(); break;
				case Keys.D6: inp += "6"; Chinp(); break;
				case Keys.D7: inp += "7"; Chinp(); break;
				case Keys.D8: inp += "8"; Chinp(); break;
				case Keys.D9: inp += "9"; Chinp(); break;
				case Keys.A: inp += "A"; Chinp(); break;
				case Keys.B: inp += "B"; Chinp(); break;
				case Keys.C: inp += "C"; Chinp(); break;
				case Keys.D: inp += "D"; Chinp(); break;
				case Keys.E: inp += "E"; Chinp(); break;
				case Keys.F: inp += "F"; Chinp(); break;
				case Keys.G: inp += "G"; Chinp(); break;
				case Keys.H: inp += "H"; Chinp(); break;
				case Keys.I: inp += "I"; Chinp(); break;
				case Keys.J: inp += "J"; Chinp(); break;
				case Keys.K: inp += "K"; Chinp(); break;
				case Keys.L: inp += "L"; Chinp(); break;
				case Keys.M: inp += "M"; Chinp(); break;
				case Keys.N: inp += "N"; Chinp(); break;
				case Keys.O: inp += "O"; Chinp(); break;
				case Keys.P: inp += "P"; Chinp(); break;
				case Keys.Q: inp += "Q"; Chinp(); break;
				case Keys.R: inp += "R"; Chinp(); break;
				case Keys.S: inp += "S"; Chinp(); break;
				case Keys.T: inp += "T"; Chinp(); break;
				case Keys.U: inp += "U"; Chinp(); break;
				case Keys.V: inp += "V"; Chinp(); break;
				case Keys.W: inp += "W"; Chinp(); break;
				case Keys.X: inp += "X"; Chinp(); break;
				case Keys.Y: inp += "Y"; Chinp(); break;
				case Keys.Z: inp += "Z"; Chinp(); break;


				default: break;
			}

		}
		private void Form1_Click(object sender, EventArgs e) {

		}
		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }

		public void Parse() {
			DateTime st = DateTime.Now;
			Draw(true);
			switch(inp) {
				case "TEST": KNTest(); break;




			}


			tym = "\nParse: " + (DateTime.Now - st).TotalMilliseconds + "; ";
			Calc();
		}
		public void Calc() {
			DateTime st = DateTime.Now;
			outp += "\n" + inp; inp = "";

			tym += "Calc: " + (DateTime.Now - st).TotalMilliseconds + "; ";
			Draw();
		}
		public void Chinp() {
			int b = 5, w = 60, lx = fx - w - 3 * b, ly = fy - w - 3 * b;
			int ox = b, oy = b, ix = b, iy = ly + 2 * b, vx = lx + 2 * b, vy = b;
			int ow = lx, oh = ly, iw = lx, ih = w, vw = w, vh = fy - 2 * b;
			gb.FillRectangle(Brushes.DarkBlue, ix, iy, iw, ih); // inp
			gb.DrawString(inp, Font, Brushes.White, ix, iy);
			gf.DrawImage(gi, 0, 0);
		}
		public void Draw(bool busy = false) {
			DateTime st = DateTime.Now;
			gb.Clear(busy ? Color.White : Color.Black);
			int b = 5, w = 60, lx = fx - w - 3 * b, ly = fy - w - 3 * b;
			int ox = b, oy = b, ix = b, iy = ly + 2 * b, vx = lx + 2 * b, vy = b;
			int ow = lx, oh = ly, iw = lx, ih = w, vw = w, vh = fy - 2 * b;
			gb.FillRectangle(Brushes.DarkRed, ox, oy, ow, oh); // outp
			gb.FillRectangle(Brushes.DarkGreen, vx, vy, vw, vh); // varp
			gb.FillRectangle(Brushes.DarkBlue, ix, iy, iw, ih); // inp

			if(!busy) {
				tym += "Draw: " + (DateTime.Now - st).TotalMilliseconds + " ms";
				outp += tym;
			}

			gb.DrawString(inp, Font, Brushes.White, ix, iy);
			gb.DrawString(outp, Font, Brushes.White, ox, oy);
			gb.DrawString(varp, Font, Brushes.White, vx, vy);


			gf.DrawImage(gi, 0, 0);
		}


		public void KNTest() {
			int l = 0;
			int a = 60128, b = 6128, c = 0;
			byte ba = 64, bb = 64, bc = 0;
			short sa = 64, sb = 64, sc = 0;
			long la = a, lb = b, lc = c;
			double da = a / 3.0, db = b / 3.0, dc = c;
			i256 ia = a, ib = b, ic = c;
			byte[] byt = new byte[32]; byt[31] = 15;
			System.Numerics.BigInteger Ia = new System.Numerics.BigInteger(byt), Ib = new System.Numerics.BigInteger(byt), Ic = new System.Numerics.BigInteger(byt);
			System.Numerics.Complex ca = a, cb = b, cc = c;
			Complex ka = a, kb = b, kc = c;
			DateTime st;

			outp += "\nKNTest:"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { c = a + b; }
			//gb.DrawString("int + int: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 6, l * 12 + 6); l++;
			outp += "\nint + int: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { bc = (byte)(ba + bb); }
			outp += "\nbyte+byte: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { sc = (short)(sa + sb); }
			outp += "\nshrt+shrt: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { lc = la + lb; }
			outp += "\nlong+long: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { dc = da + db; }
			outp += "\ndbl + dbl: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { cc = ca + cb; }
			outp += "\nSabi+Sabi: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000000 ; q++) { kc = ka + kb; }
			outp += "\nKabi+Kabi: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;

			l++;

			for(int q = 0 ; q < 100000 ; q++) { ic = ia + ib; }
			outp += "\ni256+i256: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			for(int q = 0 ; q < 100000 ; q++) { Ic = Ia + Ib; }
			outp += "\nBigI+BigI: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);

			l++;

			st = DateTime.Now;
			//cc = new System.Numerics.Complex(0, 0); 
			for(int y=0 ; y < 1024 ; y++) {
				for(int x=0 ; x < 1024 ; x++) {
					//cb = new System.Numerics.Complex(-x, -y) + cc; 
					ca = System.Numerics.Complex.Log(new System.Numerics.Complex(x, y)); c = this.c(ca);
				}
			}
			outp += "\nS1024: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);
			st = DateTime.Now;
			//kc = new Complex(0, 0);
			for(int y=0 ; y < 1024 ; y++) {
				for(int x=0 ; x < 1024 ; x++) {
					//kb = new Complex(-x, -y) + kc;
					ka = (new Complex(x, y)).ln(); c = ka.c;
				}
			}
			outp += "\nK1024: " + (DateTime.Now - st).TotalMilliseconds + " ms"; Draw(true);

			//gb.DrawString(ia.ToString() + "\n" + ia.ToString(true), Font, Brushes.White, 20, 20);
			//gb.DrawString(ib.ToString() + "\n" + ib.ToString(true), Font, Brushes.White, 300, 20);
			//gb.DrawString(((int)(ia + ib+7000)).ToString() + "\n" + (ia + ib+7000).ToString() + "\n" + (ia + ib).ToString(true), Font, Brushes.White, 200, 230);


		}
			double tau = 2*Math.PI;
			public int c(System.Numerics.Complex n) {
				double ca = 1.0, cr = 0.0, cg = 0.0, cb = 0.0;
				double _t = n.Phase, _r = n.Magnitude;
				double h = (_t + tau) % tau, l = 0.0, d = 0.0;
				//l = ((_r - 1.0) % 1.0 + 1.0) / 1.0;
				//l = ((_r - 0.5) % 0.5 + 0.5) / 1.0;
				//l = ((_r - pi/2.0) % (pi/2.0) + pi/2.0) / pi;
				//l = ((_r - pi) % pi + pi) / tau;
				//d = 0.0;
				if(_r > 16.0) return ~0xFFFFFF + 0xFFFFFF;
				l = _r % 1.0;
				if(Math.Abs(_r) < 1.0 / 128.0) return ~0xFFFFFF; // 0
				if(Math.Abs(_r - Math.Round(_r)) < 1.0 / 128.0) return ~0xFFFFFF + 0xFFFFFF; // Isolines
				//if(h > pi)
				l = Math.Min(_r, 1.0);
				d = Math.Floor(_r) / 255.0;
				double H = h * 6.0 / tau; double C = l;// 1.0 - Math.Abs(2.0 * l - 1.0); 
				double X = C * (1.0 - Math.Abs((H % 2.0) - 1.0)), m = l - C;// / 2;
				switch((int)Math.Floor(H)) {
					case 0: cr = C; cg = X; cb = d; break;
					case 1: cr = X; cg = C; cb = d; break;
					case 2: cr = d; cg = C; cb = X; break;
					case 3: cr = d; cg = X; cb = C; break;
					case 4: cr = X; cg = d; cb = C; break;
					case 5: cr = C; cg = d; cb = X; break;
				} return ~0xFFFFFF +
					((int)Math.Floor((cr + m) * 255.0) << 16) +
					((int)Math.Floor((cg + m) * 255.0) << 8) +
					 (int)Math.Floor((cb + m) * 255.0);

			}

	}
}
