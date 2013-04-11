using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;

namespace BattleFury.Components.Movement
{
    public class GrabbableComponent : Component
    {
        /// <summary>
        /// The box that is grabbable.
        /// </summary>
        BepuPhysicsComponent bepuPhysicsComponent;

        public bool IsGrabbed { get; private set; }

        public GrabbableComponent(Entity parent)
            : base(parent, "GrabbableComponent")
        {
            IsGrabbed = false;
        }



        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
       
        }

        public Box getGrabbableBox()
        {
            return this.bepuPhysicsComponent.Box;
        }

        /// <summary>
        /// Grabs an entity. Returns true if the grab is successful. False otherwise.
        /// </summary>
        /// <returns></returns>
        public bool Grab()
        {
            if (IsGrabbed)
            {
                return false;
            }
            IsGrabbed = true;
            return true;
        }

        public void Throw(Vector2 direction, float throwStrength)
        {
            IsGrabbed = false;

            if (direction.Equals(Vector2.Zero))
            {
                // TODO for now, throw up right. Should throw up in the direction facing.
                direction = Vector2.Normalize(new Vector2(1, 1));
            }
            else
            {
                direction = Vector2.Normalize(direction);
            }
            Vector2 throwVelocity = throwStrength * direction;
            Console.WriteLine("Throw Velocity " + throwVelocity);
            bepuPhysicsComponent.Box.LinearVelocity = new Vector3(throwVelocity.X, throwVelocity.Y, 10);
            Console.WriteLine("Box Linear velocity " + bepuPhysicsComponent.Box.LinearVelocity);
           
        }
    }
}
