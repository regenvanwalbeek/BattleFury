using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleFury.Input;
using BattleFury.Components.Movement;

namespace BattleFury.Components.Animated
{
    public class RobotRenderComponent : BasicModelComponent
    {

        public Matrix Transform;

        private BepuPhysicsComponent physicsComponent;

        private MoveComponent movementComponent;

        private float height;

        private Effect colorEffect;

        public RobotRenderComponent(Entity parent, float height, Color color)
            : base(parent, ContentLoader.Robot)
        {
            this.Transform = Matrix.Identity;// Matrix.CreateTranslation(new Vector3(0, 0, -10));
            this.height = height;
            this.colorEffect = ContentLoader.ColorEffect;

            // Temp fix...
            if (color.Equals(Color.Red))
            {
                this.model = ContentLoader.RobotRed;
            }
            else if (color.Equals(Color.Blue))
            {
                this.model = ContentLoader.RobotBlue;
            }
            else if (color.Equals(Color.Yellow))
            {
                this.model = ContentLoader.RobotYellow;
            }
        }

        protected override Matrix GetWorld()
        {
            // Translation offset. Fixes some screwiness with the model (or maybe I'm just rotating wrong). 
            // I'm sure there's an easier way to come up with this. Anyway, it seems to work. 
            // DON'T FREAKIN BREAK THIS. WAY TOO MUCH TIME WASTED ON THIS.
            Vector3 offset = new Vector3(physicsComponent.Box.Position.X * -.114f + .1f, 
                physicsComponent.Box.Position.Y * .2381f - 1.0f * height / 2 - height / 2, 0);
            float yRotation = 0;
            if (movementComponent.DirectionX == 1)
            {
                yRotation = -1*MathHelper.PiOver4;
            }
            else if (movementComponent.DirectionX == -1)
            {
                yRotation = -3 * MathHelper.PiOver4;
            }

            // Apply the transform. Yikes.
            Matrix transform = Matrix.CreateRotationX(-1  *MathHelper.PiOver2) * Matrix.CreateRotationY(yRotation)
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
            this.movementComponent = (MoveComponent)Parent.GetComponent("MoveComponent");
           
        }
        /*
        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.AmbientLightColor = Color.Gray.ToVector3();
                    effect.DiffuseColor = new Vector3(0, 0, .4f);

                    effect.Projection = projection;
                    effect.View = view;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }*/

       

        


    }
}
