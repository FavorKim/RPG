using Entities;
using Processors;
using System.Reflection.Metadata.Ecma335;
using Usable;
using Equipments;

namespace Managers
{
    class MonsterManager
    {
        Dictionary<string, Monster> mons;
        public MonsterManager() { mons = new Dictionary<string, Monster>(); }

        public Monster GetMonster(string s)
        {
            return mons[s];
        }
        public void AddMon(Monster mon) { mons.Add(mon.Name, mon); }
    }
}

