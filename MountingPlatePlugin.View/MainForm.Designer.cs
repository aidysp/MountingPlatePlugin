namespace MountingPlatePlugin.View
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
            
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.labelLengthDesc = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelWidthDesc = new System.Windows.Forms.Label();
            this.labelThickness = new System.Windows.Forms.Label();
            this.textBoxThickness = new System.Windows.Forms.TextBox();
            this.labelThicknessDesc = new System.Windows.Forms.Label();
            this.labelHolesLength = new System.Windows.Forms.Label();
            this.textBoxHolesLength = new System.Windows.Forms.TextBox();
            this.labelHolesLengthDesc = new System.Windows.Forms.Label();
            this.labelHolesWidth = new System.Windows.Forms.Label();
            this.textBoxHolesWidth = new System.Windows.Forms.TextBox();
            this.labelHolesWidthDesc = new System.Windows.Forms.Label();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.Location = new System.Drawing.Point(120, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(240, 29);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "МОНТАЖНАЯ ПЛАСТИНА";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(30, 70);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(160, 20);
            this.labelLength.TabIndex = 1;
            this.labelLength.Text = "Длина пластины, мм:";
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(200, 67);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(100, 27);
            this.textBoxLength.TabIndex = 2;
            this.textBoxLength.TextChanged += new System.EventHandler(this.TextBoxLength_TextChanged);
            // 
            // labelLengthDesc
            // 
            this.labelLengthDesc.AutoSize = true;
            this.labelLengthDesc.ForeColor = System.Drawing.Color.Gray;
            this.labelLengthDesc.Location = new System.Drawing.Point(320, 70);
            this.labelLengthDesc.Name = "labelLengthDesc";
            this.labelLengthDesc.Size = new System.Drawing.Size(140, 20);
            this.labelLengthDesc.TabIndex = 3;
            this.labelLengthDesc.Text = "(50-1000 мм)";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(30, 110);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(165, 20);
            this.labelWidth.TabIndex = 4;
            this.labelWidth.Text = "Ширина пластины, мм:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(200, 107);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(100, 27);
            this.textBoxWidth.TabIndex = 5;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.TextBoxWidth_TextChanged);
            // 
            // labelWidthDesc
            // 
            this.labelWidthDesc.AutoSize = true;
            this.labelWidthDesc.ForeColor = System.Drawing.Color.Gray;
            this.labelWidthDesc.Location = new System.Drawing.Point(320, 110);
            this.labelWidthDesc.Name = "labelWidthDesc";
            this.labelWidthDesc.Size = new System.Drawing.Size(120, 20);
            this.labelWidthDesc.TabIndex = 6;
            this.labelWidthDesc.Text = "(30-500 мм)";
            // 
            // labelThickness
            // 
            this.labelThickness.AutoSize = true;
            this.labelThickness.Location = new System.Drawing.Point(30, 150);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Size = new System.Drawing.Size(169, 20);
            this.labelThickness.TabIndex = 7;
            this.labelThickness.Text = "Толщина пластины, мм:";
            // 
            // textBoxThickness
            // 
            this.textBoxThickness.Location = new System.Drawing.Point(200, 147);
            this.textBoxThickness.Name = "textBoxThickness";
            this.textBoxThickness.Size = new System.Drawing.Size(100, 27);
            this.textBoxThickness.TabIndex = 8;
            this.textBoxThickness.TextChanged += new System.EventHandler(this.TextBoxThickness_TextChanged);
            // 
            // labelThicknessDesc
            // 
            this.labelThicknessDesc.AutoSize = true;
            this.labelThicknessDesc.ForeColor = System.Drawing.Color.Gray;
            this.labelThicknessDesc.Location = new System.Drawing.Point(320, 150);
            this.labelThicknessDesc.Name = "labelThicknessDesc";
            this.labelThicknessDesc.Size = new System.Drawing.Size(100, 20);
            this.labelThicknessDesc.TabIndex = 9;
            this.labelThicknessDesc.Text = "(3-50 мм)";
            // 
            // labelHolesLength
            // 
            this.labelHolesLength.AutoSize = true;
            this.labelHolesLength.Location = new System.Drawing.Point(30, 190);
            this.labelHolesLength.Name = "labelHolesLength";
            this.labelHolesLength.Size = new System.Drawing.Size(264, 20);
            this.labelHolesLength.TabIndex = 10;
            this.labelHolesLength.Text = "Количество отверстий по длине:";
            // 
            // textBoxHolesLength
            // 
            this.textBoxHolesLength.Location = new System.Drawing.Point(300, 187);
            this.textBoxHolesLength.Name = "textBoxHolesLength";
            this.textBoxHolesLength.Size = new System.Drawing.Size(100, 27);
            this.textBoxHolesLength.TabIndex = 11;
            this.textBoxHolesLength.TextChanged += new System.EventHandler(this.TextBoxHolesLength_TextChanged);
            // 
            // labelHolesLengthDesc
            // 
            this.labelHolesLengthDesc.AutoSize = true;
            this.labelHolesLengthDesc.ForeColor = System.Drawing.Color.Gray;
            this.labelHolesLengthDesc.Location = new System.Drawing.Point(420, 190);
            this.labelHolesLengthDesc.Name = "labelHolesLengthDesc";
            this.labelHolesLengthDesc.Size = new System.Drawing.Size(80, 20);
            this.labelHolesLengthDesc.TabIndex = 12;
            this.labelHolesLengthDesc.Text = "(2-20)";
            // 
            // labelHolesWidth
            // 
            this.labelHolesWidth.AutoSize = true;
            this.labelHolesWidth.Location = new System.Drawing.Point(30, 230);
            this.labelHolesWidth.Name = "labelHolesWidth";
            this.labelHolesWidth.Size = new System.Drawing.Size(270, 20);
            this.labelHolesWidth.TabIndex = 13;
            this.labelHolesWidth.Text = "Количество отверстий по ширине:";
            // 
            // textBoxHolesWidth
            // 
            this.textBoxHolesWidth.Location = new System.Drawing.Point(300, 227);
            this.textBoxHolesWidth.Name = "textBoxHolesWidth";
            this.textBoxHolesWidth.Size = new System.Drawing.Size(100, 27);
            this.textBoxHolesWidth.TabIndex = 14;
            this.textBoxHolesWidth.TextChanged += new System.EventHandler(this.TextBoxHolesWidth_TextChanged);
            // 
            // labelHolesWidthDesc
            // 
            this.labelHolesWidthDesc.AutoSize = true;
            this.labelHolesWidthDesc.ForeColor = System.Drawing.Color.Gray;
            this.labelHolesWidthDesc.Location = new System.Drawing.Point(420, 230);
            this.labelHolesWidthDesc.Name = "labelHolesWidthDesc";
            this.labelHolesWidthDesc.Size = new System.Drawing.Size(70, 20);
            this.labelHolesWidthDesc.TabIndex = 15;
            this.labelHolesWidthDesc.Text = "(2-10)";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Enabled = false;
            this.buttonBuild.Location = new System.Drawing.Point(180, 280);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(150, 40);
            this.buttonBuild.TabIndex = 16;
            this.buttonBuild.Text = "ПОСТРОИТЬ";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.labelHolesWidthDesc);
            this.Controls.Add(this.textBoxHolesWidth);
            this.Controls.Add(this.labelHolesWidth);
            this.Controls.Add(this.labelHolesLengthDesc);
            this.Controls.Add(this.textBoxHolesLength);
            this.Controls.Add(this.labelHolesLength);
            this.Controls.Add(this.labelThicknessDesc);
            this.Controls.Add(this.textBoxThickness);
            this.Controls.Add(this.labelThickness);
            this.Controls.Add(this.labelWidthDesc);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelLengthDesc);
            this.Controls.Add(this.textBoxLength);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelTitle);
            // Иконка загружается из файла
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Монтажная пластина";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelTitle;
        private Label labelLength;
        private TextBox textBoxLength;
        private Label labelLengthDesc;
        private Label labelWidth;
        private TextBox textBoxWidth;
        private Label labelWidthDesc;
        private Label labelThickness;
        private TextBox textBoxThickness;
        private Label labelThicknessDesc;
        private Label labelHolesLength;
        private TextBox textBoxHolesLength;
        private Label labelHolesLengthDesc;
        private Label labelHolesWidth;
        private TextBox textBoxHolesWidth;
        private Label labelHolesWidthDesc;
        private Button buttonBuild;
    }
}



