using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Entities;

namespace BattleFury.Components.CameraComponents
{
    /// <summary>
    /// Provides access to the camera for a model to be drawn.
    /// </summary>
    public class DrawableByCameraComponent : Component
    {

        public Camera Camera { get; set; }

        public DrawableByCameraComponent(Camera camera, Entity parent)
            : base(parent, "DrawableByCameraComponent")
        {
            this.Camera = camera;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

    }
}
