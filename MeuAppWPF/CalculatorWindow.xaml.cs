using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeuAppWPF
{
    public partial class CalculatorWindow : Window
    {
        private double _currentValue = 0;  // Valor atual
        private string _currentOperator = string.Empty;  // Operador atual
        private bool _isNewInput = true;  // Indica se o próximo input deve ser tratado como novo

        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void CalculatorWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar se a tecla é um número (0-9)
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string number = (e.Key - Key.D0).ToString();
                if (_isNewInput)
                {
                    Display.Text = number;
                    _isNewInput = false;
                }
                else
                {
                    Display.Text += number;
                }
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) // Números no teclado numérico
            {
                string number = (e.Key - Key.NumPad0).ToString();
                if (_isNewInput)
                {
                    Display.Text = number;
                    _isNewInput = false;
                }
                else
                {
                    Display.Text += number;
                }
            }
            // Verificar operadores
            else if (e.Key == Key.Add || e.Key == Key.OemPlus) // Soma
            {
                SetOperator("+");
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus) // Subtração
            {
                SetOperator("-");
            }
            else if (e.Key == Key.Multiply) // Multiplicação
            {
                SetOperator("*");
            }
            else if (e.Key == Key.Divide || e.Key == Key.OemQuestion) // Divisão
            {
                SetOperator("/");
            }
            // Verificar tecla Enter para calcular o resultado
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Calculate();
            }
            // Limpar (tecla Escape)
            else if (e.Key == Key.Escape)
            {
                ResetCalculator();
            }
        }

        // Método auxiliar para configurar o operador
        private void SetOperator(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(Display.Text))
            {
                if (!_isNewInput)
                {
                    Calculate(); // Calcula o valor atual antes de aplicar o próximo operador
                }

                _currentOperator = operatorSymbol;
                _currentValue = double.Parse(Display.Text); // Atualiza o valor atual
                _isNewInput = true;
            }
        }

        // Método auxiliar para redefinir a calculadora
        private void ResetCalculator()
        {
            _currentValue = 0;
            _currentOperator = "";
            Display.Text = "0";
            _isNewInput = true;
        }


        // Evento para botões numéricos
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null) // Verifique se o sender é realmente um Button
            {
                string number = button.Content.ToString();

                if (_isNewInput)
                {
                    Display.Text = number; // Substitui o valor exibido
                    _isNewInput = false;
                }
                else
                {
                    Display.Text += number; // Adiciona ao valor exibido
                }
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null) // Verifique se o sender é realmente um Button
            {
                string operatorSymbol = button.Content.ToString();

                if (!string.IsNullOrEmpty(Display.Text))
                {
                    if (!_isNewInput)
                    {
                        Calculate(); // Calcula o valor atual antes de aplicar o próximo operador
                    }

                    _currentOperator = operatorSymbol;
                    _currentValue = double.Parse(Display.Text); // Atualiza o valor atual
                    _isNewInput = true;
                }
            }
        }

        private string NormalizeOperator(string op)
        {
            switch (op)
            {
                case "x": return "*";
                case "÷": return "/";
                default: return op;
            }
        }

        // Evento para o botão "="
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            _currentOperator = NormalizeOperator(_currentOperator);
            Calculate();
            _currentOperator = string.Empty; // Reseta o operador atual
            _isNewInput = true;
        }

        // Evento para o botão "C" (limpar)
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _currentValue = 0;
            _currentOperator = string.Empty;
            Display.Text = "0";
            _isNewInput = true;
        }
        // Método para calcular o valor atual
        private void Calculate()
        {
            if (!string.IsNullOrEmpty(_currentOperator) && !string.IsNullOrEmpty(Display.Text))
            {
                double newValue = double.Parse(Display.Text);

                switch (_currentOperator)
                {
                    case "+":
                        _currentValue += newValue;
                        break;
                    case "-":
                        _currentValue -= newValue;
                        break;
                    case "*":
                        _currentValue *= newValue;
                        break;
                    case "/":
                        if (newValue != 0)
                        {
                            _currentValue /= newValue;
                        }
                        else
                        {
                            MessageBox.Show("Divisão por zero não é permitida!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        break;
                }
                Display.Text = _currentValue.ToString(); // Exibe o resultado
            }
        }

        // Evento para voltar ao menu principal
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
