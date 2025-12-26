using System;
using System.Drawing;
using System.Windows.Forms;
using MountingPlatePlugin.Model;
using MountingPlatePlugin.Builder;
namespace MountingPlatePlugin.View
{
    public partial class MainForm : Form
    {
        private readonly MountingPlateParameters _parameters = new MountingPlateParameters();
        
        public MainForm()
        {
            InitializeComponent();
            buttonBuild.Enabled = false;
        }
        
        private void TextBoxLength_TextChanged(object sender, EventArgs e)
        {
            textBoxLength.Text = Validator.TextBoxCheck(textBoxLength.Text);
            textBoxLength.SelectionStart = textBoxLength.Text.Length;
            ValidateLength();
        }
        
        private void TextBoxWidth_TextChanged(object sender, EventArgs e)
        {
            textBoxWidth.Text = Validator.TextBoxCheck(textBoxWidth.Text);
            textBoxWidth.SelectionStart = textBoxWidth.Text.Length;
            ValidateWidth();
        }
        
        private void TextBoxThickness_TextChanged(object sender, EventArgs e)
        {
            textBoxThickness.Text = Validator.TextBoxCheck(textBoxThickness.Text);
            textBoxThickness.SelectionStart = textBoxThickness.Text.Length;
            ValidateThickness();
        }
        
        private void TextBoxHolesLength_TextChanged(object sender, EventArgs e)
        {
            textBoxHolesLength.Text = Validator.TextBoxCheck(textBoxHolesLength.Text);
            textBoxHolesLength.SelectionStart = textBoxHolesLength.Text.Length;
            ValidateHolesLength();
        }
        
        private void TextBoxHolesWidth_TextChanged(object sender, EventArgs e)
        {
            textBoxHolesWidth.Text = Validator.TextBoxCheck(textBoxHolesWidth.Text);
            textBoxHolesWidth.SelectionStart = textBoxHolesWidth.Text.Length;
            ValidateHolesWidth();
        }
        
        private void ValidateLength()
        {
            bool isValid = ValidateFloatInput(textBoxLength, "Length");
            UpdateBuildButton();
        }
        
        private void ValidateWidth()
        {
            bool isValid = ValidateFloatInput(textBoxWidth, "Width");
            UpdateBuildButton();
        }
        
        private void ValidateThickness()
        {
            bool isValid = ValidateFloatInput(textBoxThickness, "Thickness");
            UpdateBuildButton();
        }
        
        private void ValidateHolesLength()
        {
            bool isValid = ValidateIntInput(textBoxHolesLength, "HolesLength");
            UpdateBuildButton();
        }
        
        private void ValidateHolesWidth()
        {
            bool isValid = ValidateIntInput(textBoxHolesWidth, "HolesWidth");
            UpdateBuildButton();
        }
        
        private bool ValidateFloatInput(TextBox textBox, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = Color.White;
                return false;
            }
            
            if (Validator.IsValidFloat(textBox.Text, out float value))
            {
                bool isValid = parameterName switch
                {
                    "Length" => _parameters.TrySetLength(value),
                    "Width" => _parameters.TrySetWidth(value),
                    "Thickness" => _parameters.TrySetThickness(value),
                    _ => false
                };
                
                textBox.BackColor = isValid ? Color.LightGreen : Color.LightPink;
                return isValid;
            }
            
            textBox.BackColor = Color.LightPink;
            return false;
        }
        
        private bool ValidateIntInput(TextBox textBox, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = Color.White;
                return false;
            }
            
            if (Validator.IsValidInt(textBox.Text, out int value))
            {
                bool isValid = parameterName switch
                {
                    "HolesLength" => _parameters.TrySetHolesLength(value),
                    "HolesWidth" => _parameters.TrySetHolesWidth(value),
                    _ => false
                };
                
                textBox.BackColor = isValid ? Color.LightGreen : Color.LightPink;
                return isValid;
            }
            
            textBox.BackColor = Color.LightPink;
            return false;
        }
        
        private void UpdateBuildButton()
        {
            bool allValid = 
                ValidateFloatInput(textBoxLength, "Length") &&
                ValidateFloatInput(textBoxWidth, "Width") &&
                ValidateFloatInput(textBoxThickness, "Thickness") &&
                ValidateIntInput(textBoxHolesLength, "HolesLength") &&
                ValidateIntInput(textBoxHolesWidth, "HolesWidth");
            
            buttonBuild.Enabled = allValid;
            
            // Дополнительная проверка зависимостей
            if (allValid)
            {
                try
                {
                    float diameter = _parameters.HoleDiameter;
                    float offset = _parameters.EdgeOffset;
                    
                    if (diameter < Constants.MIN_HOLE_DIAMETER || offset < diameter / 2)
                    {
                        buttonBuild.Enabled = false;
                    }
                }
                catch
                {
                    buttonBuild.Enabled = false;
                }
            }
        }
        
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            try
            {
                // Устанавливаем параметры из формы
                _parameters.Length = float.Parse(textBoxLength.Text);
                _parameters.Width = float.Parse(textBoxWidth.Text);
                _parameters.Thickness = float.Parse(textBoxThickness.Text);
                _parameters.HolesLength = int.Parse(textBoxHolesLength.Text);
                _parameters.HolesWidth = int.Parse(textBoxHolesWidth.Text);
                
                // Проверяем валидацию
                if (!_parameters.ValidateAll())
                {
                    MessageBox.Show("Ошибка: параметры не прошли валидацию!", 
                                  "Ошибка", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Error);
                    return;
                }
                
                // Сохраняем параметры в Builder
                MountingPlatePlugin.Builder.MountingPlateBuilder.CurrentParameters = _parameters;
                
                // Закрываем форму с DialogResult.OK
                this.DialogResult = DialogResult.OK;
                this.Close();
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
