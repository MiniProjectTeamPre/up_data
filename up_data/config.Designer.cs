namespace up_data {
    partial class config {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(config));
            this.pgb_operation = new System.Windows.Forms.PictureBox();
            this.pgb_debug = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ctms_sever = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tPRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tPPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pgb_operation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgb_debug)).BeginInit();
            this.ctms_sever.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgb_operation
            // 
            this.pgb_operation.Image = global::up_data.Properties.Resources.operation;
            this.pgb_operation.Location = new System.Drawing.Point(0, 0);
            this.pgb_operation.Name = "pgb_operation";
            this.pgb_operation.Size = new System.Drawing.Size(323, 191);
            this.pgb_operation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pgb_operation.TabIndex = 0;
            this.pgb_operation.TabStop = false;
            this.pgb_operation.Click += new System.EventHandler(this.pgb_operation_Click);
            // 
            // pgb_debug
            // 
            this.pgb_debug.BackColor = System.Drawing.SystemColors.Control;
            this.pgb_debug.Image = global::up_data.Properties.Resources.debug;
            this.pgb_debug.Location = new System.Drawing.Point(323, 0);
            this.pgb_debug.Name = "pgb_debug";
            this.pgb_debug.Size = new System.Drawing.Size(323, 191);
            this.pgb_debug.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pgb_debug.TabIndex = 1;
            this.pgb_debug.TabStop = false;
            this.pgb_debug.Click += new System.EventHandler(this.pgb_debug_Click);
            // 
            // textBox1
            // 
            this.textBox1.ContextMenuStrip = this.ctms_sever;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox1.Location = new System.Drawing.Point(323, 221);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 31);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "192.168.11.38";
            // 
            // textBox2
            // 
            this.textBox2.ContextMenuStrip = this.ctms_sever;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox2.Location = new System.Drawing.Point(323, 277);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(312, 31);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "TPP_PRISM";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox3.Location = new System.Drawing.Point(323, 332);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(312, 31);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "SST-PC-FCT1";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox4.Location = new System.Drawing.Point(323, 386);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(312, 31);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "FCT1_1";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox5.Location = new System.Drawing.Point(323, 440);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(312, 31);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "FCT1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Aqua;
            this.label1.ContextMenuStrip = this.ctms_sever;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(61, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Database Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Aqua;
            this.label2.ContextMenuStrip = this.ctms_sever;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(71, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Database Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Aqua;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(69, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Computer Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Aqua;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(103, 386);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Station Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aqua;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(89, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "Process Name";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::up_data.Properties.Resources.b_save;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(503, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 49);
            this.button1.TabIndex = 12;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctms_sever
            // 
            this.ctms_sever.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tPRToolStripMenuItem,
            this.tPPToolStripMenuItem});
            this.ctms_sever.Name = "ctms_sever";
            this.ctms_sever.Size = new System.Drawing.Size(96, 48);
            // 
            // tPRToolStripMenuItem
            // 
            this.tPRToolStripMenuItem.Name = "tPRToolStripMenuItem";
            this.tPRToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.tPRToolStripMenuItem.Text = "TPR";
            this.tPRToolStripMenuItem.Click += new System.EventHandler(this.tPRToolStripMenuItem_Click);
            // 
            // tPPToolStripMenuItem
            // 
            this.tPPToolStripMenuItem.Name = "tPPToolStripMenuItem";
            this.tPPToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.tPPToolStripMenuItem.Text = "TPP";
            this.tPPToolStripMenuItem.Click += new System.EventHandler(this.tPPToolStripMenuItem_Click);
            // 
            // config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::up_data.Properties.Resources.b_config;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(647, 627);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pgb_debug);
            this.Controls.Add(this.pgb_operation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "config";
            ((System.ComponentModel.ISupportInitialize)(this.pgb_operation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgb_debug)).EndInit();
            this.ctms_sever.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pgb_operation;
        private System.Windows.Forms.PictureBox pgb_debug;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip ctms_sever;
        private System.Windows.Forms.ToolStripMenuItem tPRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tPPToolStripMenuItem;
    }
}