
namespace VaR
{
    partial class Form1
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
            this.dgTicks = new System.Windows.Forms.DataGridView();
            this.dgPortfolio = new System.Windows.Forms.DataGridView();
            this.BtnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgTicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolio)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTicks
            // 
            this.dgTicks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTicks.Location = new System.Drawing.Point(0, 0);
            this.dgTicks.Name = "dgTicks";
            this.dgTicks.Size = new System.Drawing.Size(240, 150);
            this.dgTicks.TabIndex = 0;
            // 
            // dgPortfolio
            // 
            this.dgPortfolio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPortfolio.Location = new System.Drawing.Point(273, 0);
            this.dgPortfolio.Name = "dgPortfolio";
            this.dgPortfolio.Size = new System.Drawing.Size(240, 150);
            this.dgPortfolio.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(212, 193);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "Mentés";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.dgPortfolio);
            this.Controls.Add(this.dgTicks);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgTicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPortfolio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTicks;
        private System.Windows.Forms.DataGridView dgPortfolio;
        private System.Windows.Forms.Button BtnSave;
    }
}

