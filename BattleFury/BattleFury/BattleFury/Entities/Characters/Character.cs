using System;
using BattleFury.Components.Characters;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Arenas;
using BattleFury.Components.Movement;
using System.Collections.Generic;

namespace BattleFury.Entities.Characters
{
    /// <summary>
    /// Game character used for battling.
    /// </summary>
    public abstract class Character : Entity
    {

        protected VitalityComponent vitalityComponent;

        protected BepuPhysicsComponent bepuPhysicsComponent;

        protected CharacterInformationComponent characterInformationComponent;

        protected RespawnableComponent respawnableComponent;

        protected GrabbableComponent grabbableComponent;

        protected GrabComponent grabComponent;

        public Character(string id, int lives, Box box, PlayerIndex controllingPlayer, int team, Environment environment) {
            // Create the vitality component to track the character's health.
            vitalityComponent = new VitalityComponent(this, lives);
            this.AttachComponent(vitalityComponent);

            // Create the info component to track the character information.
            characterInformationComponent = new CharacterInformationComponent(this, controllingPlayer, team);
            this.AttachComponent(characterInformationComponent);

            // Create the physics Component. 
            // This will actually probably just translate into one box for jumps/gravity/trajectories
            // and collision between characters.
            bepuPhysicsComponent = new BepuPhysicsComponent(this, box);
            this.AttachComponent(bepuPhysicsComponent);

            // Create the Respawnable Component
            respawnableComponent = new RespawnableComponent(this, environment.Arena);
            this.AttachComponent(respawnableComponent);

            grabbableComponent = new GrabbableComponent(this);
            this.AttachComponent(grabbableComponent);

            
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }

        public bool IsKO()
        {
            return vitalityComponent.IsKO;
        }

        public void SetPlacement(int place)
        {
            characterInformationComponent.Place = place;
        }

        public int GetPlacement()
        {
            return characterInformationComponent.Place;
        }

        public int GetPlayerIndex()
        {
            return (int) characterInformationComponent.PlayerIndex;
        }



    }
}
