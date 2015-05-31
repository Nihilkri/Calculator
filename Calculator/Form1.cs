using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
	public partial class Form1 : Form {
		public Form1() {InitializeComponent();	}
		private void Form1_Load(object sender, EventArgs e) {
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

		public void Calc() {
			outp.Text += "\n" + inp.Text; inp.Clear();


		}


	}
}
