using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using Renderer.Annotations;

namespace Renderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string MODEL_PATH = @"C:\Users\hs140219\source\repos\Renderer\Stanford_Bunny_sample.stl";

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(MODEL_PATH);
            
            // Add to view port
            viewPort3d.Children.Add(device3D);
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        /// <summary>
        /// Display 3D Model
        /// </summary>
        /// <param name="model">Path to the Model file</param>
        /// <returns>3D Model Content</returns>
        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                import.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.SlateGray));

                //Load the 3D model file
                device = import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not find the 3D model file
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }

        // Private
        private string _filePath;

        private void BtnLoadSTL_Click(object sender, RoutedEventArgs e)
        {
            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(FilePath);

            // Add to view port
            viewPort3d.Children.RemoveAt(0);
            viewPort3d.Children.Add(device3D);
        }

        private void BtnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == false) return;

            FilePath = ofd.FileName;
        }

        // Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
