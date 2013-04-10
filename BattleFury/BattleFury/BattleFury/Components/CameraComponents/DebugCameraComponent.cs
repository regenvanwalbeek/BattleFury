using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BattleFury.Settings;
using BattleFury.Input;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.CameraComponents
{

    public class DebugCameraComponent : Component
    {

        private ViewProjectionComponent viewProjection;

        public DebugCameraComponent(Entity parent)
            : base(parent, "DebugCameraComponent")
        {
        
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            viewProjection = (ViewProjectionComponent)Parent.GetComponent("ViewProjectionComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (DebugBindings.IsCameraForward(null))
            {
                viewProjection.MoveForward();
            }
            if (DebugBindings.IsCameraBackward(null))
            {
                viewProjection.MoveBackward();
            }
            if (DebugBindings.IsCameraLeft(null))
            {
                viewProjection.MoveLeft();
            }
            if (DebugBindings.IsCameraRight(null))
            {
                viewProjection.MoveRight();
            }
            
            if (DebugBindings.IsCameraLookLeft(null))
            {
                viewProjection.LookLeft();
            }
            if (DebugBindings.IsCameraLookRight(null))
            {
                viewProjection.LookRight();
            }
            if (DebugBindings.IsCameraLookDown(null))
            {
                viewProjection.LookDown();
            }
            if (DebugBindings.IsCameraLookUp(null))
            {
                viewProjection.LookUp();
            }
            // viewProjection.Look(InputState.GetMouseDeltaX(), InputState.GetMouseDeltaY());

        }

    }
}
