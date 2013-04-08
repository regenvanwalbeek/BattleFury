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


        /// <summary>
        /// The camera's view matrix. Contains the camera's position, the camera's target, and the 's up vector.
        /// </summary>
        public Matrix View { get; protected set; }

        /// <summary>
        /// The camera's projection matrix. Contains the camera's field of view, aspect ratio, and near and far planes.
        /// </summary>
        public Matrix Projection { get; protected set; }

        public ViewProjectionComponent(Entity parent, Vector3 position, Vector3 target, Vector3 up)
            : base(parent, "ViewProjectionComponent")
        {
            this.View = Matrix.CreateLookAt(position, target, up);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), 
                (float) GameSettings.WindowWidth / GameSettings.WindowHeight, NEAR_PLANE_DISTANCE, FAR_PLANE_DISTANCE);
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
            // TODO
            // for now, do nothing!
        }

    }
}
