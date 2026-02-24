using MapEditTool.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MapEditTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

    }
}