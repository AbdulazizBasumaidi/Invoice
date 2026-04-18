namespace Invoice
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonView = new System.Windows.Forms.Button();
            this.CreateInvoice = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonView);
            this.panel1.Controls.Add(this.CreateInvoice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 322);
            this.panel1.TabIndex = 0;
            // 
            // buttonView
            // 
            this.buttonView.BackColor = System.Drawing.Color.Transparent;
            this.buttonView.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonView.FlatAppearance.BorderSize = 0;
            this.buttonView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonView.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonView.ForeColor = System.Drawing.Color.White;
            this.buttonView.Location = new System.Drawing.Point(0, 60);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(168, 60);
            this.buttonView.TabIndex = 1;
            this.buttonView.Text = "عرض كل الفواتير";
            this.buttonView.UseVisualStyleBackColor = false;
            this.buttonView.Click += new System.EventHandler(this.button2_Click);
            // 
            // CreateInvoice
            // 
            this.CreateInvoice.BackColor = System.Drawing.Color.Transparent;
            this.CreateInvoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.CreateInvoice.FlatAppearance.BorderSize = 0;
            this.CreateInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CreateInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CreateInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateInvoice.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateInvoice.ForeColor = System.Drawing.Color.White;
            this.CreateInvoice.Location = new System.Drawing.Point(0, 0);
            this.CreateInvoice.Name = "CreateInvoice";
            this.CreateInvoice.Size = new System.Drawing.Size(168, 60);
            this.CreateInvoice.TabIndex = 0;
            this.CreateInvoice.Text = "إنشاء فاتورة";
            this.CreateInvoice.UseVisualStyleBackColor = false;
            this.CreateInvoice.Click += new System.EventHandler(this.CreateInvoice_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arabic Typesetting", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(282, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 111);
            this.label1.TabIndex = 1;
            this.label1.Text = "برنامج فاتورة";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arabic Typesetting", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(535, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "إنشاء فاتورة جديدة - عرض فاتورة - تعديل فاتورة -حذف فاتورة";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(704, 322);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "برنامج الفواتير";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Button CreateInvoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

