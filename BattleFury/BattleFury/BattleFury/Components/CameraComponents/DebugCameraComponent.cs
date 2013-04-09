using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;

namespace BattleFury.Components.CameraComponents
{

    public class DebugCameraComponent : Component
    {

        ViewProjectionComponent cameraViewProjection;

        public DebugCameraComponent(Entity parent)
            : base(parent, "DebugCameraComponent")
        {
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            cameraViewProjection = (ViewProjectionComponent)Parent.GetComponent("ViewProjectionComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // TODO update the view and projection of the parent camera based on user input
            throw new NotImplementedException();
        }
    }
}
