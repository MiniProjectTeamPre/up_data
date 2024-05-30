using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace up_data {
    public partial class WaitChrome : Form {
        public WaitChrome() {
            InitializeComponent();
        }

        public bool close = false;
        private void WaitChrome_FormClosing(object sender, FormClosingEventArgs e) {
            if (!close) {
                e.Cancel = true;
                return;
            }
        }
    }
}
