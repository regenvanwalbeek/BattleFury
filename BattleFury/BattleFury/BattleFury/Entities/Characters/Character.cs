using System;
using BattleFury.Components.Characters;
using BattleFury.EntitySystem;
using BattleFury.Components;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using BattleFury.Entities.Arenas;

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

        protected RespawnableComponent respawnableObjectComponent;

        public Character(string id, int lives, Box box, PlayerIndex controllingPlayer, int team, Arena arena) {
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
            respawnableObjectComponent = new RespawnableComponent(this, arena);
            this.AttachComponent(respawnableObjectComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }

    }
}
