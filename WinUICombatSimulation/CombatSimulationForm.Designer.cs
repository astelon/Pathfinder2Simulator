
namespace WinUICombatSimulation
{
    partial class CombatSimulationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxObserverUI = new System.Windows.Forms.RichTextBox();
            this.monsterComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxObserverUI
            // 
            this.richTextBoxObserverUI.Location = new System.Drawing.Point(12, 52);
            this.richTextBoxObserverUI.Name = "richTextBoxObserverUI";
            this.richTextBoxObserverUI.ReadOnly = true;
            this.richTextBoxObserverUI.Size = new System.Drawing.Size(776, 386);
            this.richTextBoxObserverUI.TabIndex = 0;
            this.richTextBoxObserverUI.Text = "";
            // 
            // monsterComboBox
            // 
            this.monsterComboBox.FormattingEnabled = true;
            this.monsterComboBox.Location = new System.Drawing.Point(95, 9);
            this.monsterComboBox.Name = "monsterComboBox";
            this.monsterComboBox.Size = new System.Drawing.Size(570, 28);
            this.monsterComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monster";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(687, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(101, 28);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // CombatSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monsterComboBox);
            this.Controls.Add(this.richTextBoxObserverUI);
            this.Name = "CombatSimulationForm";
            this.Text = "CombatSimulation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxObserverUI;
        private System.Windows.Forms.ComboBox monsterComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button runButton;
    }
}

