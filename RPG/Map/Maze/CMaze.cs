﻿using CRoom;
using System.Buffers;
using Mapper;
using Entities;

namespace CMaze
{
    
    enum Tile
    {
        ROAD = 1,
        WALL = 2,
        PLAYER = 3,
        GOAL = 4,
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
        Up, Down, Left, Right, Fail
    }

    class Maze 
    {
        public Buffer<Room> Rbuffer;
        const int Size = (int)MapSize.Height * (int)MapSize.Width;
        Room[] maze = new Room[Size];
        Room[] buffer;
        int stageLV;

        public bool IsGoal { get; private set; }

        public Maze()
        {
            maze = new Room[Size];

            SetMaze();
            Rbuffer = new Buffer<Room>(Size, DrawMaze, maze);
            buffer = Rbuffer.GetArray();
        }

        void ResetMaze()
        {
            for(int i=0; i<Size; i++)
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
        void SetMonsters()
        {
            int mons = 0;
            mons = stageLV + 2;
            Stack<Room> monRoom = new Stack<Room>(mons);
            monRoom = GetEmpty(mons);

            for(int i=0; i<monRoom.Count; i++)
                monRoom.Pop().SetWoW(Tile.Monster);
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
                        Console.Write("▒");
                    else if (buffer[count].GetWoW() == Tile.PLAYER)
                        Console.Write("◈");
                    else if (buffer[count].GetWoW() == Tile.GOAL)
                        Console.Write("★");
                    else if (buffer[count].GetWoW() == Tile.Monster)
                        Console.Write("Ψ");
                    count++;
                }
                Console.WriteLine();

            }
        }

        Stack<Room> GetEmpty(int count)
        {
            List<Room> emptys = new List<Room>(maze.Length);
            Stack<Room> ret = new Stack<Room>(count);
            Random rand = new Random();
            int sour = 0;
            Room temp;
            foreach(Room r in maze)
            {
                if (r.GetWoW() != Tile.ROAD)
                    continue;
                emptys.Add(r);
            }

            for(int i=0; i < 100; i++)
            {
                sour = rand.Next(0, emptys.Count-1);
                temp = emptys[sour];
                emptys[sour] = emptys[sour + 1];
                emptys[sour+1] = temp;
            }

            for(int i = 0; i < count; i++)
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

            roomtomove = player.GetRoomToMove(dir);

            if (roomtomove == null) 
                return Tile.Fail;

            if(roomtomove.GetWoW() == Tile.GOAL)
                return Tile.GOAL;

            if (roomtomove.CanMove())
            {
                Room Ptemp = new Room(0,0);
                Room Rtemp = new Room(0, 0);
                Ptemp = player;
                Rtemp = roomtomove;

                player = roomtomove;
                roomtomove = Ptemp;

                if (Rtemp.GetWoW() == Tile.ROAD)
                {
                    player.SetWoW(Tile.PLAYER);
                    roomtomove.SetWoW(Tile.ROAD);
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
