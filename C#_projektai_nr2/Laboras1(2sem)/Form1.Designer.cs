namespace Laboras1_2sem_
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
            this.close = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.RichTextBox();
            this.run = new System.Windows.Forms.Button();
            this.Įvesti = new System.Windows.Forms.TextBox();
            this.count = new System.Windows.Forms.Button();
            this.sort = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.results2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.close.Location = new System.Drawing.Point(955, 503);
            this.close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(183, 50);
            this.close.TabIndex = 0;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // results
            // 
            this.results.Font = new System.Drawing.Font("Courier New", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.results.Location = new System.Drawing.Point(31, 12);
            this.results.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(428, 541);
            this.results.TabIndex = 1;
            this.results.Text = "";
            // 
            // run
            // 
            this.run.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.run.Location = new System.Drawing.Point(506, 12);
            this.run.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(179, 50);
            this.run.TabIndex = 2;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // Įvesti
            // 
            this.Įvesti.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Įvesti.ForeColor = System.Drawing.Color.Red;
            this.Įvesti.Location = new System.Drawing.Point(479, 230);
            this.Įvesti.Name = "Įvesti";
            this.Įvesti.Size = new System.Drawing.Size(206, 30);
            this.Įvesti.TabIndex = 3;
            this.Įvesti.Text = "player\'s age";
            // 
            // count
            // 
            this.count.Enabled = false;
            this.count.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.count.Location = new System.Drawing.Point(506, 84);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(179, 50);
            this.count.TabIndex = 4;
            this.count.Text = "Count";
            this.count.UseVisualStyleBackColor = true;
            this.count.Click += new System.EventHandler(this.count_Click);
            // 
            // sort
            // 
            this.sort.Enabled = false;
            this.sort.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.sort.Location = new System.Drawing.Point(506, 154);
            this.sort.Name = "sort";
            this.sort.Size = new System.Drawing.Size(179, 50);
            this.sort.TabIndex = 5;
            this.sort.Text = "Sort";
            this.sort.UseVisualStyleBackColor = true;
            this.sort.Click += new System.EventHandler(this.sort_Click);
            // 
            // remove
            // 
            this.remove.Enabled = false;
            this.remove.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.remove.Location = new System.Drawing.Point(506, 293);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(179, 50);
            this.remove.TabIndex = 6;
            this.remove.Text = "Remove";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // results2
            // 
            this.results2.Font = new System.Drawing.Font("Courier New", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.results2.Location = new System.Drawing.Point(703, 12);
            this.results2.Name = "results2";
            this.results2.Size = new System.Drawing.Size(444, 458);
            this.results2.TabIndex = 7;
            this.results2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 573);
            this.Controls.Add(this.results2);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.sort);
            this.Controls.Add(this.count);
            this.Controls.Add(this.Įvesti);
            this.Controls.Add(this.run);
            this.Controls.Add(this.results);
            this.Controls.Add(this.close);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Krepšininkai";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RichTextBox results;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.TextBox Įvesti;
        private System.Windows.Forms.Button count;
        private System.Windows.Forms.Button sort;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.RichTextBox results2;
    }
}

