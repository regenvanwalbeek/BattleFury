
using BattleFury.EntitySystem;
using BattleFury.Entities;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using System.Collections.Generic;
using BEPUphysics.Collidables;

namespace BattleFury.Components.Movement
{
    public class PunchComponent : Component
    {

        /// <summary>
        /// Punching hitbox
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        private PlayerIndex controllingPlayer;

        private Environment environment;

        /// <summary>
        /// Number of milliseconds between punches
        /// </summary>
        private int punchSpeed;

        /// <summary>
        /// Strength of the punch attack.
        /// </summary>
        private int strength; 

        /// <summary>
        /// Milliseconds until a punch is allowed. Punches enabled when <= 0.
        /// </summary>
        private int timeTillPunch = 0;

        

        public PunchComponent(Entity parent, Environment environment, int punchSpeed, int strength)
            : base(parent, "PunchComponent")
        {
            this.punchSpeed = punchSpeed;
            this.environment = environment;
            this.strength = strength;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(GameTime gameTime)
        {
            timeTillPunch -= gameTime.ElapsedGameTime.Milliseconds;
   
            // Do a punch if the timer allows
            if (GameplayBindings.IsPunch(controllingPlayer) && timeTillPunch <= 0)
            {
                System.Console.WriteLine("FALCON PUNCH");
                timeTillPunch = punchSpeed;

                // Get all punchable components available in this frame.
                List<PunchableComponent> punchables = environment.GetEntitiesWithComponent<PunchableComponent>("PunchableComponent");

                // Get all the entities colliding with the hitbox
                EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;

                // Iterate through the colliding entities and punch any entity if it is punchable.
                EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BEPUphysics.Entities.Entity collidingEntity = enumerator.Current;
                    // Check if the entity is equal to any of the punchables
                    for (int i = 0; i < punchables.Count; i++)
                    {
                        if (collidingEntity == punchables[i].GetPunchableBox())
                        {
                            punchables[i].Punch(this, strength);
                        }
                    }
                }
            }
        }


        public Vector3 GetPosition()
        {
            return bepuPhysicsComponent.Box.Position;
        }


    }
}
