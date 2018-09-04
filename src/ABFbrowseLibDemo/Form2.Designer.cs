namespace ABFbrowseLibTest
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.abFbrowseUC1 = new ABFbrowseLib.ABFbrowseUC();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "selected ABF filename";
            // 
            // abFbrowseUC1
            // 
            this.abFbrowseUC1.Location = new System.Drawing.Point(12, 41);
            this.abFbrowseUC1.Name = "abFbrowseUC1";
            this.abFbrowseUC1.Size = new System.Drawing.Size(818, 437);
            this.abFbrowseUC1.TabIndex = 2;
            this.abFbrowseUC1.ABFclicked += new System.EventHandler(this.abFbrowseUC1_ABFclicked_1);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 490);
            this.Controls.Add(this.abFbrowseUC1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2 - demo using the ABFbrowseUC User Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private ABFbrowseLib.ABFbrowseUC abFbrowseUC1;
    }
}