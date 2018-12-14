using System.Windows.Forms;

public static class Prompt
{
    public static string ShowDialog(string text, string caption) {
        Form prompt = new Form() {
            Width = 500,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = caption,
            StartPosition = FormStartPosition.CenterScreen
        };
        Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
        Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
    }

    public static string ShowTypeDialog() {
        Form prompt = new Form();

        System.Windows.Forms.Label label1;
        System.Windows.Forms.Label label2;
        System.Windows.Forms.Panel panel1;
        System.Windows.Forms.RadioButton radioButton2;
        System.Windows.Forms.RadioButton radioButton1;
        System.Windows.Forms.Panel panel2;
        System.Windows.Forms.Button button1;
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        panel2 = new System.Windows.Forms.Panel();
        button1 = new System.Windows.Forms.Button();
        radioButton1 = new System.Windows.Forms.RadioButton();
        radioButton2 = new System.Windows.Forms.RadioButton();

        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = System.Windows.Forms.DockStyle.Top;
        label1.Font = new System.Drawing.Font("Arial Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
        label1.Location = new System.Drawing.Point(0, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(248, 33);
        label1.TabIndex = 1;
        label1.Text = "Login Verification";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = System.Windows.Forms.DockStyle.Top;
        label2.Location = new System.Drawing.Point(0, 33);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(402, 13);
        label2.TabIndex = 2;
        label2.Text = "In order to verify your identity, we\'ll send you a code to your preferred method " +
"below.";
        // 
        // panel1
        // 
        panel1.Controls.Add(radioButton2);
        panel1.Controls.Add(radioButton1);
        panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        panel1.Location = new System.Drawing.Point(0, 46);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(587, 84);
        panel1.TabIndex = 3;
        // 
        // panel2
        // 
        panel2.Controls.Add(button1);
        panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
        panel2.Location = new System.Drawing.Point(0, 130);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(587, 39);
        panel2.TabIndex = 0;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(12, 6);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(142, 23);
        button1.TabIndex = 0;
        button1.Text = "Send Security Code";
        button1.UseVisualStyleBackColor = true;
        button1.DialogResult = DialogResult.OK;
        // 
        // radioButton1
        // 
        radioButton1.AutoSize = true;
        radioButton1.Checked = true;
        radioButton1.Location = new System.Drawing.Point(12, 29);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new System.Drawing.Size(143, 17);
        radioButton1.TabIndex = 0;
        radioButton1.TabStop = true;
        radioButton1.Text = "Send to my Primary Email";
        radioButton1.UseVisualStyleBackColor = true;
        // 
        // radioButton2
        // 
        radioButton2.AutoSize = true;
        radioButton2.Location = new System.Drawing.Point(12, 52);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new System.Drawing.Size(148, 17);
        radioButton2.TabIndex = 1;
        radioButton2.TabStop = true;
        radioButton2.Text = "Use my App Authenticator";
        radioButton2.UseVisualStyleBackColor = true;
        // 
        // LoginVerification
        // 
        prompt.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        prompt.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        prompt.ClientSize = new System.Drawing.Size(587, 169);
        prompt.Controls.Add(panel1);
        prompt.Controls.Add(panel2);
        prompt.Controls.Add(label2);
        prompt.Controls.Add(label1);
        prompt.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        prompt.Name = "LoginVerification";
        prompt.Text = "LoginVerification";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        panel2.ResumeLayout(false);
        prompt.ResumeLayout(false);
        prompt.PerformLayout();

        button1.Click += (sender, e) => { prompt.Close(); };
        prompt.AcceptButton = button1;

        return prompt.ShowDialog() == DialogResult.OK ? (radioButton1.Checked ? "EMAIL" : "APP") : "";
    }
}