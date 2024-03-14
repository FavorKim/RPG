using Entities;
using Process;
using System.Reflection.Metadata.Ecma335;
using Usable;

namespace Manager
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

    class ItemManager
    {
        public ItemManager(List<Item> inventory) { this.inventory = inventory; }
        
        public void GetItem(Item item)
        {
            EmptyRemover();

            int already = 0;
            int index = 0;
            if (inventory.Count == 0)
            {
                inventory.Add(item);
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].GetName() == item.GetName())
                    {
                        already++;
                        index = i;
                    }
                }
                if (already != 0)
                    inventory[index].Consume++;
                else
                {
                    inventory.Add(item);
                }
            }
        }

        public List<Item> inventory;

        public void EmptyRemover()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Consume == 0)
                {
                    inventory.Remove(inventory[i]);
                    i--;
                }
            }
        }

        public void InvenIndicator()
        {
            EmptyRemover();

            if (inventory.Count == 0)
                Console.WriteLine("인벤토리가 비어있습니다.");

            for (int i = 0; i < inventory.Count; i++)
                Console.WriteLine($"{inventory[i].GetName()} : {inventory[i].Consume}ea");
        }

        
    }

    class SkillManager
    {
        List<Skill> RTL;
        List<Skill> skills;
        Player player;
        Entity dest;
        public SkillManager(Player player)
        {
            this.player = player;
            RTL = new List<Skill>();
            skills = new List<Skill>();
            skills.Add(new Slash(player));
            player.OnLevelUp += SetRTL;
        }

        public Skill CanGet(Skill skill)
        {
            if (skill.IsLearned)
                return null;
            else
            {
                if (player.LV >= skill.SkillLV)
                {
                    skill.IsLearned = true;
                    return skill;
                }
                else
                    return null;
            }
        }
        public void SetRTL()
        {
            Skill skill;
            foreach (Skill s in skills)
            {
                skill = CanGet(s);
                if (skill == null)
                    continue;
                RTL.Add(skill);
            }
        }
        public List<Skill> GetRTL() { return RTL; }

    }
    /*
      -획득-
    스킬 획득 가능 레벨 도달시 플레이어는 스킬을 획득
    스킬레벨 도달시, 스킬을 전달 후, 이미 배운 스킬은 Skills에서 제외
     - 매개인 Skill 객체가 아직 배우지 않았고, 스킬레벨에 도달했는지
    Skill을 검사하고 해당하면 Skill의 내부 변수인 이미 배움 bool변수를 false로 초기화하는
    메서드 선언 Skill CanGet(Skill skill)

     - Skill을 매개로 받아, 해당 Skill을 Player에게 전달하는 메서드 선언 Skill SendSkill(Skill skill)
     - 현재 배울 수 있는 스킬들의 목록을 반환하는 public RTLSender메서드 선언
    foreach(Skill s in Skills) { List<Skill> RTLlist.ADD (SendSkill(CanGet(s))); }
    return RTLlist;
    
    스킬매니저 내부의 RTLlist라는 리스트를 보유.
    플레이어는 RTLlist를 생성자를 통해 보유
     */
}

