using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Components.CameraComponents;
using Microsoft.Xna.Framework;


namespace BattleFury.Entities
{
    public class Camera : Entity
    {
        public ViewProjectionComponent ViewProjection;

        public Camera()
            : base("Camera")
        {
            this.ViewProjection = new ViewProjectionComponent(this, new Vector3(0, 10, 40), Vector3.Zero, Vector3.Up);
            this.AttachComponent(ViewProjection);
        }
    }
}
