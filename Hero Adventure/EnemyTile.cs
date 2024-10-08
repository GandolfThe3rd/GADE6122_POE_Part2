using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public abstract class EnemyTile :CharacterTile
    {
        public EnemyTile(Position position, int hitPoints, int attackPower) : base(position, hitPoints, attackPower)
        {

        }

        public abstract bool GetMove(out Tile targetTile);

        public abstract CharacterTile[] GetTargets();

    }
}
