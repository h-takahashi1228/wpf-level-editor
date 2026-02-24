using MapEditTool.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.Interaction3D
{
    public class CameraController
    {
        private readonly CameraViewModel _camera;
        private bool _isRightMouseDown = false;

        private Point _lastPos;

        public CameraController(CameraViewModel camera)
        {
            _camera = camera;
            CompositionTarget.Rendering += OnRendering;
        }
        public void OnMouseRightDown(Viewport3D viewport, Point pos)
        {
            _lastPos = pos;
            _isRightMouseDown = true;
            Mouse.Capture(viewport);
        }
        public void OnMouseRightUp(Point pos)
        {
            _isRightMouseDown = false;
            Mouse.Capture(null);
        }

        public void OnMouseMove(Point pos, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Point current = pos;
                Vector delta = current - _lastPos;
                _lastPos = current;
                _camera.Yaw += delta.X * 0.1;
                _camera.Pitch -= delta.Y * 0.1;
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                return;
            }
        }

        public void OnMouseWheel(int delta)
        {
            var distance = _camera.Distance;
            distance -= delta * 0.01;
            _camera.Distance = distance;
        }

        private void OnRendering(object? sender, EventArgs e)
        {
            if (!_isRightMouseDown)
                return;

            HandleKeyboardMovement();
        }
        private void HandleKeyboardMovement()
        {
            if (!_isRightMouseDown)
                return;

            double speed = 0.2;

            double yawRad = _camera.Yaw * Math.PI / 180.0;

            // ★ CameraViewModel の座標系に合わせた forward/right
            Vector3D forward = new Vector3D(
                -Math.Cos(yawRad),
                0,
                -Math.Sin(yawRad)
            );

            Vector3D right = new Vector3D(
                Math.Sin(yawRad),
                0,
                -Math.Cos(yawRad)
            );

            Vector3D move = new Vector3D();

            if (Keyboard.IsKeyDown(Key.W))
                move += forward;

            if (Keyboard.IsKeyDown(Key.S))
                move -= forward;

            if (Keyboard.IsKeyDown(Key.A))
                move -= right;

            if (Keyboard.IsKeyDown(Key.D))
                move += right;

            if (move.LengthSquared > 0)
            {
                move.Normalize();
                move *= speed;

                _camera.Target = new Point3D(
                    _camera.Target.X + move.X,
                    _camera.Target.Y,
                    _camera.Target.Z + move.Z
                );
            }
        }
    }
}