using MapEditTool.Interaction3D;
using MapEditTool.ViewModel3D;
using MapEditTool.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapEditTool.View3D
{
    public partial class Map3DView : UserControl
    {
        private CameraController _cameraController;

        public Map3DView()
        {
            InitializeComponent();

            // DataContext は MainViewModel から渡される
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is Map3DViewModel vm)
            {
                _cameraController = new CameraController(vm.Camera);
            }
        }

        private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is Map3DViewModel vm3D)
            {
                if (vm3D.Parent is MainViewModel main)
                {
                    var pos = e.GetPosition(this);
                    main.On3DLeftClick(Viewport, pos);
                }
            }
        }

        private void OnMouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is Map3DViewModel vm3D)
            {
                if (vm3D.Parent is MainViewModel main)
                {
                    var pos = e.GetPosition(this);
                    main.On3DLeftRelease(Viewport, pos);
                }
            }
        }

        private void OnMouseRightDown(object sender, MouseButtonEventArgs e)
            => _cameraController?.OnMouseRightDown(Viewport, e.GetPosition(this));

        private void OnMouseRightUp(object sender, MouseButtonEventArgs e)
            => _cameraController?.OnMouseRightUp(e.GetPosition(this));

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _cameraController?.OnMouseMove(e.GetPosition(this), e);

            if (DataContext is Map3DViewModel vm3D)
            {
                if (vm3D.Parent is MainViewModel main)
                {
                    var pos = e.GetPosition(this);
                    main.On3DMouseMove(Viewport, pos);
                }
            }
        }


        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
            => _cameraController?.OnMouseWheel(e.Delta);
    }
}