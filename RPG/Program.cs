using Entities;
using Process;
using Usable;
using Manager;
using System.Diagnostics;
using System.Windows;

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
            MainProcessor battleP = new MainProcessor();

            battleP.Indicator(player, dummy);

            skillBook.LearnSkill(new TripleSlash(player));
            skillBook.LearnSkill(new Slash(player));
            
            player.Use(skillBook.GetSkill(0),dummy);
            
            battleP.Indicator(player, dummy);

            player.Use(skillBook.GetSkill(1), dummy);

            battleP.Indicator(player, dummy);
            */
        }
    }
}