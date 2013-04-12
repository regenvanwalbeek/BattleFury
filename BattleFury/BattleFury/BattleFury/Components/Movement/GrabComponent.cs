using System;
using BattleFury.EntitySystem;
using Microsoft.Xna.Framework;
using BattleFury.Settings;
using BattleFury.Components.Characters;
using BattleFury.Entities.Characters;
using System.Collections.Generic;
using BEPUphysics.Collidables;
using BattleFury.Input;

namespace BattleFury.Components.Movement
{
    /// <summary>
    /// Component which enables an entity to grab a Grabbable entity. After an entity is grabbed, it can be thrown.
    /// </summary>
    public class GrabComponent : Component
    {
        /// <summary>
        /// How hard to throw the grabbed entity.
        /// </summary>
        private float throwStrength;

        /// <summary>
        /// Grabbing hitbox
        /// </summary>
        private BepuPhysicsComponent bepuPhysicsComponent;

        /// <summary>
        /// Player doing the grabbing for this entity.
        /// </summary>
        private PlayerIndex controllingPlayer;

        /// <summary>
        /// List of grabbable characters
        /// </summary>
        List<Character> characters;

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

        public GrabComponent(Entity parent, List<Character> characters, float throwStrength) : base(parent, "GrabComponent"){
            this.characters = characters;
            this.throwStrength = throwStrength;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            this.controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.bepuPhysicsComponent = (BepuPhysicsComponent) Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(GameTime gameTime)
        {

            if (!IsGrabbingObject)
            {
                // Do a grab if nothing is currently grabbed.
                if (GameplayBindings.IsGrab(controllingPlayer))
                {
                    // Get all the grabbable components available in this frame.
                    List<GrabbableComponent> grabbables = new List<GrabbableComponent>();
                    for (int i = 0; i < characters.Count; i++)
                    {
                        grabbables.Add((GrabbableComponent)characters[i].GetComponent("GrabbableComponent"));
                    }

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
                            if (collidingEntity == grabbables[i].getGrabbableBox())
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
                    grabbedObject.Throw(direction, throwStrength);
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
