using System;
using Microsoft.Xna.Framework;
using BattleFury.EntitySystem;
using BattleFury.Settings;

namespace BattleFury.Components.CameraComponents
{
    public class ViewProjectionComponent : Component
    {
        /// <summary>
        /// Field of view in degrees.
        /// </summary>
        private const int FOV = 60;
        private const int NEAR_PLANE_DISTANCE = 1;

        private const int FAR_PLANE_DISTANCE = 3000;

        private Vector3 startPosition;
        private Vector3 startDirection;
        private Vector3 startUp;


        public float MoveSpeed = 1.0f;

        public float RotationSpeed = .05f;

        /// <summary>
        /// The camera's view matrix. Contains the camera's position, the camera's target, and the 's up vector.
        /// </summary>
        public Matrix View { get; protected set; }

        /// <summary>
        /// The camera's projection matrix. Contains the camera's field of view, aspect ratio, and near and far planes.
        /// </summary>
        public Matrix Projection { get; protected set; }

        public Vector3 Position { get; protected set; }

        public Vector3 Direction { get; protected set; }

        public Vector3 Up { get; protected set; }

        

        public ViewProjectionComponent(Entity parent, Vector3 position, Vector3 target, Vector3 up)
            : base(parent, "ViewProjectionComponent")
        {
            this.View = Matrix.CreateLookAt(position, target, up);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), 
                (float) GameSettings.WindowWidth / GameSettings.WindowHeight, NEAR_PLANE_DISTANCE, FAR_PLANE_DISTANCE);

            this.Position = position;
            this.Direction = Vector3.Normalize(target - position);
            this.Up = Vector3.Normalize(up);

            this.startPosition = Position;
            this.startDirection = Direction;
            this.startUp = Up;


        }

        public override void Initialize()
        {
            // initialize the component
        }

        public override void Start()
        {
            // gather references to other components.
        }

        public override void Update(GameTime gameTime)
        {
            // update the view matrix
            View = Matrix.CreateLookAt(Position, Position + Direction, Up);
        }

        public void Reset()
        {
            this.Position = startPosition;
            this.Direction = startDirection;
            this.Up = startUp;
        }


        public void MoveForward()
        {
            Position += MoveSpeed * Direction;
        }

        public void MoveBackward()
        {
            Position -= MoveSpeed * Direction;
        }
        
        public void MoveLeft()
        {
            Position -= MoveSpeed * Vector3.Cross(Direction, Up);
        }
        
        public void MoveRight()
        {
            Position += MoveSpeed * Vector3.Cross(Direction, Up);
        }

        public void LookUp()
        {
            float pitchRotation = RotationSpeed;
            if (Position.Z < 0)
            {
                pitchRotation *= -1;
            }
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(0, pitchRotation, 0);
            Direction = Vector3.Transform(Direction, Matrix.CreateFromQuaternion(rotation));
            Up = Vector3.Transform(Up, Matrix.CreateFromQuaternion(rotation));
        }

        public void LookDown(){
            float pitchRotation = -RotationSpeed;
            if (Position.Z < 0)
            {
                pitchRotation *= -1;
            }
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(0, pitchRotation, 0);
            Direction = Vector3.Transform(Direction, Matrix.CreateFromQuaternion(rotation));
            Up = Vector3.Transform(Up, Matrix.CreateFromQuaternion(rotation));
        }

        public void LookLeft(){
            float yawRotation = RotationSpeed;
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(yawRotation, 0, 0);
            Direction = Vector3.Transform(Direction, Matrix.CreateFromQuaternion(rotation));
        }

        public void LookRight()
        {
            float yawRotation = -RotationSpeed;
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(yawRotation, 0, 0);
            Direction = Vector3.Transform(Direction, Matrix.CreateFromQuaternion(rotation));
        }
  
    }
}
