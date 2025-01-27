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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Abre a janela do conversor binário
        private void OpenBinaryConverter_Click(object sender, RoutedEventArgs e)
        {
            var binaryConverterWindow = new BinaryConverterWindow();
            binaryConverterWindow.Show();
            this.Close(); // Fecha a janela principal
        }

        // Abre a janela da calculadora
        private void OpenCalculator_Click(object sender, RoutedEventArgs e)
        {
            var calculatorWindow = new CalculatorWindow();
            calculatorWindow.Show();
            this.Close(); // Fecha a janela principal
        }
    }
}
