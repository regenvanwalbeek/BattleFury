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
    public class PunchableComponent : Component
    {

        /// <summary>
        /// The box that is punchable.
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        private float minFlinchDistance;

        private float maxFlinchDistance;

        public PunchableComponent(Entity parent, float minFlinchDistance, float maxFlinchDistance)
            : base(parent, "PunchableComponent")
        {
            this.minFlinchDistance = minFlinchDistance;
            this.maxFlinchDistance = maxFlinchDistance;
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

        /// <summary>
        /// Punches this punchable object
        /// </summary>
        /// <param name="puncher">The puncher doing the punching</param>
        /// <param name="damage">The amount of damage to deal</param>
        /// <param name="direction">The direction the player wants to punch in.</param>
        public void Punch(PunchComponent puncher, float damage, Vector3 direction)
        {
            // Calculate the flinch value. This determines how far the character will be knocked back
            VitalityComponent health = (VitalityComponent)Parent.GetComponent("VitalityComponent");
            float flinch = minFlinchDistance;
            if (health != null){
                flinch = minFlinchDistance + ((maxFlinchDistance - minFlinchDistance) / 100) * (100 - health.RageMeter);
                if (health.RageMeter == 0)
                {
                    // RAGE MODE
                    flinch *= 4;
                }
            }

            // Calculate the direction to send the entity flying at.
            // This is a combination of user input direction and the positions of the entities
            if (direction.X != 0 && direction.Y != 0 && direction.Z != 0)
            {
                direction = Vector3.Normalize(direction);

            }
            direction = Vector3.Normalize((direction + (Vector3.Normalize(this.bepuPhysicsComponent.Box.Position - puncher.GetPosition()))) / 2);
            Console.WriteLine(direction);
            

            // Calculate the velocity to send the entity flying at.
            Vector3 velocity = direction * flinch;
            this.bepuPhysicsComponent.Box.LinearVelocity = velocity;


            // Damage the vitality component when thrown if the entity has vitality
            if (health != null)
            {
                health.Damage(damage);
            }
        }

        public Box GetPunchableBox(){
            return bepuPhysicsComponent.Box;
        }
    }
}
