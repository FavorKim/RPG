using CRoom;
using Entities;
using Processors;
using Managers;
using Equipments;

namespace CMaze
{

    enum Tile
    {
        ROAD = 1,
        WALL = 2,
        PLAYER = 3,
        GOAL = 4,
        ESC,
        Monster,
        Fail
    }

    enum MapSize
    {
        Width = 21,
        Height = 15,
    }

    enum Direction
    {
        Up, Down, Left, Right, ESC, Fail
    }

    class Maze
    {
        public Buffer<Room> Rbuffer;
        const int Size = (int)MapSize.Height * (int)MapSize.Width;
        Room[] maze = new Room[Size];
        Room[] buffer;
        int stageLV;
        int moveCount;
        ItemManager iM;
        Equip eM;

        public event Action OnMove;

        public bool IsGoal { get; private set; }

        public Maze()
        {
            maze = new Room[Size];

            SetMaze();
            OnMove += MoveMonsters;
            Rbuffer = new Buffer<Room>(Size, DrawMaze, maze);
            buffer = Rbuffer.GetArray();
        }

        public void ResetMaze()
        {
            for (int i = 0; i < Size; i++)
            {
                maze[i] = null;
            }
            SetMaze();
        }

        void SetMaze()
        {
            InitMaze();
            MakeMaze();
            SetGoalnStart();
            SetMonsters();
            moveCount = 0;
        }

        public Room[] GetMaze() { return maze; }

        void MakeMaze()
        {
            maze[(int)MapSize.Width + 1].GoNext(null);
        }
        void InitMaze()
        {
            FillMaze();
            Room.SetMaze(maze);
            foreach (Room room in maze)
                room.InitRoom();

        }
        void FillMaze()
        {
            int count = 0;
            for (int i = 0; i < (int)MapSize.Height; i++)
            {
                for (int k = 0; k < (int)MapSize.Width; k++)
                {
                    maze[count] = new Room(k, i);
                    count++;
                }
            }
        }
        void SetGoalnStart()
        {
            maze[maze.Length - 2].SetWoW(Tile.GOAL);
            maze[1].SetWoW(Tile.PLAYER);
        }
        public void SetMonsters()
        {
            int tempX = 0;
            int tempY = 0;
            int mons = 0;
            Room temp;
            mons = stageLV + 2;
            Stack<Room> monRoom = new Stack<Room>(mons);
            monRoom = GetEmpty(mons);

            for (int i = 0; i < monRoom.Count; i++)
            {
                temp = monRoom.Pop();
                tempX = temp.X;
                tempY = temp.Y;
                for (int k = 0; k < maze.Length; k++)
                {
                    if (maze[k].X != tempX || maze[k].Y != tempY)
                        continue;
                    else
                    {
                        maze[k].SetWoW(Tile.Monster);
                        break;
                    }
                }
            }
        }

        public void DrawMaze()
        {

            int count = 0;

            for (int i = 0; i < (int)MapSize.Height; i++)
            {
                for (int k = 0; k < (int)MapSize.Width; k++)
                {
                    if (buffer[count].GetWoW() == Tile.ROAD)
                        Console.Write(" ");
                    else if (buffer[count].GetWoW() == Tile.WALL)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("▒");
                        Console.ResetColor();
                    }
                    else if (buffer[count].GetWoW() == Tile.PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("◈");
                        Console.ResetColor();
                    }
                    else if (buffer[count].GetWoW() == Tile.GOAL)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("★");
                        Console.ResetColor();
                    }
                    else if (buffer[count].GetWoW() == Tile.Monster)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Ψ");
                        Console.ResetColor();
                    }
                    count++;
                }
                Console.WriteLine();

            }
        }

        Stack<Room> GetEmpty(int count)
        {
            List<Room> emptys = maze.ToList();
            Stack<Room> ret = new Stack<Room>(count);
            Random rand = new Random();
            int sour = 0;
            int dest = 0;
            Room temp;
            foreach (Room r in maze)
            {
                if (r.GetWoW() != Tile.ROAD)
                {
                    emptys.Remove(r);
                }
            }



            for (int i = 0; i < 100; i++)
            {
                sour = rand.Next(0, emptys.Count - 1);
                do
                {
                    dest = rand.Next(0, emptys.Count - 1);
                } while (dest == sour);

                temp = emptys[sour];
                emptys[sour] = emptys[dest];
                emptys[dest] = temp;
            }

            for (int i = 0; i < count; i++)
                ret.Push(emptys[i]);

            return ret;
        }


        Direction GetDirection()
        {
            ConsoleKeyInfo input;
            input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.DownArrow:
                    return Direction.Down;

                case ConsoleKey.Escape:
                    return Direction.ESC;
                default:
                    return Direction.Fail;
            }
        }
        Room FindPlayer()
        {
            foreach (Room r in maze)
            {
                if (r.GetWoW() == Tile.PLAYER)
                    return r;
                else
                    continue;
            }

            Console.WriteLine("플레이어 찾기 실패");
            return null;
        }
        public Tile Move()
        {
            Room roomtomove;
            Room player = FindPlayer();
            Direction dir = GetDirection();

            if (dir == Direction.Fail)
                return Tile.Fail;

            if (dir == Direction.ESC)
                return Tile.ESC;

            roomtomove = player.GetRoomToMove(dir);

            if (roomtomove == null)
                return Tile.Fail;

            if (roomtomove.GetWoW() == Tile.GOAL)
                return Tile.GOAL;

            if (roomtomove.CanMove())
            {
                Room Ptemp = new Room(0, 0);
                Room Rtemp = new Room(0, 0);
                Ptemp = player;
                Rtemp = roomtomove;

                player = roomtomove;
                roomtomove = Ptemp;

                if (Rtemp.GetWoW() == Tile.ROAD)
                {
                    player.SetWoW(Tile.PLAYER);
                    roomtomove.SetWoW(Tile.ROAD);
                    moveCount++;
                    OnMove();
                    return Tile.ROAD;
                }
                else if (Rtemp.GetWoW() == Tile.Monster)
                {
                    player.SetWoW(Tile.PLAYER);
                    roomtomove.SetWoW(Tile.ROAD);
                    return Tile.Monster;
                }
            }
            return Tile.Fail;
        }
        public void Move(Room mon)
        {
            Random rand = new Random();
            Room roomtomove;

            while (true)
            {
                Direction dir = (Direction)rand.Next(0, 4);
                roomtomove = mon.GetRoomToMove(dir);
                if (!roomtomove.CanMove())
                    continue;

                else
                    break;
            }
            Room Ptemp = new Room(0, 0);
            Room Rtemp = new Room(0, 0);
            Ptemp = mon;
            Rtemp = roomtomove;

            mon = roomtomove;
            roomtomove = Ptemp;

            if (Rtemp.GetWoW() == Tile.ROAD)
            {
                mon.SetWoW(Tile.Monster);
                roomtomove.SetWoW(Tile.ROAD);
            }
            else if (Rtemp.GetWoW() == Tile.Monster || Rtemp.GetWoW() == Tile.PLAYER)
                return;
        }




        void MoveMonsters()
        {
            foreach (Room r in maze)
            {
                if (r.GetWoW() == Tile.Monster)
                    Move(r);
            }
        }


        public void SetLevel(Player player)
        {
            stageLV = player.LV / 3;
        }
        public int GetLevel() { return stageLV; }


        public void OnGoal()
        {
            ResetMaze();
            Console.WriteLine("Goal!");
            Console.ReadLine();
        }




    }


}
