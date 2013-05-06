using BattleFury.Components.Animated;
using BattleFury.Components.Movement;
using BEPUphysics.Entities.Prefabs;
using Microsoft.Xna.Framework;
using System;
using BattleFury.Components;

namespace BattleFury.Entities.Items
{
    public class Popsicle : Item
    {

        private float healAmount;

        private float healTime;

        private GrabbableComponent grabbable;

        private Environment environment;

        public Popsicle(Vector3 spawnPosition, float healAmount, float healTime, Environment environment)
            : base(new Box(spawnPosition, 1, 1, 1, .01f))
        {
            this.healAmount = healAmount;
            this.healTime = healTime;
            this.environment = environment;

            // Create the rendering component. Since the cube model is 1x1x1, 
            // it needs to be scaled to match the size of each individual box.
            Matrix scaling = Matrix.CreateScale(.7f, .7f, .7f);
            BasicModelComponent drawComponent = new CubeRenderComponent(this, scaling);
            this.AttachComponent(drawComponent);

            grabbable = new GrabbableComponent(this, 0, 100);
            grabbable.OnGrab += OnPopsiclePickup;
            this.AttachComponent(grabbable);

        }

        public void OnPopsiclePickup(object sender, EventArgs e)
        {
            grabbable.Grabber.Parent.AttachComponent(new HealComponent(grabbable.Grabber.Parent, healAmount, healTime));
            environment.ItemManager.Remove(this);
        }

    }
}
