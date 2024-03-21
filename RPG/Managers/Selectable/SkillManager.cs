using Entities;
using Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            selP = new SelectProcessor<Skill>(SkillsUsable, true);
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

    }
}
