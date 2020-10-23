using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using Renderer.Annotations;
using Renderer.ViewModels.Utiltiies;

namespace Renderer.ViewModels
{
    public class RenderViewModel : INotifyPropertyChanged
    {
        /* ************************************************************************************** */
        public RenderViewModel()
        {
            //Import 3D model file
            _import = new ModelImporter
            {
                DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.SlateGray))
            };

            CurrentModel = Display3d(MODEL_PATH);
        }

        /* ************************************************************************************** */
        // Public 
        /* ************************************************************************************** */
        public Model3D CurrentModel
        {
            get => _currentModel;
            set
            {
                _currentModel = value;
                OnPropertyChanged(nameof(CurrentModel));
            }
        }

        /* ************************************************************************************** */
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        /* ************************************************************************************** */
        public ICommand BrowseFileCommand => new RelayCommand(BrowseFile);

        /* ************************************************************************************** */
        // Private
        /* ************************************************************************************** */
        private const string MODEL_PATH = @"C:\Users\hs140219\source\repos\Renderer\Stanford_Bunny_sample.stl";

        private string _filePath;
        private ModelImporter _import;
        private Model3D _currentModel;

        /* ************************************************************************************** */
        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Load the 3D model file
                device = _import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not find the 3D model file
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }

        /* ************************************************************************************** */
        private void BrowseFile()
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == false) return;

            FilePath = ofd.FileName;

            LoadModel();
        }

        /* ************************************************************************************** */
        private void LoadModel()
        {
            CurrentModel = Display3d(FilePath);

            //TODO: use suggestions from here: https://stackoverflow.com/questions/60415033/helixtoolkit-zoomextentswhenloaded-and-binding/62408094#62408094 to zoom to extents in an MVVM way
            // viewPort3d.ZoomExtents(CurrentModel.Bounds);
        }

        /* ************************************************************************************** */
        // Property Changed
        /* ************************************************************************************** */
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /* ************************************************************************************** */
    }
}
