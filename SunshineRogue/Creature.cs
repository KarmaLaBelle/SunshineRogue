using System.Collections.Generic;
using System.Linq;

namespace SunshineRogue
{
    class Creature
    {
        public string Name;
        public int Health;
        public int Damage;

        public static Dictionary<string, Creature> GetDictionary()
        {
            var entries = new[]
            {
                new Creature()
                {
                    Name ="Rat",
                    Health =10,
                    Damage =3
                },
                new Creature()
                {
                    Name = "Kobold",
                    Health = 15,
                    Damage = 5
                }
            };

            return entries.ToDictionary(item => item.Name);
        }
    }
}
