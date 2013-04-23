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

        public GrabComponent Grabber
        {
            get { return grabber; }
        }

        private Vector3 grabbingEntityPositionOffset;

        public bool IsGrabbed
        {
            get
            {
                return (grabber != null);
            }
        }

        /// <summary>
        /// Event to be triggered when the entity is thrown
        /// </summary>
        public EventHandler OnThrow;

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
                    MovementComponent moveComponent = ((MovementComponent)Parent.GetComponent("MovementComponent"));
                    if (moveComponent != null)
                    {
                        moveComponent.Enabled = true;
                    }
                    grabber.LoseGrip();
                    grabber = null;
                    this.bepuPhysicsComponent.Box.IsAffectedByGravity = true; // re enable gravity when let go.
                }
                else
                {
                    // Move this entity with the grabbing entity.
                    this.bepuPhysicsComponent.Box.Position = grabber.GetPosition() + grabbingEntityPositionOffset;
                    
                  //  this.bepuPhysicsComponent.Box.LinearVelocity = Vector3.Zero; // Else it gains acceleration from gravity, and when it's dropped, it goes crazy.
                }
            }
        }

        public Box GetGrabbableBox()
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
            Vector3 currentPos = bepuPhysicsComponent.Box.Position;
            this.bepuPhysicsComponent.Box.Position = new Vector3(currentPos.X , grabber.GetPosition().Y, currentPos.Z);
            this.grabbingEntityPositionOffset = this.bepuPhysicsComponent.Box.Position - grabber.GetPosition();
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = false; // disable gravity, else it falls fast when dropped.

            // Disable the move component if the entity has a moveable
            MovementComponent moveComponent = (MovementComponent) Parent.GetComponent("MovementComponent");
            if (moveComponent != null)
            {
                moveComponent.Enabled = false;
            }
            return true;
        }

        public void Throw(Vector2 direction, float throwStrength)
        {
            EventHandler handler = OnThrow;
            if (handler != null)
            {
                handler(this, null);
            }
            
            this.bepuPhysicsComponent.Box.IsAffectedByGravity = true; // reenable gravity when let go.
            // Give movement back.
            MovementComponent moveComponent = (MovementComponent)Parent.GetComponent("MovementComponent");
            if (moveComponent != null)
            {
                moveComponent.Enabled = true;
            }

            // Set the force to throw the entity.
            if (direction.Equals(Vector2.Zero))
            {
                // Throw up in the direction facing.
                int xDir = ((MovementComponent)grabber.Parent.GetComponent("MovementComponent")).DirectionX;
                direction = Vector2.Normalize(new Vector2(xDir, 1));
            }
            else
            {
                direction = Vector2.Normalize(direction);
            }
            Vector2 throwVelocity = throwStrength * direction;
            bepuPhysicsComponent.Box.LinearVelocity += new Vector3(throwVelocity.X, throwVelocity.Y, 0);
            
            // Damage the vitality component when thrown if the entity has vitality
            VitalityComponent health = (VitalityComponent)Parent.GetComponent("VitalityComponent");
            if (health != null)
            {
                health.Damage(5);
            }
            grabber = null;
        }

        
    }
}
