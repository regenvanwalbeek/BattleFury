using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BattleFury.EntitySystem
{
    public interface IEntityDrawable
    {
        bool IsVisible { get; set; }

        void Draw(GameTime gameTime);
    }
}
