using System.Windows;

namespace test_register_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void Console_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Console.ScrollToEnd();
        }
    }
}
