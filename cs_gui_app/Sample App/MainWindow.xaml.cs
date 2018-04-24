using System.Windows;

namespace TrackPlatform.App.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = DataContext as MainWindowViewModel;

            //Dispose resources
            Dispatcher.ShutdownStarted += (sender, args) =>
            {
                _vm.Dispose();
            };
        }

        private void OnConnectClicked(object sender, RoutedEventArgs e)
        {
            if (!_vm.ToggleConnection())
            {
                MessageBox.Show("Cannot connect or disconnect from device");
            }
        }
    }
}
