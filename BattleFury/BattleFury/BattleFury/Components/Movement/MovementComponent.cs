using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using BattleFury.Settings;

namespace BattleFury.Components.Movement
{
    public class MovementComponent : Component
    {
        private PlayerIndex controllingPlayer;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private float moveSpeed;


        public MovementComponent(Entity parent, float moveSpeed)
            : base(parent, "MovementComponent")
        {
            this.moveSpeed = moveSpeed;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            bepuPhysicsComponent = ((BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent"));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Calculate the new move speed in the x direction. 
            // The object should not move faster than moveSpeed due to user input.
            float maxSpeed = moveSpeed;
            float moveAmount = GameplayBindings.MoveAmount(controllingPlayer);
            Vector3 currentLinearVelocity = bepuPhysicsComponent.Box.LinearVelocity;
            float currentLinearVelocityX = currentLinearVelocity.X;

            float newLinearVelocity = currentLinearVelocityX + moveAmount; // new velocity without a speed limit

            // impose the speed limit.
            if (newLinearVelocity > maxSpeed || newLinearVelocity < -maxSpeed){
                if (currentLinearVelocityX > 0 && currentLinearVelocityX < maxSpeed){
                    newLinearVelocity = maxSpeed;
                } else if (currentLinearVelocityX < 0 && currentLinearVelocityX > maxSpeed){
                    newLinearVelocity = -maxSpeed;
                } else {
                    newLinearVelocity = currentLinearVelocityX;
                }
            }

            // Set the new speed in the x direction.
            bepuPhysicsComponent.Box.LinearVelocity = new Vector3(newLinearVelocity, currentLinearVelocity.Y, currentLinearVelocity.Z);

        }
    }
}
