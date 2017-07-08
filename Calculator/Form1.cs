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
		static Graphics gb, gf; static Bitmap gi;
		static int fx, fy, fx2, fy2;
		/// <summary>
		/// The rectangle of the status box
		/// </summary>
		Rectangle rects;
		/// <summary>
		/// The rectangle of the input box
		/// </summary>
		Rectangle recti;
		/// <summary>
		/// The rectangle of the output box
		/// </summary>
		Rectangle recto;
		/// <summary>
		/// The rectangle of the variable box
		/// </summary>
		Rectangle rectv;
		/// <summary>
		/// The rectangle of the graphing box, unimplemented
		/// </summary>
		Rectangle rectg;
		#endregion Graphics
		#region IO Strings
		/// <summary>
		/// The status box text, used to show calculation status and mode selection
		/// </summary>
		string txtsta = "";
		/// <summary>
		/// The input box text, pressing enter will send this to the parser
		/// </summary>
		string txtinp = "";
		/// <summary>
		/// The output box text, where everything is stored
		/// </summary>
		string txtout = "";
		/// <summary>
		/// The variable box text, unimplemented
		/// </summary>
		string txtvar = "";
		/// <summary>
		/// The string that holds the "Parse: Calc: Draw: " timings for every calculations before it's pushed to txtout
		/// </summary>
		string txttym = "";
		/// <summary>
		/// The answer from the previous calculation
		/// </summary>
		string txtans = "";
		/// <summary>
		/// Input position, used for input text editing
		/// </summary>
		int inpp = 0;
		/// <summary>
		/// Selection position, used for selecting text
		/// </summary>
		int selp = 0;
		#endregion IO Strings
		#region Control and Calculation
		/// <summary>
		/// The mode dictionary, that holds all the options and runtime preferences
		/// </summary>
		Dictionary<string, string> mode = new Dictionary<string, string>();
		/// <summary>
		/// The list of input commands parsed from txtinp
		/// </summary>
		List<string> i = new List<string>();

		#endregion Control and Calculation
		#endregion Variables

		#region Events
		public Form1() { InitializeComponent(); }
		private void Form1_Load(object sender, EventArgs e) {
			fx2 = (fx = (Width - 16)) / 2; fy2 = (fy = (Height - 39)) / 2;
			Text = fx + ", " + fy;
			gi = new Bitmap(fx, fy);
			gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			#region Rectangle Definition
			// Define the three rectangles
			// b = border width, w = input/variable width
			// A border of 5 pixels around and between all four areas. rects taking the full top width, rectv taking the remaining right side, with recti taking the remaining bottom, all of width 60. recto takes the remaining top left area under rects
			int b = 5, w = 44;
			int lx = fx - w - 3 * b, ly = fy - w - 2 * b;

			int sx = b, sy = b;
			int vx = lx + 2 * b, vy = w + 2 * b;
			int ix = b, iy = ly + b;
			int ox = b, oy = w + 2 * b;

			int sw = fx - 2 * b, sh = w;
			int vw = w, vh = fy - vy - b;
			int iw = lx, ih = w;
			int ow = lx, oh = ly - oy;
			// A border of 5 pixels around and between all three areas, rectv taking the full right side with recti taking the remaining bottom, both of width 60, with recto taking the remaining top left
			//int b = 5, w = 60, lx = fx - w - 3 * b, ly = fy - w - 3 * b;
			//int ox = b, oy = b, ix = b, iy = ly + 2 * b, vx = lx + 2 * b, vy = b;
			//int ow = lx, oh = ly, iw = lx, ih = w, vw = w, vh = fy - 2 * b;
			rects = new Rectangle(sx, sy, sw, sh);
			recti = new Rectangle(ix, iy, iw, ih);
			recto = new Rectangle(ox, oy, ow, oh);
			rectv = new Rectangle(vx, vy, vw, vh);
			#endregion Rectangle Definition
			#region Initialization
			mode.Add("Mode", "Normal");
			mode.Add("Debug", "0");

			#endregion Initialization
			Draw();

			DateTime st = DateTime.Now;
			System.Diagnostics.Debug.Print("Pfact = " + NihilKri.KN.pfact(137));
			System.Diagnostics.Debug.Print("Etime = " + (DateTime.Now - st).TotalMilliseconds + "ms");
			System.Diagnostics.Debug.Flush();
		} // void Form1_Load(object sender, EventArgs e)
		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }
		private void Form1_Resize(object sender, EventArgs e) {

		} // void Form1_Resize(object sender, EventArgs e)
		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			// Check for Shift, etc
			switch(e.Modifiers) {
				#region No modifiers
				case Keys.None:
					switch(e.KeyCode) {
						#region Control keys
						case Keys.Escape: Close(); break;
						case Keys.Enter: if(txtinp.Length > 0) Calc(); break;
						case Keys.Back: if(txtinp.Length > 0 && inpp > 0) {txtinp = txtinp.Substring(0, inpp - 1) + ((inpp == txtinp.Length) ? "" : txtinp.Substring(inpp, txtinp.Length - 1)); inpp -= 1; Chinp(); Text = inpp.ToString(); } break;
						case Keys.Space: ins(" "); break;
						#endregion Control keys

						#region Number keys (useful for a calculator)
						case Keys.D0: case Keys.NumPad0: ins("0"); break;
						case Keys.D1: case Keys.NumPad1: ins("1"); break;
						case Keys.D2: case Keys.NumPad2: ins("2"); break;
						case Keys.D3: case Keys.NumPad3: ins("3"); break;
						case Keys.D4: case Keys.NumPad4: ins("4"); break;
						case Keys.D5: case Keys.NumPad5: ins("5"); break;
						case Keys.D6: case Keys.NumPad6: ins("6"); break;
						case Keys.D7: case Keys.NumPad7: ins("7"); break;
						case Keys.D8: case Keys.NumPad8: ins("8"); break;
						case Keys.D9: case Keys.NumPad9: ins("9"); break;
						case Keys.OemPeriod: case Keys.Decimal: ins("."); break;
						#endregion Number keys (useful for a calculator)

						#region Alphabetic keys
						case Keys.A: ins("a"); break;
						case Keys.B: ins("b"); break;
						case Keys.C: ins("c"); break;
						case Keys.D: ins("d"); break;
						case Keys.E: ins("e"); break;
						case Keys.F: ins("f"); break;
						case Keys.G: ins("g"); break;
						case Keys.H: ins("h"); break;
						case Keys.I: ins("i"); break;
						case Keys.J: ins("j"); break;
						case Keys.K: ins("k"); break;
						case Keys.L: ins("l"); break;
						case Keys.M: ins("m"); break;
						case Keys.N: ins("n"); break;
						case Keys.O: ins("o"); break;
						case Keys.P: ins("p"); break;
						case Keys.Q: ins("q"); break;
						case Keys.R: ins("r"); break;
						case Keys.S: ins("s"); break;
						case Keys.T: ins("t"); break;
						case Keys.U: ins("u"); break;
						case Keys.V: ins("v"); break;
						case Keys.W: ins("w"); break;
						case Keys.X: ins("x"); break;
						case Keys.Y: ins("y"); break;
						case Keys.Z: ins("z"); break;
						#endregion Alphabetic keys

						#region Mathematical symbols
						case Keys.Add: ins("+"); break;
						case Keys.Subtract: case Keys.OemMinus: ins("-"); break;
						case Keys.Multiply: ins("*"); break;
						case Keys.Divide: case Keys.OemQuestion: ins("/"); break;

						#endregion Mathematical symbols


						// Key not found, what's the keycode?
						default: Text = e.KeyCode.ToString(); break;
					} // switch(e.KeyCode)
					break;
				#endregion No modifiers
				#region If shift is held
				case Keys.Shift:
					switch(e.KeyCode) {
						#region Control keys
						case Keys.Enter: ins("\n"); break;
						#endregion Control keys

						#region Capital letters
						case Keys.A: ins("A"); break;
						case Keys.B: ins("B"); break;
						case Keys.C: ins("C"); break;
						case Keys.D: ins("D"); break;
						case Keys.E: ins("E"); break;
						case Keys.F: ins("F"); break;
						case Keys.G: ins("G"); break;
						case Keys.H: ins("H"); break;
						case Keys.I: ins("I"); break;
						case Keys.J: ins("J"); break;
						case Keys.K: ins("K"); break;
						case Keys.L: ins("L"); break;
						case Keys.M: ins("M"); break;
						case Keys.N: ins("N"); break;
						case Keys.O: ins("O"); break;
						case Keys.P: ins("P"); break;
						case Keys.Q: ins("Q"); break;
						case Keys.R: ins("R"); break;
						case Keys.S: ins("S"); break;
						case Keys.T: ins("T"); break;
						case Keys.U: ins("U"); break;
						case Keys.V: ins("V"); break;
						case Keys.W: ins("W"); break;
						case Keys.X: ins("X"); break;
						case Keys.Y: ins("Y"); break;
						case Keys.Z: ins("Z"); break;
						#endregion Capital letters

						#region Mathematical symbols
						case Keys.Oemplus: ins("+"); break;
						case Keys.D6: ins("^"); break;
						case Keys.D8: ins("*"); break;
						#endregion Mathematical symbols


						// Key not found, what's the keycode?
						default: Text = "Shift + " + e.KeyCode.ToString(); break;
					} // switch(e.KeyCode)
					break;
				#endregion If shift is held


				// Key not found, what's the keycode?
				default: Text = "Modifier = " + e.Modifiers.ToString(); break;
			} // switch(e.Modifiers)
		} // void Form1_KeyDown(object sender, KeyEventArgs e)
		private void Form1_Click(object sender, EventArgs e) {

		} // void Form1_Click(object sender, EventArgs e)
		#endregion Events

		#region IO Pipeline
		public void ins(string s) {
			txtinp += s; inpp += s.Length; Chinp(); 
		} // void ins(string s)
		public bool Parse() {
			Draw(true);
			DateTime st = DateTime.Now;
			i.Clear(); //txtans = "";
			char v; string reg = "";
			List<String> nm = new List<string>(), op = new List<string>();
			for(int q = 0; q < txtinp.Length; q++) {
				v = txtinp[q];
				if(char.IsLetterOrDigit(v) || (v == '.' && reg.Length > 0)) { reg += v; } else {
					ParseReg(reg); reg = "";
					i.Add(v.ToString());
					//switch(v) {
					//	case ' ': break;
					//	case '+': i.Add("+"); break;
					//	case '-': i.Add("-"); break;
					//	case '*': i.Add("*"); break;
					//	case '/': i.Add("/"); break;

					//	default:  break;
					//} // switch(v)
				} // else
			} // for(int q = 0; q < txtinp.Length; q++)
			if(reg.Length > 0) ParseReg(reg);
			//foreach(string s in i) 
			//txtans += "\n\"" + s + "\"";


			txttym = "Parse: " + (DateTime.Now - st).TotalMilliseconds + "; ";
			return true;
		} // void Parse()
		public void ParseReg(string reg) {
			i.Add(reg);

		}
		public void Calc() {
			DateTime st = DateTime.Now;
			if(Parse()) {
				st = DateTime.Now; //txtans = "";
				double tmp = 0.0; Stack<double> s = new Stack<double>(); ;
				for (int q = 0; q < i.Count; q++) {
					if(double.TryParse(i[q], out tmp)) {
						s.Push(tmp);

					} else {
						switch (i[q]) {
								// Control
							case "TEST": KNTest(); break;
							case "cls": txtout = ""; break;
							case "debug": mode["Debug"] = (s.Count == 0) ? "0.0" : s.Pop().ToString(); break;

								// Constants
							case "txtans": if(double.TryParse(txtans, out tmp)) s.Push(tmp); break;
							case "pi": s.Push(Math.PI); break;
							case "e": s.Push(Math.E); break;

								// Operators
							case "+":
								if(s.Count > 1) tmp = s.Pop() + s.Pop();
								else if(s.Count == 1 && double.TryParse(txtans, out tmp)) tmp += s.Pop();
								s.Push(tmp); break;
							case "-":
								if(s.Count > 0) tmp = -s.Pop();
								else if(s.Count == 0 && double.TryParse(txtans, out tmp)) tmp = -tmp;
								s.Push(tmp); break;
							case "*":
								if(s.Count > 1) tmp = s.Pop() * s.Pop();
								else if(s.Count == 1 && double.TryParse(txtans, out tmp)) tmp *= s.Pop();
								s.Push(tmp); break;
							case "/":
								if(s.Count > 0) tmp = 1.0 / s.Pop();
								else if(s.Count == 0 && double.TryParse(txtans, out tmp)) tmp = 1.0 / tmp;
								s.Push(tmp); break;
							case "^":
								if(s.Count > 0) tmp = s.Pop(); // Math.Pow(, s.Pop());
								else if(s.Count == 0 && double.TryParse(txtans, out tmp)) ; tmp = Math.Pow(s.Pop(), tmp);
								s.Push(tmp); break;

								// Functions
							case "pfact":
								if(s.Count > 0) tmp = s.Pop(); // Math.Pow(, s.Pop());
								else if(s.Count == 0 && double.TryParse(txtans, out tmp)) ; foreach(int pf in KN.listpfact((int)tmp)) s.Push(pf);
								//s.Push(tmp);
								break;

						} // switch (i[q])


					} // if(!double.TryParse(i[q], out tmp))

				} // for (int q = 0; q < i.Count; q++)
				//if(txtans != "") txtinp += "txtans = " + txtans;
				if(s.Count == 0) { txtinp = ""; } else
				if(s.Count == 1) { txtans = s.Pop().ToString(); txtinp = "> " + txtinp + " = " + txtans + "\n"; Clipboard.SetText(txtinp); } else {
					txtinp = "> ERROR! txtinp = " + txtinp;
					txtinp += "\n  ERROR! txtans = " + txtans;
					txtinp += "\n  ERROR! i[] = ["; foreach(string thing in i) txtinp += thing.ToString() + ", "; txtinp += "]";
					txtinp += "\n  ERROR! s[] = ["; foreach(double thing in s) txtinp += thing.ToString() + ", "; txtinp += "]\n";
				}
				txtout += txtinp; txtinp = ""; inpp = selp = 0; Text = inpp.ToString();
				//txtout += "\n\nans = " + txtans + "\n\n";
			} // if(Parse())
			txttym += "Calc: " + (DateTime.Now - st).TotalMilliseconds + "; ";
			Draw();
		} // void Calc()
		public void Chinp() {
			gb.FillRectangle(Brushes.DarkBlue, recti); // txtinp
			gb.DrawRectangle(Pens.LightBlue, recti); // txtinp
			gb.DrawString(txtinp, Font, Brushes.White, recti.Location);
			Text = inpp.ToString(); gf.DrawImage(gi, 0, 0);
		} // void Chinp()
		public void Draw(bool busy = false) {
			DateTime st = DateTime.Now;
			gb.Clear(busy ? Color.White : Color.Black);
			gb.FillRectangle(Brushes.DarkSlateGray, rects); // status
			gb.FillRectangle(Brushes.DarkRed, recto); // txtout
			gb.FillRectangle(Brushes.DarkGreen, rectv); // txtvar
			gb.DrawRectangle(Pens.Cyan, rects); // Status
			gb.DrawRectangle(Pens.Pink, recto); // txtout
			gb.DrawRectangle(Pens.LightGreen, rectv); // txtvar

			if(!busy) {
				txttym += "Draw: " + (DateTime.Now - st).TotalMilliseconds + " ms\n";
				if(mode["Debug"] == "0") txtsta = txttym; else { txtout += txttym; txtsta = "\n"; }
				txtsta += "\nMode: " + mode["Mode"].ToString();
				txtsta += "\nDebug: " + mode["Debug"].ToString();
			} // if(!busy)

			//gb.DrawString(txtinp, Font, Brushes.White, recti.Location);
			gb.DrawString(txtout, Font, Brushes.White, recto.Location);
			gb.DrawString(txtvar, Font, Brushes.White, rectv.Location);
			gb.DrawString(txtsta, Font, Brushes.White, rects.Location);
			//gf.DrawImage(gi, 0, 0);
			Chinp();
		} // void Draw(bool busy = false)
		#endregion IO Pipeline

		#region Testing
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

			txtout += "\nKNTest:"; Draw(true);
			st = DateTime.Now; txtout += "\nint + int: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { c = a + b; }
			//gb.DrawString("int + int: " + (DateTime.Now - st).TotalMilliseconds + " ms", Font, Brushes.White, 6, l * 12 + 6); l++;
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nbyte+byte: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { bc = (byte)(ba + bb); }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nshrt+shrt: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { sc = (short)(sa + sb); }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nlong+long: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { lc = la + lb; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\ndbl + dbl: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { dc = da + db; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nSabi+Sabi: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { cc = ca + cb; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nKabi+Kabi: "; Draw(true);
			for(int q = 0; q < 100000000; q++) { kc = ka + kb; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";

			l++;

			st = DateTime.Now; txtout += "\ni256+i256: "; Draw(true);
			for(int q = 0; q < 100000; q++) { ic = ia + ib; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nBigI+BigI: "; Draw(true);
			for(int q = 0; q < 100000; q++) { Ic = Ia + Ib; }
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";

			l++;

			st = DateTime.Now; txtout += "\nS1024: "; Draw(true);
			//cc = new System.Numerics.Complex(0, 0); 
			for(int y = 0; y < 1024; y++) {
				for(int x = 0; x < 1024; x++) {
					//cb = new System.Numerics.Complex(-x, -y) + cc; 
					//ca = System.Numerics.Complex.Log(new System.Numerics.Complex(x, y)); 
					ca = new System.Numerics.Complex(x, y).Phase;
					c = this.c(ca);
				}
			}
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";
			st = DateTime.Now; txtout += "\nK1024: "; Draw(true);
			//kc = new Complex(0, 0);
			for(int y = 0; y < 1024; y++) {
				for(int x = 0; x < 1024; x++) {
					//kb = new Complex(-x, -y) + kc;
					ka = (new Complex(x, y)).t; c = ka.c;
				}
			}
			txtout += (DateTime.Now - st).TotalMilliseconds + " ms";

			//gb.DrawString(ia.ToString() + "\n" + ia.ToString(true), Font, Brushes.White, 20, 20);
			//gb.DrawString(ib.ToString() + "\n" + ib.ToString(true), Font, Brushes.White, 300, 20);
			//gb.DrawString(((int)(ia + ib+7000)).ToString() + "\n" + (ia + ib+7000).ToString() + "\n" + (ia + ib).ToString(true), Font, Brushes.White, 200, 230);


		} // void KNTest()
		double tau = 2 * Math.PI;
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
			}
			return ~0xFFFFFF +
			((int)Math.Floor((cr + m) * 255.0) << 16) +
			((int)Math.Floor((cg + m) * 255.0) << 8) +
				(int)Math.Floor((cb + m) * 255.0);

		} // int c(System.Numerics.Complex n)
		#endregion Testing

	} // class Form1
} // namespace Calculator
