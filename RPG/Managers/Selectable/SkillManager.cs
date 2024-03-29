﻿using Entities;
using Processors;
using Usable;

namespace Managers
{
    class SkillManager
    {
        List<Skill> SkillsUsable;
        List<Skill> skills;
        Player player;
        Entity dest;
        public SelectProcessor<Skill> selP;

        public SkillManager(Player player)
        {
            this.player = player;
            SkillsUsable = new List<Skill>();
            skills = new List<Skill>();
            skills.Add(new Slash(player));
            skills.Add(new Rage(player));
            player.OnLevelUp += SetSkills;
            selP = new SelectProcessor<Skill>(SkillsUsable);
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
                Console.WriteLine($"─────────{s.Name} Skill Learned!────────");
                Console.ResetColor();
                Console.ReadLine();
                SkillsUsable[0].IsSelected = true;
            }
        }
        public List<Skill> GetSkillsUsable() { return SkillsUsable; }

        public void ResetRage()
        {
            if(player.IsRaged)
            {
                player.Atk -= 10;
                player.MaxHP -= 30;
                player.CurHP -= 30;
                player.IsRaged = false;

                Console.WriteLine("Rage Over!");
                Console.ReadLine();
                Console.WriteLine("Player's Status Reset");
                Console.ReadLine();
            }
        }

    }
}
