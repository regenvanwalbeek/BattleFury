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
    public class GrabComponent : Component
    {
        private float throwStrength;

        private BepuPhysicsComponent bepuPhysicsComponent;

        private PlayerIndex controllingPlayer;

        List<Character> characters;

        private bool isGrabbingObject = false;

        private GrabbableComponent grabbedObject;

        public GrabComponent(Entity parent, List<Character> characters, float throwStrength) : base(parent, "GrabComponent"){
            this.characters = characters;
            this.throwStrength = throwStrength;
        }

        public override void Initialize()
        {
        }

        public override void Start()
        {
            controllingPlayer = ((CharacterInformationComponent)Parent.GetComponent("CharacterInformationComponent")).PlayerIndex;
            this.bepuPhysicsComponent = (BepuPhysicsComponent) Parent.GetComponent("BepuPhysicsComponent");
        }

        public override void Update(GameTime gameTime)
        {

            if (!isGrabbingObject)
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
                                bool success = grabbables[i].Grab();
                                if (success)
                                {
                                    isGrabbingObject = true;
                                    grabbedObject = grabbables[i];
                                    break;
                                }
                            }
                        }
                        if (isGrabbingObject)
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
                    // Do a throw.
                    isGrabbingObject = false;

                    // Throw in the direction of the left analog stick
                    Vector2 direction = InputState.GetLeftAnalogStick(controllingPlayer);
                    grabbedObject.Throw(direction, throwStrength);
                }
            }
        }
    }
}
