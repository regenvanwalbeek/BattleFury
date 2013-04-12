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
        private const int MAX_GRAB_TIME = 3000;

        private int timeSinceGrab;

        /// <summary>
        /// The box that is grabbable.
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        public bool IsGrabbed { get; private set; }

        private MovementComponent grabbedEntityMoveComponent;

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
            // If the entity is grabbed, move it with the grabbing entity.
            // Also, drop the grab after 3 seconds.
            if (IsGrabbed)
            {
                timeSinceGrab += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceGrab >= MAX_GRAB_TIME)
                {

                    // Drop the entity. TODO
                }
                else
                {
                    // TODO move with grabbing entity
                }
            }
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
                // Can't be grabbed twice. Though grab stealing could be a cool feature...
                return false;
            }

            IsGrabbed = true;
            // Disable the move component
            grabbedEntityMoveComponent =(MovementComponent) Parent.GetComponent("MovementComponent");
            Parent.DetachComponent(grabbedEntityMoveComponent);

            return true;
        }

        public void Throw(Vector2 direction, float throwStrength)
        {
            
            IsGrabbed = false;
            // Give movement back.
            Parent.AttachComponent(grabbedEntityMoveComponent);

            // Set the force to throw the entity.
            if (direction.Equals(Vector2.Zero))
            {
                // TODO for now, throw up right. Should throw up in the direction facing. (maybe get the bepubox linear velocity x)
                direction = Vector2.Normalize(new Vector2(1, 1));
            }
            else
            {
                direction = Vector2.Normalize(direction);
            }
            Vector2 throwVelocity = throwStrength * direction;
            Console.WriteLine("Throw Velocity " + throwVelocity);
            bepuPhysicsComponent.Box.LinearVelocity += new Vector3(throwVelocity.X, throwVelocity.Y, 10);
            Console.WriteLine("Box Linear velocity " + bepuPhysicsComponent.Box.LinearVelocity);
           
        }
    }
}
