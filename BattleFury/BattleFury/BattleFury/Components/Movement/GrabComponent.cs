using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Settings;
using BattleFury.Components.Characters;
using BattleFury.Entities.Characters;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Input;
using BattleFury.Entities;

namespace BattleFury.Components.Movement
{
    /// <summary>
    /// Component which enables an entity to grab a Grabbable entity. After an entity is grabbed, it can be thrown.
    /// </summary>
    public class GrabComponent : Component
    {
        /// <summary>
        /// Damage dealt when thrown when Entity's SM = 100%
        /// </summary>
        private float baseDamage;

        /// <summary>
        /// Damage dealth when thrown when Entity's SM = 1%
        /// </summary>
        private float maxDamage;

        /// <summary>
        /// Grabbing hitbox
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        /// <summary>
        /// Player doing the grabbing for this entity.
        /// </summary>
        private PlayerIndex controllingPlayer;

        private Environment environment;

        private VitalityComponent health;

        /// <summary>
        /// Whether this entity is currently grabbing a grabbable object.
        /// </summary>
        public bool IsGrabbingObject
        {
            get
            {
                return (grabbedObject != null);
            }
        }

        /// <summary>
        /// The object being grabbed, if IsGrabbingObject = true;
        /// </summary>
        private GrabbableComponent grabbedObject = null;

        public GrabComponent(Entity parent, Environment environment, float baseDamage, float maxDamage) : base(parent, "GrabComponent"){
            this.environment = environment;
            this.baseDamage = baseDamage;
            this.maxDamage = maxDamage;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.bepuPhysicsComponent = (BepuPhysicsComponent) Parent.GetComponent("BepuPhysicsComponent");
            this.health = (VitalityComponent)Parent.GetComponent("VitalityComponent");
        }

        public override void Update(GameTime gameTime)
        {

            if (!IsGrabbingObject)
            {
                // Do a grab if nothing is currently grabbed.
                if (GameplayBindings.IsGrab(controllingPlayer))
                {
                    // Get all the grabbable components available in this frame.
                    List<GrabbableComponent> grabbables = environment.GetEntitiesWithComponent<GrabbableComponent>("GrabbableComponent");

                    // Get all the entities colliding with the hitbox
                    EntityCollidableCollection overlappedCollideables = bepuPhysicsComponent.Box.CollisionInformation.OverlappedEntities;

                    // Iterate through the colliding entities and Grab any entity if it is grabbable.
                    EntityCollidableCollection.Enumerator enumerator = overlappedCollideables.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        BEPUphysics.Entities.Entity collidingEntity = enumerator.Current;
                        // Check if the entity is equal to any of the grabbables.
                        for (int i = 0; i < grabbables.Count; i++)
                        {
                            if (collidingEntity == grabbables[i].GetGrabbableBox())
                            {
                                bool success = grabbables[i].Grab(this);
                                if (success)
                                {
                                    grabbedObject = grabbables[i];
                                    break;
                                }
                            }
                        }
                        if (IsGrabbingObject)
                        {
                            break;
                        }
                    }


                }
            }
            else
            {
                // Do a throw if something is currently being grabbed.
                if (GameplayBindings.IsThrow(controllingPlayer))
                {
                    // Do a throw in the direction of the left analog stick
                    Vector2 direction = InputState.GetLeftAnalogStick(controllingPlayer);
                                      
                    // Determine how much damage to do. This will scale linearly
                    float rage = this.health.RageMeter;
                    float damage = baseDamage + ((maxDamage - baseDamage) / 99) * (100 - rage);
                    if (rage == 0)
                    {
                        damage *= 2; // DOUBLE DAMAGE! RAAAAAAAAAGE MODE.
                    }

                    grabbedObject.Throw(direction, damage);
                    this.grabbedObject = null; // Let go!
                }
            }
        }

        public Vector3 GetPosition()
        {
            return bepuPhysicsComponent.Box.Position;
        }

        /// <summary>
        /// Forces this entity to lose grip of the grabbed object
        /// </summary>
        public void LoseGrip()
        {
            this.grabbedObject = null;
        }
    }
}
