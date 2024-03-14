using Entities;
using Process;
using Usable;
using Manager;
using System.Diagnostics;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainProcessor main = new MainProcessor();
            main.MainProcess();


            /*
            Player player = new Player();
            SkillManager skillBook = new SkillManager(player);
            Monster dummy = new Dummy();
            MainProcessor bp = new MainProcessor();

            bp.Indicator(player, dummy);

            skillBook.LearnSkill(new TripleSlash(player));
            skillBook.LearnSkill(new Slash(player));
            
            player.Use(skillBook.GetSkill(0),dummy);
            
            bp.Indicator(player, dummy);

            player.Use(skillBook.GetSkill(1), dummy);

            bp.Indicator(player, dummy);
            */
        }
    }
}