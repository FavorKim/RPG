using Entities;
using Process;
using System.Reflection.Metadata.Ecma335;
using Usable;
using Equipments;

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

        public void AddInven(Item item)
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
                    if (inventory[i].Name == item.Name)
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
                Console.WriteLine("Inventory is Empty");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌─────────────Inventory───────────┐");
            for (int i = 0; i < inventory.Count; i++)
                Console.WriteLine($"│\t{i + 1}. {inventory[i].Name} : {inventory[i].Consume}ea\t  │");
            Console.WriteLine("└─────────────────────────────────┘");
        }


    }

    class SkillManager
    {
        List<Skill> SkillsUsable;
        List<Skill> skills;
        Player player;
        Entity dest;
        public SkillManager(Player player)
        {
            this.player = player;
            SkillsUsable = new List<Skill>();
            skills = new List<Skill>();
            skills.Add(new Slash(player));
            skills.Add(new Rage(player));
            player.OnLevelUp += SetSkills;
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
        public void SetSkills()
        {
            Skill skill;
            foreach (Skill s in skills)
            {
                skill = CanGet(s);
                if (skill == null)
                    continue;
                SkillsUsable.Add(skill);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"─────────{s.name} Skill Learned!────────");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
        public List<Skill> GetSkillsUsable() { return SkillsUsable; }

    }

    class EquipManager
    {
        Equip[] Equipped = new Equip[(int)Parts.Max];
        List<Equip> EInven = new List<Equip>();
        Player player;

        public EquipManager(Player player)
        {
            this.player = player;
            for(int i = 0; i < (int)Parts.Max; i++)
            {
                Equipped[i] = new Equip();
                Equipped[i].part = (Parts)i;
            }
        }


        public void Equips(Equip equip)
        {
            if (player.LV < equip.equipLV)
            {
                Console.WriteLine("Not Enough Level");
                Console.ReadLine();
                return;
            }
            if (Equipped[(int)equip.part].isEquipped)
                UnEquip(Equipped[(int)equip.part]);

            equip.isEquipped = true;
            Equipped[(int)equip.part] = equip;
            EStatUp(equip);
            RemoveEinven(equip);
        }

        public void UnEquip(Equip equip)
        {
            Equipped[(int)equip.part].isEquipped = false;
            UEStatDown(Equipped[(int)equip.part]);
            EInven.Add(Equipped[(int)equip.part]);
            Array.Clear(Equipped, (int)equip.part, 1);

            equip.isEquipped = true;
            Equipped[(int)equip.part] = equip;
        }

        public void EStatUp(Equip equip)
        {
            if (equip.part == Parts.Weapon)
                player.Atk += equip.Value;
            else
                player.Def += equip.Value;
        }

        public void UEStatDown(Equip equip)
        {
            if (equip.part == Parts.Weapon)
                player.Atk -= equip.Value;
            else
                player.Def -= equip.Value;
        }

        public void EinvenAdd(Equip equip)
        {
            EInven.Add(equip);
        }

        public void EinvenIndicator()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌───Equipmnets Inventory──┐");
            for(int i=0; i<EInven.Count; i++)
                Console.WriteLine($"│\t{i + 1}. {EInven[i].Name}\t  │");
            Console.WriteLine("└─────────────────────────┘");
            Console.ResetColor();
        }

        public void ShowEquipped()
        {
            Console.WriteLine("┌─────────────────────────┐");
            for (int i = 0; i < (int)Parts.Max; i++)
            {
                if (Equipped[i].Name == null)
                    Console.WriteLine("│\t   Empty\t  │");
                else
                    Console.WriteLine($"│\t   {Equipped[i].Name}\t  │");
                //Console.WriteLine
            }
            Console.WriteLine("└─────────────────────────┘");
        }
        
        public List<Equip> GetEinven() { return EInven; }

        public Equip[] GetEquipped() { return Equipped; }

        public void RemoveEinven(Equip equip)
        {
            EInven.Remove(equip);
        }
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

