using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Input;

namespace BattleFury.Components.Animated
{
    public class RobotRenderComponent : BasicModelComponent
    {

        public Matrix Transform;

        private BepuPhysicsComponent physicsComponent;

        public RobotRenderComponent(Entity parent)
            : base(parent, ContentLoader.Robot)
        {
            this.Transform = Matrix.Identity;// Matrix.CreateTranslation(new Vector3(0, 0, -10));
        }

        protected override Matrix GetWorld()
        {
            // Translation offset. Fixes some screwiness with the model (or maybe I'm just rotating wrong). 
            // I'm sure there's an easier way to come up with this. Anyway, it seems to work. 
            // DON'T FREAKIN BREAK THIS. WAY TOO MUCH TIME WASTED ON THIS.
            Vector3 offset = new Vector3(physicsComponent.Box.Position.X * -.114f + .1f, physicsComponent.Box.Position.Y * .2381f - 1.0f, 0);

            // Apply the transform. Yikes.
            Matrix transform = Matrix.CreateRotationX(-1*MathHelper.PiOver2) 
                * Matrix.CreateTranslation(physicsComponent.Box.Position + offset) 
                * physicsComponent.Box.WorldTransform * Matrix.CreateRotationZ(-1 * MathHelper.PiOver2) 
                * Matrix.CreateRotationY(-1*MathHelper.PiOver2);

            return transform;
        }

        public override void Start()
        {
            base.Start();
 
            // Get a reference to the transform component
            this.physicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        


    }
}
