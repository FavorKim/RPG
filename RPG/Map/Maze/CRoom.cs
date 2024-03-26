using CMaze;
namespace CRoom
{
    class Room
    {
        public static void SetMaze(Room[] maze)
        {
            Room.maze = maze;
        }


        int x;
        int y;
        Tile wayorWall;
        bool isVisit;
        Room? left;
        Room? right;
        Room? up;
        Room? down;
        Room? before;
        Room[] rooms;
        Stack<Room> nexts;
        static Room[] maze;


        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;
            wayorWall = Tile.WALL;
        }

        public Tile GetWoW() { return wayorWall; }
        public void SetWoW(Tile value) { wayorWall = value; }
        public int X { get { return x; } set {  x = value; } }
        public int Y { get { return y; } set {  y = value; } }

        Room GetRoomatMaze(int x, int y)
        {
            if (maze == null) return null;

            foreach (Room r in maze)
            {
                if (r.x == x && r.y == y)
                    return r;
                else continue;
            }
            return null;
        }
        Room GetBeside(Room r1, Room r2)
        {
            int temp;
            int y1 = r1.y;
            int y2 = r2.y;
            int x1 = r1.x;
            int x2 = r2.x;
            Room beside;
            if (r1.y == r2.y)
            {
                if (x1 < x2) temp = x1 + 1;
                else temp = x2 + 1;
                beside = GetRoomatMaze(temp, y);
            }
            else if (r1.x == r2.x)
            {
                if (y1 > y2) temp = y2 + 1;
                else temp = y1 + 1;
                beside = GetRoomatMaze(x, temp);
            }
            else
            {
                Console.WriteLine("GetBeside Error");
                return null;
            }
            return beside;
        }

        void SetRooms()
        {
            if (rooms != null)
                return;
            else
                rooms = new Room[4];

            left = GetRoomatMaze(x - 2, y);
            right = GetRoomatMaze(x + 2, y);
            down = GetRoomatMaze(x, y + 2);
            up = GetRoomatMaze(x, y - 2);

            if (left != null)
                rooms[0] = left;
            if (right != null)
                rooms[1] = right;
            if (up != null)
                rooms[2] = up;
            if (down != null)
                rooms[3] = down;

        }
        public void InitRoom()
        {
            Shuffle();

            if (nexts != null)
                return;
            else
                nexts = new Stack<Room>(4);

            foreach (Room room in rooms)
                nexts.Push(room);

        }
        void Shuffle()
        {
            SetRooms();
            Room temp;
            Random rand = new Random();
            int sour;
            for (int i = 0; i < 100; i++)
            {
                for (int k = 0; k < rooms.Length - 1; k++)
                {
                    sour = rand.Next(rooms.Length);
                    temp = rooms[sour];
                    rooms[sour] = rooms[k];
                    rooms[k] = temp;
                }
            }
        }
        public void GoNext(Room? bef)
        {
            bool isFirst = true;
            InitRoom();
            before = bef;
            isVisit = true;
            Room next;
            while (true)
            {
                if (nexts.Count <= 0)
                {
                    if (before != null)
                        before.GoNext(before.before);
                    return;
                }

                next = nexts.Pop();
                if (next == null) continue;
                if (next.x < 1 || next.y < 1)
                    continue;
                if (next.x > (int)MapSize.Width - 1 || next.y > (int)MapSize.Height - 1)
                    continue;
                if (next.isVisit == true)
                    continue;

                break;
            }

            wayorWall = Tile.ROAD;
            next.wayorWall = Tile.ROAD;

            GetBeside(this, next).wayorWall = Tile.ROAD;

            if (before == null && !isFirst)
                return;
            isFirst = false;
            next.GoNext(this);
        }


        public bool CanMove()
        {
            if (this == null) return false;
            else if (x < 0 || x >= (int)MapSize.Width)
                return false;
            else if (y < 0 || y >= (int)MapSize.Height)
                return false;
            else if (GetWoW() == Tile.WALL) 
                return false;

            return true;

        }
        public Room GetRoomToMove(Direction dir)
        {
            int x = X;
            int y = Y;
            switch (dir)
            {
                case Direction.Left:
                    x = X - 1;
                    break;

                case Direction.Right:
                    x = X + 1;
                    break;

                case Direction.Up:
                    y = Y - 1;
                    break;

                case Direction.Down:
                    y = Y + 1;
                    break;

                default:
                    break;
            }
            Room room = GetRoomatMaze(x, y);

            return room;
        }
    }
}
