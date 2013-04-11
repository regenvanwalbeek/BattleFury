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

        private float previousMovementAmount = 0;

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
            float moveAmount = GameplayBindings.MoveAmount(controllingPlayer);
            float downMovement = bepuPhysicsComponent.Box.LinearVelocity.Y;
            bepuPhysicsComponent.Box.LinearVelocity = new Vector3(moveSpeed * moveAmount, downMovement, 0);

            previousMovementAmount = moveAmount;

            //bepuPhysicsComponent.Box.Position += new Vector3(.15f * moveAmount, 0, 0);
            

            
            
            
            
            
            /*
            float currentMovementX = bepuPhysicsComponent.Box.LinearVelocity.X;
            float movementX = moveSpeed * moveAmount;
            float newMovementX = currentMovementX + movementX;
            bepuPhysicsComponent.Box.LinearVelocity = new Vector3(newMovementX, downMovement, bepuPhysicsComponent.Box.LinearVelocity.Z);*/
        }
    }
}
