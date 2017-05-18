using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts
{
    interface IShootable
    {
        void SetBulletDamage(float damage);
        void SetBulletSpeed(float speed);
        void FireBullet(Level1 game, Player player, DirectionEnum direction);
    }
}
