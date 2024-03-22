using Entities;
namespace Usable
{
    

    abstract class Skill : IUsable
    {
        public Entity dest { get; set; }
        protected Player player;
        public string Name { get; set; }
        public int Consume { set; get; }
        public int SkillLV { get; protected set; }
        protected int value;
        public bool IsLearned {  set; get; }
        public bool IsAttack { get; protected set; }
        public bool IsSelected { get; set; }
        public void ShowNum() { Console.WriteLine($" : {Consume}mp"); }

        public Skill(Player player) 
        {
            this.player = player;
            IsLearned = false; 
        }
        public void Use()
        {
            if (CanUse())
            {
                player.CurMP -= Consume;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{0} On {1}!", Name, dest.Name);
                Console.ResetColor();
                Effect();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Can't Use Skill");
                Console.ReadLine();
            }
        }
        public bool CanUse()
        {
            return player.CurMP >= Consume;
        }

        public void SetDest(Entity dest)
        {
            this.dest = dest;
        }

        public abstract void Effect();
    }

    class Slash : Skill
    {
        public Slash(Player player) :base(player)
        {
            value = player.Atk + (player.LV * 4);
            Consume = 10;
            SkillLV = 3;
            Name = "Slash";
            IsAttack = true;
        }

        public override void Effect() { dest.CurHP -= value; }
    }

    class Rage : Skill
    {
        public Rage(Player player) : base(player) 
        {
            value = 10;
            Consume = 15;
            SkillLV = 5;
            Name = "Rage";
            IsAttack = false;
        }
        public override void Effect() { dest.Atk += value; dest.MaxHP += value; }
    }

    /* 스킬
    플레이어는 사용할 수 있는 스킬 리스트를 보유 SkillList
    스킬 매니저에서 모든 속성의 스킬 객체들을 보유 Skills


    -요소-
    스킬은 IUsable을 상속받은 객체로써, Player.Use메서드를 통해 사용.
    소모값 Consume으로 스킬 획득시 필요한 MP의 양을 설정
    Use의 매개는 스킬 효과를 받는 Entity객체.
     ㄴ 이는 시전자가 될 수도 있음
    CanUse()는 현재 MP가 소모값보다 낮을 경우 false 외에 true
    스킬은 플레이어가 도달시 스킬이 획득되는 스킬레벨이 각각있음

    

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
    

    
    


    스킬 획득시 스킬 매니저에서 획득한 스킬을 플레이어에게 전달
    전달받은 스킬은 플레이어가 스킬 리스트에 추가.
    

     -사용-
    SkillProcessor(이하 SP)에서 해당 기능을 관리
    SP는 SkillIndicator메서드를 통해 보유하고있는 스킬과 소모값을 출력.
    스킬리스트의 인덱스값+1 과 입력값이 일치하면 사용
    
    
    
    -사용불가-
    소모값(MP)가 적으면 사용불가
    


     
     */


}
