using System.Windows.Media.Media3D;

namespace MapEditTool.ViewModels
{
    public class CameraViewModel : ViewModelBase
    {
        private Point3D _target = new Point3D(10, 0, 10);
        public Point3D Target
        {
            get => _target;
            set { SetProperty(ref _target, value); UpdateCamera(); }
        }

        private double _distance = 20;
        public double Distance
        {
            get => _distance;
            set { SetProperty(ref _distance, value); UpdateCamera(); }
        }

        private double _yaw = 225;
        public double Yaw
        {
            get => _yaw;
            set { SetProperty(ref _yaw, value); UpdateCamera(); }
        }

        private double _pitch = 30;
        public double Pitch
        {
            get => _pitch;
            set { SetProperty(ref _pitch, value); UpdateCamera(); }
        }

        public Point3D Position { get; private set; }
        public Vector3D LookDirection { get; private set; }
        public Vector3D UpDirection { get; private set; } = new Vector3D(0, 1, 0);
        public double FieldOfView { get; private set; } = 45;

        public CameraViewModel()
        {
            UpdateCamera();
        }

        public void InitilizeCamera()
        {
            Target = new Point3D(10, 0, 10);
            Distance = 20;
            Yaw = 225;
            Pitch = 30;
        }

        private void UpdateCamera()
        {
            double yawRad = Yaw * Math.PI / 180;
            double pitchRad = Pitch * Math.PI / 180;

            Position = new Point3D(
                Target.X + Distance * Math.Cos(pitchRad) * Math.Cos(yawRad),
                Target.Y + Distance * Math.Sin(pitchRad),
                Target.Z + Distance * Math.Cos(pitchRad) * Math.Sin(yawRad)
            );

            LookDirection = Target - Position;

            RaisePropertyChanged(nameof(Position));
            RaisePropertyChanged(nameof(LookDirection));
        }
    }

}
