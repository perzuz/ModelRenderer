using System.Windows;
using Renderer.ViewModels;

namespace Renderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new RenderViewModel();
            InitializeComponent();
        }
    }
}
