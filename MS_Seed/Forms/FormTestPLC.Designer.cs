namespace MS_Seed.Forms
{
    partial class FormTestPLC
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
            this.BtnWriteBit = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.BtnWriteWord = new System.Windows.Forms.Button();
            this.BtnWriteDWord = new System.Windows.Forms.Button();
            this.BtnWriteString = new System.Windows.Forms.Button();
            this.BtnWriteFloat = new System.Windows.Forms.Button();
            this.BtnReadFloat = new System.Windows.Forms.Button();
            this.BtnReadString = new System.Windows.Forms.Button();
            this.BtnReadDWord = new System.Windows.Forms.Button();
            this.BtnReadWord = new System.Windows.Forms.Button();
            this.BtnReadBit = new System.Windows.Forms.Button();
            this.txtWriteRegister1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWriteValue1 = new System.Windows.Forms.TextBox();
            this.txtWriteValue2 = new System.Windows.Forms.TextBox();
            this.txtWriteRegister2 = new System.Windows.Forms.TextBox();
            this.txtWriteValue3 = new System.Windows.Forms.TextBox();
            this.txtWriteRegister3 = new System.Windows.Forms.TextBox();
            this.txtWriteValue4 = new System.Windows.Forms.TextBox();
            this.txtWriteRegister4 = new System.Windows.Forms.TextBox();
            this.txtWriteValue5 = new System.Windows.Forms.TextBox();
            this.txtWriteRegister5 = new System.Windows.Forms.TextBox();
            this.txtWritePlc5 = new System.Windows.Forms.TextBox();
            this.txtWritePlc4 = new System.Windows.Forms.TextBox();
            this.txtWritePlc3 = new System.Windows.Forms.TextBox();
            this.txtWritePlc2 = new System.Windows.Forms.TextBox();
            this.txtWritePlc1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReadPlc5 = new System.Windows.Forms.TextBox();
            this.txtReadPlc4 = new System.Windows.Forms.TextBox();
            this.txtReadPlc3 = new System.Windows.Forms.TextBox();
            this.txtReadPlc2 = new System.Windows.Forms.TextBox();
            this.txtReadPlc1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReadRegister5 = new System.Windows.Forms.TextBox();
            this.txtReadRegister4 = new System.Windows.Forms.TextBox();
            this.txtReadRegister3 = new System.Windows.Forms.TextBox();
            this.txtReadRegister2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReadRegister1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnWriteBit
            // 
            this.BtnWriteBit.Location = new System.Drawing.Point(30, 42);
            this.BtnWriteBit.Name = "BtnWriteBit";
            this.BtnWriteBit.Size = new System.Drawing.Size(89, 32);
            this.BtnWriteBit.TabIndex = 0;
            this.BtnWriteBit.Text = "Write Bit";
            this.BtnWriteBit.UseVisualStyleBackColor = true;
            this.BtnWriteBit.Click += new System.EventHandler(this.BtnWriteBit_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(30, 299);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(677, 245);
            this.txtResult.TabIndex = 1;
            // 
            // BtnWriteWord
            // 
            this.BtnWriteWord.Location = new System.Drawing.Point(30, 90);
            this.BtnWriteWord.Name = "BtnWriteWord";
            this.BtnWriteWord.Size = new System.Drawing.Size(89, 32);
            this.BtnWriteWord.TabIndex = 2;
            this.BtnWriteWord.Text = "Write Word";
            this.BtnWriteWord.UseVisualStyleBackColor = true;
            this.BtnWriteWord.Click += new System.EventHandler(this.BtnWriteWord_Click);
            // 
            // BtnWriteDWord
            // 
            this.BtnWriteDWord.Location = new System.Drawing.Point(30, 137);
            this.BtnWriteDWord.Name = "BtnWriteDWord";
            this.BtnWriteDWord.Size = new System.Drawing.Size(89, 32);
            this.BtnWriteDWord.TabIndex = 3;
            this.BtnWriteDWord.Text = "Write DWord";
            this.BtnWriteDWord.UseVisualStyleBackColor = true;
            this.BtnWriteDWord.Click += new System.EventHandler(this.BtnWriteDWord_Click);
            // 
            // BtnWriteString
            // 
            this.BtnWriteString.Location = new System.Drawing.Point(30, 185);
            this.BtnWriteString.Name = "BtnWriteString";
            this.BtnWriteString.Size = new System.Drawing.Size(89, 32);
            this.BtnWriteString.TabIndex = 4;
            this.BtnWriteString.Text = "Write String";
            this.BtnWriteString.UseVisualStyleBackColor = true;
            this.BtnWriteString.Click += new System.EventHandler(this.BtnWriteString_Click);
            // 
            // BtnWriteFloat
            // 
            this.BtnWriteFloat.Location = new System.Drawing.Point(30, 232);
            this.BtnWriteFloat.Name = "BtnWriteFloat";
            this.BtnWriteFloat.Size = new System.Drawing.Size(89, 32);
            this.BtnWriteFloat.TabIndex = 5;
            this.BtnWriteFloat.Text = "Write Float";
            this.BtnWriteFloat.UseVisualStyleBackColor = true;
            this.BtnWriteFloat.Click += new System.EventHandler(this.BtnWriteFloat_Click);
            // 
            // BtnReadFloat
            // 
            this.BtnReadFloat.Location = new System.Drawing.Point(449, 232);
            this.BtnReadFloat.Name = "BtnReadFloat";
            this.BtnReadFloat.Size = new System.Drawing.Size(89, 32);
            this.BtnReadFloat.TabIndex = 10;
            this.BtnReadFloat.Text = "Read Float";
            this.BtnReadFloat.UseVisualStyleBackColor = true;
            this.BtnReadFloat.Click += new System.EventHandler(this.BtnReadFloat_Click);
            // 
            // BtnReadString
            // 
            this.BtnReadString.Location = new System.Drawing.Point(449, 185);
            this.BtnReadString.Name = "BtnReadString";
            this.BtnReadString.Size = new System.Drawing.Size(89, 32);
            this.BtnReadString.TabIndex = 9;
            this.BtnReadString.Text = "Read String";
            this.BtnReadString.UseVisualStyleBackColor = true;
            this.BtnReadString.Click += new System.EventHandler(this.BtnReadString_Click);
            // 
            // BtnReadDWord
            // 
            this.BtnReadDWord.Location = new System.Drawing.Point(449, 137);
            this.BtnReadDWord.Name = "BtnReadDWord";
            this.BtnReadDWord.Size = new System.Drawing.Size(89, 32);
            this.BtnReadDWord.TabIndex = 8;
            this.BtnReadDWord.Text = "Read DWord";
            this.BtnReadDWord.UseVisualStyleBackColor = true;
            this.BtnReadDWord.Click += new System.EventHandler(this.BtnReadDWord_Click);
            // 
            // BtnReadWord
            // 
            this.BtnReadWord.Location = new System.Drawing.Point(449, 90);
            this.BtnReadWord.Name = "BtnReadWord";
            this.BtnReadWord.Size = new System.Drawing.Size(89, 32);
            this.BtnReadWord.TabIndex = 7;
            this.BtnReadWord.Text = "Read Word";
            this.BtnReadWord.UseVisualStyleBackColor = true;
            this.BtnReadWord.Click += new System.EventHandler(this.BtnReadWord_Click);
            // 
            // BtnReadBit
            // 
            this.BtnReadBit.Location = new System.Drawing.Point(449, 42);
            this.BtnReadBit.Name = "BtnReadBit";
            this.BtnReadBit.Size = new System.Drawing.Size(89, 32);
            this.BtnReadBit.TabIndex = 6;
            this.BtnReadBit.Text = "Read Bit";
            this.BtnReadBit.UseVisualStyleBackColor = true;
            this.BtnReadBit.Click += new System.EventHandler(this.BtnReadBit_Click);
            // 
            // txtWriteRegister1
            // 
            this.txtWriteRegister1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteRegister1.Location = new System.Drawing.Point(125, 42);
            this.txtWriteRegister1.Multiline = true;
            this.txtWriteRegister1.Name = "txtWriteRegister1";
            this.txtWriteRegister1.Size = new System.Drawing.Size(116, 32);
            this.txtWriteRegister1.TabIndex = 11;
            this.txtWriteRegister1.Text = "M34000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Register";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Value";
            // 
            // txtWriteValue1
            // 
            this.txtWriteValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteValue1.Location = new System.Drawing.Point(245, 42);
            this.txtWriteValue1.Multiline = true;
            this.txtWriteValue1.Name = "txtWriteValue1";
            this.txtWriteValue1.Size = new System.Drawing.Size(81, 32);
            this.txtWriteValue1.TabIndex = 14;
            this.txtWriteValue1.Text = "1";
            // 
            // txtWriteValue2
            // 
            this.txtWriteValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteValue2.Location = new System.Drawing.Point(245, 90);
            this.txtWriteValue2.Multiline = true;
            this.txtWriteValue2.Name = "txtWriteValue2";
            this.txtWriteValue2.Size = new System.Drawing.Size(81, 32);
            this.txtWriteValue2.TabIndex = 16;
            this.txtWriteValue2.Text = "19";
            // 
            // txtWriteRegister2
            // 
            this.txtWriteRegister2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteRegister2.Location = new System.Drawing.Point(125, 90);
            this.txtWriteRegister2.Multiline = true;
            this.txtWriteRegister2.Name = "txtWriteRegister2";
            this.txtWriteRegister2.Size = new System.Drawing.Size(116, 32);
            this.txtWriteRegister2.TabIndex = 15;
            this.txtWriteRegister2.Text = "D45000";
            // 
            // txtWriteValue3
            // 
            this.txtWriteValue3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteValue3.Location = new System.Drawing.Point(245, 137);
            this.txtWriteValue3.Multiline = true;
            this.txtWriteValue3.Name = "txtWriteValue3";
            this.txtWriteValue3.Size = new System.Drawing.Size(81, 32);
            this.txtWriteValue3.TabIndex = 18;
            this.txtWriteValue3.Text = "21";
            // 
            // txtWriteRegister3
            // 
            this.txtWriteRegister3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteRegister3.Location = new System.Drawing.Point(125, 137);
            this.txtWriteRegister3.Multiline = true;
            this.txtWriteRegister3.Name = "txtWriteRegister3";
            this.txtWriteRegister3.Size = new System.Drawing.Size(116, 32);
            this.txtWriteRegister3.TabIndex = 17;
            this.txtWriteRegister3.Text = "D45100";
            // 
            // txtWriteValue4
            // 
            this.txtWriteValue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteValue4.Location = new System.Drawing.Point(245, 185);
            this.txtWriteValue4.Multiline = true;
            this.txtWriteValue4.Name = "txtWriteValue4";
            this.txtWriteValue4.Size = new System.Drawing.Size(81, 32);
            this.txtWriteValue4.TabIndex = 20;
            this.txtWriteValue4.Text = "abc";
            // 
            // txtWriteRegister4
            // 
            this.txtWriteRegister4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteRegister4.Location = new System.Drawing.Point(125, 185);
            this.txtWriteRegister4.Multiline = true;
            this.txtWriteRegister4.Name = "txtWriteRegister4";
            this.txtWriteRegister4.Size = new System.Drawing.Size(116, 32);
            this.txtWriteRegister4.TabIndex = 19;
            this.txtWriteRegister4.Text = "D45200";
            // 
            // txtWriteValue5
            // 
            this.txtWriteValue5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteValue5.Location = new System.Drawing.Point(245, 232);
            this.txtWriteValue5.Multiline = true;
            this.txtWriteValue5.Name = "txtWriteValue5";
            this.txtWriteValue5.Size = new System.Drawing.Size(81, 32);
            this.txtWriteValue5.TabIndex = 22;
            this.txtWriteValue5.Text = "1.5";
            // 
            // txtWriteRegister5
            // 
            this.txtWriteRegister5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteRegister5.Location = new System.Drawing.Point(125, 232);
            this.txtWriteRegister5.Multiline = true;
            this.txtWriteRegister5.Name = "txtWriteRegister5";
            this.txtWriteRegister5.Size = new System.Drawing.Size(116, 32);
            this.txtWriteRegister5.TabIndex = 21;
            this.txtWriteRegister5.Text = "D45300";
            // 
            // txtWritePlc5
            // 
            this.txtWritePlc5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWritePlc5.Location = new System.Drawing.Point(330, 232);
            this.txtWritePlc5.Multiline = true;
            this.txtWritePlc5.Name = "txtWritePlc5";
            this.txtWritePlc5.Size = new System.Drawing.Size(33, 32);
            this.txtWritePlc5.TabIndex = 28;
            this.txtWritePlc5.Text = "1";
            // 
            // txtWritePlc4
            // 
            this.txtWritePlc4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWritePlc4.Location = new System.Drawing.Point(330, 185);
            this.txtWritePlc4.Multiline = true;
            this.txtWritePlc4.Name = "txtWritePlc4";
            this.txtWritePlc4.Size = new System.Drawing.Size(33, 32);
            this.txtWritePlc4.TabIndex = 27;
            this.txtWritePlc4.Text = "1";
            // 
            // txtWritePlc3
            // 
            this.txtWritePlc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWritePlc3.Location = new System.Drawing.Point(330, 137);
            this.txtWritePlc3.Multiline = true;
            this.txtWritePlc3.Name = "txtWritePlc3";
            this.txtWritePlc3.Size = new System.Drawing.Size(33, 32);
            this.txtWritePlc3.TabIndex = 26;
            this.txtWritePlc3.Text = "1";
            // 
            // txtWritePlc2
            // 
            this.txtWritePlc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWritePlc2.Location = new System.Drawing.Point(330, 90);
            this.txtWritePlc2.Multiline = true;
            this.txtWritePlc2.Name = "txtWritePlc2";
            this.txtWritePlc2.Size = new System.Drawing.Size(33, 32);
            this.txtWritePlc2.TabIndex = 25;
            this.txtWritePlc2.Text = "1";
            // 
            // txtWritePlc1
            // 
            this.txtWritePlc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWritePlc1.Location = new System.Drawing.Point(330, 42);
            this.txtWritePlc1.Multiline = true;
            this.txtWritePlc1.Name = "txtWritePlc1";
            this.txtWritePlc1.Size = new System.Drawing.Size(33, 32);
            this.txtWritePlc1.TabIndex = 24;
            this.txtWritePlc1.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Plc";
            // 
            // txtReadPlc5
            // 
            this.txtReadPlc5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadPlc5.Location = new System.Drawing.Point(666, 232);
            this.txtReadPlc5.Multiline = true;
            this.txtReadPlc5.Name = "txtReadPlc5";
            this.txtReadPlc5.Size = new System.Drawing.Size(33, 32);
            this.txtReadPlc5.TabIndex = 46;
            this.txtReadPlc5.Text = "1";
            // 
            // txtReadPlc4
            // 
            this.txtReadPlc4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadPlc4.Location = new System.Drawing.Point(666, 185);
            this.txtReadPlc4.Multiline = true;
            this.txtReadPlc4.Name = "txtReadPlc4";
            this.txtReadPlc4.Size = new System.Drawing.Size(33, 32);
            this.txtReadPlc4.TabIndex = 45;
            this.txtReadPlc4.Text = "1";
            // 
            // txtReadPlc3
            // 
            this.txtReadPlc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadPlc3.Location = new System.Drawing.Point(666, 137);
            this.txtReadPlc3.Multiline = true;
            this.txtReadPlc3.Name = "txtReadPlc3";
            this.txtReadPlc3.Size = new System.Drawing.Size(33, 32);
            this.txtReadPlc3.TabIndex = 44;
            this.txtReadPlc3.Text = "1";
            // 
            // txtReadPlc2
            // 
            this.txtReadPlc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadPlc2.Location = new System.Drawing.Point(666, 90);
            this.txtReadPlc2.Multiline = true;
            this.txtReadPlc2.Name = "txtReadPlc2";
            this.txtReadPlc2.Size = new System.Drawing.Size(33, 32);
            this.txtReadPlc2.TabIndex = 43;
            this.txtReadPlc2.Text = "1";
            // 
            // txtReadPlc1
            // 
            this.txtReadPlc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadPlc1.Location = new System.Drawing.Point(666, 42);
            this.txtReadPlc1.Multiline = true;
            this.txtReadPlc1.Name = "txtReadPlc1";
            this.txtReadPlc1.Size = new System.Drawing.Size(33, 32);
            this.txtReadPlc1.TabIndex = 42;
            this.txtReadPlc1.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(665, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Plc";
            // 
            // txtReadRegister5
            // 
            this.txtReadRegister5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadRegister5.Location = new System.Drawing.Point(545, 232);
            this.txtReadRegister5.Multiline = true;
            this.txtReadRegister5.Name = "txtReadRegister5";
            this.txtReadRegister5.Size = new System.Drawing.Size(116, 32);
            this.txtReadRegister5.TabIndex = 39;
            this.txtReadRegister5.Text = "D45300";
            // 
            // txtReadRegister4
            // 
            this.txtReadRegister4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadRegister4.Location = new System.Drawing.Point(545, 185);
            this.txtReadRegister4.Multiline = true;
            this.txtReadRegister4.Name = "txtReadRegister4";
            this.txtReadRegister4.Size = new System.Drawing.Size(116, 32);
            this.txtReadRegister4.TabIndex = 37;
            this.txtReadRegister4.Text = "D45200";
            // 
            // txtReadRegister3
            // 
            this.txtReadRegister3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadRegister3.Location = new System.Drawing.Point(545, 137);
            this.txtReadRegister3.Multiline = true;
            this.txtReadRegister3.Name = "txtReadRegister3";
            this.txtReadRegister3.Size = new System.Drawing.Size(116, 32);
            this.txtReadRegister3.TabIndex = 35;
            this.txtReadRegister3.Text = "D45100";
            // 
            // txtReadRegister2
            // 
            this.txtReadRegister2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadRegister2.Location = new System.Drawing.Point(545, 90);
            this.txtReadRegister2.Multiline = true;
            this.txtReadRegister2.Name = "txtReadRegister2";
            this.txtReadRegister2.Size = new System.Drawing.Size(116, 32);
            this.txtReadRegister2.TabIndex = 33;
            this.txtReadRegister2.Text = "D45000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Register";
            // 
            // txtReadRegister1
            // 
            this.txtReadRegister1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadRegister1.Location = new System.Drawing.Point(545, 42);
            this.txtReadRegister1.Multiline = true;
            this.txtReadRegister1.Name = "txtReadRegister1";
            this.txtReadRegister1.Size = new System.Drawing.Size(116, 32);
            this.txtReadRegister1.TabIndex = 29;
            this.txtReadRegister1.Text = "M34000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Result";
            // 
            // FormTestPLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 567);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtReadPlc5);
            this.Controls.Add(this.txtReadPlc4);
            this.Controls.Add(this.txtReadPlc3);
            this.Controls.Add(this.txtReadPlc2);
            this.Controls.Add(this.txtReadPlc1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReadRegister5);
            this.Controls.Add(this.txtReadRegister4);
            this.Controls.Add(this.txtReadRegister3);
            this.Controls.Add(this.txtReadRegister2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReadRegister1);
            this.Controls.Add(this.txtWritePlc5);
            this.Controls.Add(this.txtWritePlc4);
            this.Controls.Add(this.txtWritePlc3);
            this.Controls.Add(this.txtWritePlc2);
            this.Controls.Add(this.txtWritePlc1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWriteValue5);
            this.Controls.Add(this.txtWriteRegister5);
            this.Controls.Add(this.txtWriteValue4);
            this.Controls.Add(this.txtWriteRegister4);
            this.Controls.Add(this.txtWriteValue3);
            this.Controls.Add(this.txtWriteRegister3);
            this.Controls.Add(this.txtWriteValue2);
            this.Controls.Add(this.txtWriteRegister2);
            this.Controls.Add(this.txtWriteValue1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWriteRegister1);
            this.Controls.Add(this.BtnReadFloat);
            this.Controls.Add(this.BtnReadString);
            this.Controls.Add(this.BtnReadDWord);
            this.Controls.Add(this.BtnReadWord);
            this.Controls.Add(this.BtnReadBit);
            this.Controls.Add(this.BtnWriteFloat);
            this.Controls.Add(this.BtnWriteString);
            this.Controls.Add(this.BtnWriteDWord);
            this.Controls.Add(this.BtnWriteWord);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.BtnWriteBit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormTestPLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Test PLC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTestPLC_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnWriteBit;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button BtnWriteWord;
        private System.Windows.Forms.Button BtnWriteDWord;
        private System.Windows.Forms.Button BtnWriteString;
        private System.Windows.Forms.Button BtnWriteFloat;
        private System.Windows.Forms.Button BtnReadFloat;
        private System.Windows.Forms.Button BtnReadString;
        private System.Windows.Forms.Button BtnReadDWord;
        private System.Windows.Forms.Button BtnReadWord;
        private System.Windows.Forms.Button BtnReadBit;
        private System.Windows.Forms.TextBox txtWriteRegister1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWriteValue1;
        private System.Windows.Forms.TextBox txtWriteValue2;
        private System.Windows.Forms.TextBox txtWriteRegister2;
        private System.Windows.Forms.TextBox txtWriteValue3;
        private System.Windows.Forms.TextBox txtWriteRegister3;
        private System.Windows.Forms.TextBox txtWriteValue4;
        private System.Windows.Forms.TextBox txtWriteRegister4;
        private System.Windows.Forms.TextBox txtWriteValue5;
        private System.Windows.Forms.TextBox txtWriteRegister5;
        private System.Windows.Forms.TextBox txtWritePlc5;
        private System.Windows.Forms.TextBox txtWritePlc4;
        private System.Windows.Forms.TextBox txtWritePlc3;
        private System.Windows.Forms.TextBox txtWritePlc2;
        private System.Windows.Forms.TextBox txtWritePlc1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReadPlc5;
        private System.Windows.Forms.TextBox txtReadPlc4;
        private System.Windows.Forms.TextBox txtReadPlc3;
        private System.Windows.Forms.TextBox txtReadPlc2;
        private System.Windows.Forms.TextBox txtReadPlc1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReadRegister5;
        private System.Windows.Forms.TextBox txtReadRegister4;
        private System.Windows.Forms.TextBox txtReadRegister3;
        private System.Windows.Forms.TextBox txtReadRegister2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReadRegister1;
        private System.Windows.Forms.Label label5;
    }
}