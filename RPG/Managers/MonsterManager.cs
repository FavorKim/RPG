using Entities;

namespace Managers
{
    class MonsterManager
    {
        Dictionary<string, Monster> mons;
        public MonsterManager() { mons = new Dictionary<string, Monster>(); }

        public Dictionary<string,Monster> Mons { get { return mons; } }
        public Monster GetMonster(string s)
        {
            return mons[s];
        }
        public void AddMon(Monster mon) { mons.Add(mon.Name, mon); }
    }
}

