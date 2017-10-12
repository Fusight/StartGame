﻿using System.Drawing;

namespace StartGame
{
    internal enum AttackType
    { melee, range, magic };

    internal class Troop
    {
        public string name;
        public Weapon weapon;
        public int MaxMovement = 5;
        public Point position;
        public Bitmap image;

        public Troop(string Name, Weapon Weapon, Bitmap Image)
        {
            image = Image;
            name = Name;
            weapon = Weapon;
        }
    }

    internal class Weapon
    {
        public int attackDamage;
        public AttackType type;
        public int range;

        public Weapon(int AttackDamage, AttackType Type, int Range)
        {
            range = Range;
            type = Type;
            attackDamage = AttackDamage;
        }
    }
}