using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;

namespace BattleFury.Components.Movement
{
    /// <summary>
    /// A component which makes an entity grabbable.
    /// </summary>
    public class GrabbableComponent : Component
    {
        /// <summary>
        /// Maximum amount of time this entity can be grabbed.
        /// </summary>
        private const int MAX_GRAB_TIME = 3000;

        /// <summary>
        /// Time since the entity has been picked up.
        /// </summary>
        private int timeSinceGrab;

        /// <summary>
        /// The box that is grabbable.
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        /// <summary>
        /// Entity grabbing this entity
        /// </summary>
        private GrabComponent grabber = null;

        private Vector3 grabbingEntityPositionOffset;

        public bool IsGrabbed
        {
            get
            {
                return (grabber != null);
            }
        }

        public GrabbableComponent(Entity parent)
            : base(parent, "GrabbableComponent")
        {

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
                    // Drop the entity
                    ((MovementComponent)Parent.GetComponent("MovementComponent")).Enabled = true;
                    grabber.LoseGrip();
                    grabber = null;
                }
                else
                {
                    // Move this entity with the grabbing entity.
                    this.bepuPhysicsComponent.Box.Position = grabber.GetPosition() + grabbingEntityPositionOffset;
                    this.bepuPhysicsComponent.Box.LinearVelocity = Vector3.Zero; // Else it gains acceleration from gravity, and when it's dropped, it goes crazy.
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
        /// <param name="grabber">The component grabbing this component</param>
        /// <returns>True if successful grab</returns>
        public bool Grab(GrabComponent grabber)
        {
            if (IsGrabbed)
            {
                // Can't be grabbed twice. Though grab stealing could be a cool feature...
                return false;
            }

            
            timeSinceGrab = 0;
            this.grabber = grabber;
            this.grabbingEntityPositionOffset = this.bepuPhysicsComponent.Box.Position - grabber.GetPosition();

            // Disable the move component
            ((MovementComponent)Parent.GetComponent("MovementComponent")).Enabled = false;
            return true;
        }

        public void Throw(Vector2 direction, float throwStrength)
        {

            grabber = null;
            // Give movement back.
            ((MovementComponent)Parent.GetComponent("MovementComponent")).Enabled = true;

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
            bepuPhysicsComponent.Box.LinearVelocity += new Vector3(throwVelocity.X, throwVelocity.Y, 0);
            
            // Damage the vitality component when thrown.
            VitalityComponent health = (VitalityComponent) Parent.GetComponent("VitalityComponent");
            health.Damage(5);
        }
    }
}
