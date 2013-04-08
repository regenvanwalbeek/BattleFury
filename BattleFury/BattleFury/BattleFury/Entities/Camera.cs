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

        public Camera()
            : base("Camera")
        {
            this.AttachComponent(new ViewProjectionComponent(this, new Vector3(0, 0, -10), Vector3.Zero, Vector3.Up));
        }
    }
}
