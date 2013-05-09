
using BattleFury.EntitySystem;
using BattleFury.Entities;
using BattleFury.Settings;
using Microsoft.Xna.Framework;
using BattleFury.Components.Characters;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Input;
using BattleFury.Entities.Items;
using BattleFury.Entities.Characters;
using BattleFury.SoundManager;

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
        private float baseDamage;

        private float maxDamage;

        /// <summary>
        /// Milliseconds until a punch is allowed. Punches enabled when <= 0.
        /// </summary>
        private int timeTillPunch = 0;

        private VitalityComponent health;

        private MoveComponent moveComponent;

        private float minFlinch;

        private float maxFlinch;

        public PunchComponent(Entity parent, Environment environment, int punchSpeed, float baseDamage, float maxDamage, float minFlinch, float maxFlinch)
            : base(parent, "PunchComponent")
        {
            this.punchSpeed = punchSpeed;
            this.environment = environment;
            this.baseDamage = baseDamage;
            this.maxDamage = maxDamage;
            this.minFlinch = minFlinch;
            this.maxFlinch = maxFlinch;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.bepuPhysicsComponent = (BepuPhysicsComponent)Parent.GetComponent("BepuPhysicsComponent");
            this.health = (VitalityComponent)Parent.GetComponent("VitalityComponent");
            this.moveComponent = ((MoveComponent)Parent.GetComponent("MoveComponent"));
        }

        public override void Update(GameTime gameTime)
        {
            timeTillPunch -= gameTime.ElapsedGameTime.Milliseconds;


            // Do a punch if the timer allows
            if (GameplayBindings.IsPunch(controllingPlayer) && timeTillPunch <= 0)
            {

                // Determine how much damage to do. This will scale linearly
                float rage = this.health.RageMeter;
                float damage = baseDamage + ((maxDamage - baseDamage) / 99) * (100 - rage);
                if (rage == 0)
                {
                    damage *= 2; // DOUBLE DAMAGE! RAAAAAAAAAGE MODE.
                }

                // Do ground pound
                if (GameplayBindings.IsGroundPound(controllingPlayer))
                {
                    System.Console.WriteLine("Ground Pound!");
                    Vector3 offset = new Vector3(0, -4, 0);
                    Vector3 impulse = new Vector3(0, -50, 0);
                    bepuPhysicsComponent.Box.ApplyLinearImpulse(ref impulse);

                    // Get all the entities colliding with the hitbox
                    GroundPoundHitbox hitbox = new GroundPoundHitbox(bepuPhysicsComponent.Box.Position + offset, (Character)Parent, damage, minFlinch, maxFlinch, environment);
                    hitbox.Initialize();
                    environment.ItemManager.AddItem(hitbox);
                }
                // Do normal punch
                else
                {
                    Vector3 offset;
                    if (GameplayBindings.IsUpPunch(controllingPlayer))
                    {
                        if (GameSettings.PunchJumpMode)
                        {
                            offset = new Vector3(0, 2.6f, 0);
                        }
                        else
                        {
                            offset = new Vector3(0, 2.635f, 0);
                        }
                    }
                    else
                    {
                        if (GameSettings.PunchJumpMode) // because my playtesters are sadists who hate fun.
                        {
                            offset = new Vector3(moveComponent.DirectionX, 0, 0);
                        }
                        else
                        {
                            offset = new Vector3(moveComponent.DirectionX * 1.435f, 0, 0);
                        }
                    }
                    // Get all the entities colliding with the hitbox
                    Fist f = new Fist(bepuPhysicsComponent.Box.Position + offset, (Character)Parent, damage, minFlinch, maxFlinch, environment);
                    f.Initialize();
                    environment.ItemManager.AddItem(f);

                }

                timeTillPunch = punchSpeed;

                

                
            }
        }


        public Vector3 GetPosition()
        {
            return bepuPhysicsComponent.Box.Position;
        }


    }
}
