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
    public partial class BinaryConverterWindow : Window
    {
        public BinaryConverterWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BinaryInput.Text == "Insira até 8 dígitos binários")
            {
                BinaryInput.Text = string.Empty;
                BinaryInput.Foreground = Brushes.Black; // Muda a cor do texto para preto
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BinaryInput.Text))
            {
                BinaryInput.Text = "Insira até 8 dígitos binários";
                BinaryInput.Foreground = Brushes.Gray; // Muda a cor do texto para cinza
            }
        }

        // Método chamado quando o botão é clicado
        private void BinaryInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica se a tecla pressionada é o Enter
            if (e.Key == Key.Enter)
            {
                // Chama o método de conversão
                ConvertBinaryToDecimal();
            }
        }

        private void ConvertBinaryToDecimal()
        {
            string binary = BinaryInput.Text;

            // Verifica se o valor é válido
            if (string.IsNullOrWhiteSpace(binary) || binary.Any(c => c != '0' && c != '1'))
            {
                MessageBox.Show("Por favor, insira apenas dígitos binários (0 ou 1).", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Realiza a conversão
            int decimalValue = Convert.ToInt32(binary, 2);

            // Exibe o resultado
            ResultText.Text = $"Decimal: {decimalValue}";
            ResultText.Visibility = Visibility.Visible;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            // Usa o mesmo método ao clicar no botão
            ConvertBinaryToDecimal();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Fecha a janela atual
        }

    }
}