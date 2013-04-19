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
            

            // scaling *= Matrix.CreateRotationX(MathHelper.PiOver2);
            //scaling *= Matrix.CreateRotationY(MathHelper.PiOver2);
          
            // scaling *= Matrix.CreateRotationZ(MathHelper.PiOver2);
            //scaling *= Matrix.

            this.Transform = Matrix.Identity;// Matrix.CreateTranslation(new Vector3(0, 0, -10));
        }
        int xVal = 0;
        int yVal = 0;
        int zVal = 0;
        protected override Matrix GetWorld()
        {
            PlayerIndex outP;
            if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.X, null, out outP))
            {
                xVal++;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }
            else if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.Z, null, out outP))
            {
                zVal++;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }
            else if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.Y, null, out outP))
            {
                yVal++;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }
            else if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.C, null, out outP))
            {
                xVal--;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }
            else if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.V, null, out outP))
            {
                zVal--;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }
            else if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.T, null, out outP))
            {
                yVal--;
                Console.WriteLine("X: " + xVal + " Y: " + yVal + " Z: " + zVal);
            }

            // Matrix transform = Matrix.CreateTranslation(new Vector3(10, -5, 3)) * Matrix.CreateRotationY(-1 *MathHelper.PiOver2) ;
            Matrix transform = Matrix.CreateRotationZ(MathHelper.PiOver2) * Matrix.CreateTranslation(new Vector3(xVal, yVal, zVal));

            return transform * physicsComponent.Box.WorldTransform;
        }

        public override void Start()
        {
            base.Start();
 
            // Get a reference to the transform component
            this.physicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        


    }
}
