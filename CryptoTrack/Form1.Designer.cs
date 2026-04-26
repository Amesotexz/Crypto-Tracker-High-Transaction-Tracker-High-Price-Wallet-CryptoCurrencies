namespace CryptoPriceTracker.WinForms;

partial class Form1
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
        label2 = new Label();
        label1 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        labelBybit = new Label();
        comboBoxPairs = new ComboBox();
        labelBitget = new Label();
        labelKucoin = new Label();
        labelBinance = new Label();
        buttonStartStop = new Button();
        comboBoxProtocols = new ComboBox();
        label6 = new Label();
        SuspendLayout();
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label2.ForeColor = SystemColors.ControlText;
        label2.Location = new Point(36, 186);
        label2.Name = "label2";
        label2.RightToLeft = RightToLeft.Yes;
        label2.Size = new Size(102, 33);
        label2.TabIndex = 0;
        label2.Text = "Binance";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label1.ForeColor = SystemColors.ControlText;
        label1.Location = new Point(36, 44);
        label1.Name = "label1";
        label1.RightToLeft = RightToLeft.Yes;
        label1.Size = new Size(71, 33);
        label1.TabIndex = 1;
        label1.Text = "Bybit";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label3.ForeColor = SystemColors.ControlText;
        label3.Location = new Point(36, 93);
        label3.Name = "label3";
        label3.RightToLeft = RightToLeft.Yes;
        label3.Size = new Size(82, 33);
        label3.TabIndex = 2;
        label3.Text = "Bitget";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label4.ForeColor = SystemColors.ControlText;
        label4.Location = new Point(36, 139);
        label4.Name = "label4";
        label4.RightToLeft = RightToLeft.Yes;
        label4.Size = new Size(89, 33);
        label4.TabIndex = 3;
        label4.Text = "Kucoin";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label5.ForeColor = SystemColors.ControlText;
        label5.Location = new Point(36, 269);
        label5.Name = "label5";
        label5.RightToLeft = RightToLeft.Yes;
        label5.Size = new Size(110, 33);
        label5.TabIndex = 6;
        label5.Text = "Currency";
        // 
        // labelBybit
        // 
        labelBybit.AutoSize = true;
        labelBybit.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        labelBybit.ForeColor = SystemColors.ControlText;
        labelBybit.Location = new Point(191, 44);
        labelBybit.Name = "labelBybit";
        labelBybit.RightToLeft = RightToLeft.Yes;
        labelBybit.Size = new Size(29, 33);
        labelBybit.TabIndex = 7;
        labelBybit.Text = "0";
        // 
        // comboBoxPairs
        // 
        comboBoxPairs.Font = new Font("Montserrat", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        comboBoxPairs.FormattingEnabled = true;
        comboBoxPairs.Location = new Point(162, 266);
        comboBoxPairs.Name = "comboBoxPairs";
        comboBoxPairs.Size = new Size(121, 38);
        comboBoxPairs.TabIndex = 8;
        // 
        // labelBitget
        // 
        labelBitget.AutoSize = true;
        labelBitget.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        labelBitget.ForeColor = SystemColors.ControlText;
        labelBitget.Location = new Point(191, 93);
        labelBitget.Name = "labelBitget";
        labelBitget.RightToLeft = RightToLeft.Yes;
        labelBitget.Size = new Size(29, 33);
        labelBitget.TabIndex = 9;
        labelBitget.Text = "0";
        // 
        // labelKucoin
        // 
        labelKucoin.AutoSize = true;
        labelKucoin.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        labelKucoin.ForeColor = SystemColors.ControlText;
        labelKucoin.Location = new Point(191, 139);
        labelKucoin.Name = "labelKucoin";
        labelKucoin.RightToLeft = RightToLeft.Yes;
        labelKucoin.Size = new Size(29, 33);
        labelKucoin.TabIndex = 10;
        labelKucoin.Text = "0";
        // 
        // labelBinance
        // 
        labelBinance.AutoSize = true;
        labelBinance.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        labelBinance.ForeColor = SystemColors.ControlText;
        labelBinance.Location = new Point(191, 186);
        labelBinance.Name = "labelBinance";
        labelBinance.RightToLeft = RightToLeft.Yes;
        labelBinance.Size = new Size(29, 33);
        labelBinance.TabIndex = 11;
        labelBinance.Text = "0";
        // 
        // buttonStartStop
        // 
        buttonStartStop.Font = new Font("Montserrat", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        buttonStartStop.Location = new Point(75, 371);
        buttonStartStop.Name = "buttonStartStop";
        buttonStartStop.Size = new Size(165, 52);
        buttonStartStop.TabIndex = 12;
        buttonStartStop.Text = "Start ";
        buttonStartStop.UseVisualStyleBackColor = true;
        buttonStartStop.Click += buttonStartStop_Click;
        // 
        // comboBoxProtocols
        // 
        comboBoxProtocols.Font = new Font("Montserrat", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        comboBoxProtocols.FormattingEnabled = true;
        comboBoxProtocols.Location = new Point(162, 310);
        comboBoxProtocols.Name = "comboBoxProtocols";
        comboBoxProtocols.Size = new Size(121, 38);
        comboBoxProtocols.TabIndex = 14;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Montserrat", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label6.ForeColor = SystemColors.ControlText;
        label6.Location = new Point(36, 313);
        label6.Name = "label6";
        label6.RightToLeft = RightToLeft.Yes;
        label6.Size = new Size(104, 33);
        label6.TabIndex = 13;
        label6.Text = "Protocol";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.InactiveCaption;
        ClientSize = new Size(828, 463);
        Controls.Add(comboBoxProtocols);
        Controls.Add(label6);
        Controls.Add(buttonStartStop);
        Controls.Add(labelBinance);
        Controls.Add(labelKucoin);
        Controls.Add(labelBitget);
        Controls.Add(comboBoxPairs);
        Controls.Add(labelBybit);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label1);
        Controls.Add(label2);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Crypto Tracker";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label2;
    private Label label1;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label labelBybit;
    private ComboBox comboBoxPairs;
    private Label labelBitget;
    private Label labelKucoin;
    private Label labelBinance;
    private Button buttonStartStop;
    private ComboBox comboBoxProtocols;
    private Label label6;
}