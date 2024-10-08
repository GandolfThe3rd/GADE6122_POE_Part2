using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class HeroTile : CharacterTile
    {
        public HeroTile(Position aPosition) : base(aPosition, 40, 5)
        {
            
        }

        public override char Display
        {
            get
            {
                switch (isDead)
                {
                    case false:
                        {
                            return Convert.ToChar("▼");
                        }
                    default:
                        {
                            return Convert.ToChar("X");
                        }
                }
            }
        }
    }
}
