using System;
using System.Drawing;
using System.Windows.Forms;
using MountingPlatePlugin.Model;

namespace MountingPlatePlugin.View
{
    public partial class MainForm : Form
    {
        private readonly MountingPlateParameters _parameters = new MountingPlateParameters();
        
        // Элементы управления (добавьте в конструкторе)
        private Button buttonBuild;
        private TextBox textBoxLength;
        private TextBox textBoxWidth;
        private TextBox textBoxThickness;
        private TextBox textBoxHolesLength;
        private TextBox textBoxHolesWidth;
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "Монтажная пластина";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Создаем элементы управления
            CreateControls();
            
            // Изначально кнопка заблокирована
            buttonBuild.Enabled = false;
        }
        
        private void CreateControls()
        {
            int yPos = 20;
            int labelWidth = 200;
            int textBoxWidth = 100;
            
            // Заголовок
            var labelTitle = new Label();
            labelTitle.Text = "МОНТАЖНАЯ ПЛАСТИНА";
            labelTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            labelTitle.Location = new Point(150, yPos);
            labelTitle.Size = new Size(250, 30);
            this.Controls.Add(labelTitle);
            
            yPos += 40;
            
            // 1. Длина
            AddParameterControl("Длина (50-1000 мм):", "textBoxLength", ref yPos);
            yPos += 30;
            
            // 2. Ширина
            AddParameterControl("Ширина (30-500 мм):", "textBoxWidth", ref yPos);
            yPos += 30;
            
            // 3. Толщина
            AddParameterControl("Толщина (3-50 мм):", "textBoxThickness", ref yPos);
            yPos += 30;
            
            // 4. Отверстия по длине
            AddParameterControl("Отверстий по длине (2-20):", "textBoxHolesLength", ref yPos);
            yPos += 30;
            
            // 5. Отверстия по ширине
            AddParameterControl("Отверстий по ширине (2-10):", "textBoxHolesWidth", ref yPos);
            yPos += 40;
            
            // Кнопка "Построить"
            buttonBuild = new Button();
            buttonBuild.Text = "ПОСТРОИТЬ";
            buttonBuild.Location = new Point(200, yPos);
            buttonBuild.Size = new Size(100, 30);
            buttonBuild.Click += ButtonBuild_Click;
            this.Controls.Add(buttonBuild);
        }
        
        private void AddParameterControl(string labelText, string textBoxName, ref int yPos)
        {
            // Label
            var label = new Label();
            label.Text = labelText;
            label.Location = new Point(50, yPos);
            label.Size = new Size(200, 25);
            this.Controls.Add(label);
            
            // TextBox
            var textBox = new TextBox();
            textBox.Name = textBoxName;
            textBox.Location = new Point(260, yPos);
            textBox.Size = new Size(100, 25);
            textBox.TextChanged += TextBox_TextChanged;
            this.Controls.Add(textBox);
            
            // Сохраняем ссылку
            switch (textBoxName)
            {
                case "textBoxLength": textBoxLength = textBox; break;
                case "textBoxWidth": textBoxWidth = textBox; break;
                case "textBoxThickness": textBoxThickness = textBox; break;
                case "textBoxHolesLength": textBoxHolesLength = textBox; break;
                case "textBoxHolesWidth": textBoxHolesWidth = textBox; break;
            }
        }
        
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            
            // Фильтрация ввода
            textBox.Text = Validator.TextBoxCheck(textBox.Text);
            textBox.SelectionStart = textBox.Text.Length;
            
            // Валидация
            bool isValid = ValidateTextBox(textBox);
            UpdateBuildButton();
        }
        
        private bool ValidateTextBox(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = Color.White;
                return false;
            }
            
            // Определяем тип параметра по имени TextBox
            if (textBox == textBoxLength || textBox == textBoxWidth || textBox == textBoxThickness)
            {
                if (Validator.IsValidFloat(textBox.Text, out float value))
                {
                    bool isValid = textBox.Name switch
                    {
                        "textBoxLength" => _parameters.TrySetLength(value),
                        "textBoxWidth" => _parameters.TrySetWidth(value),
                        "textBoxThickness" => _parameters.TrySetThickness(value),
                        _ => false
                    };
                    
                    textBox.BackColor = isValid ? Color.LightGreen : Color.LightPink;
                    return isValid;
                }
            }
            else if (textBox == textBoxHolesLength || textBox == textBoxHolesWidth)
            {
                if (Validator.IsValidInt(textBox.Text, out int value))
                {
                    bool isValid = textBox.Name switch
                    {
                        "textBoxHolesLength" => _parameters.TrySetHolesLength(value),
                        "textBoxHolesWidth" => _parameters.TrySetHolesWidth(value),
                        _ => false
                    };
                    
                    textBox.BackColor = isValid ? Color.LightGreen : Color.LightPink;
                    return isValid;
                }
            }
            
            textBox.BackColor = Color.LightPink;
            return false;
        }
        
        private void UpdateBuildButton()
        {
            // Проверяем все TextBox
            bool allValid = true;
            
            var textBoxes = new[] { textBoxLength, textBoxWidth, textBoxThickness, 
                                   textBoxHolesLength, textBoxHolesWidth };
            
            foreach (var textBox in textBoxes)
            {
                if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    allValid = false;
                    break;
                }
                
                if (textBox.BackColor != Color.LightGreen)
                {
                    allValid = false;
                    break;
                }
            }
            
            buttonBuild.Enabled = allValid;
        }
        
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            try
            {
                // Устанавливаем окончательные значения
                _parameters.Length = float.Parse(textBoxLength.Text);
                _parameters.Width = float.Parse(textBoxWidth.Text);
                _parameters.Thickness = float.Parse(textBoxThickness.Text);
                _parameters.HolesLength = int.Parse(textBoxHolesLength.Text);
                _parameters.HolesWidth = int.Parse(textBoxHolesWidth.Text);
                
                // Проверяем все параметры
                if (!_parameters.ValidateAll())
                {
                    MessageBox.Show("Ошибка: параметры не прошли валидацию!", 
                                  "Ошибка", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Error);
                    return;
                }
                
                // Успешное сообщение
                MessageBox.Show(
                    $"Параметры успешно установлены!\n\n" +
                    $"Длина: {_parameters.Length} мм\n" +
                    $"Ширина: {_parameters.Width} мм\n" +
                    $"Толщина: {_parameters.Thickness} мм\n" +
                    $"Отверстия: {_parameters.HolesLength}×{_parameters.HolesWidth} = {_parameters.TotalHoles} шт.\n" +
                    $"Диаметр отверстий: {_parameters.HoleDiameter:F1} мм\n" +
                    $"Отступ от края: {_parameters.EdgeOffset:F1} мм",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", 
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}


