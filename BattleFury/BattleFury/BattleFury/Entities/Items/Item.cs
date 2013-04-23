using BattleFury.Components;
using BattleFury.EntitySystem;
using BEPUphysics.Entities.Prefabs;

namespace BattleFury.Entities.Items
{
    public abstract class Item : Entity
    {
        protected BepuPhysicsComponent bepuPhysicsComponent;

        public Item(Box box){
            this.bepuPhysicsComponent = new BepuPhysicsComponent(this, box);
            this.AttachComponent(bepuPhysicsComponent);
        }

        public Box GetBox()
        {
            return bepuPhysicsComponent.Box;
        }
    }
}
