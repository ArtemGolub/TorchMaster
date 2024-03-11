using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosiotion;
        public Vector2Int maxPosition;

        public bool obligatory;
        
        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn
            // 1 - can spawn
            // 2 - has to spawn
            if (x >= minPosiotion.x && x <= maxPosition.x && y >= minPosiotion.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }
            
            return 0;
        }
    }
    
    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offSet;
    private List<Cell> _board;
    void Start()
    {
        MazeGenerator();
    }
    
    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var randomRoom = -1;
                List<int> avaliableRooms = new List<int>();

                for (int k = 0; k < rooms.Length; k++)
                {
                    int p = rooms[k].ProbabilityOfSpawning(i, j);
                    if (p == 2)
                    {
                        randomRoom = k;
                        break;
                    }
                    else if(p == 1)
                    {
                        avaliableRooms.Add(k);
                    }
                }

                if (randomRoom == - 1)
                {
                    if (avaliableRooms.Count > 0)
                    {
                        randomRoom = avaliableRooms[Random.Range(0, avaliableRooms.Count)];
                    }
                    else
                    {
                        randomRoom = 0;
                    }
                }
                
                Cell currentCell = _board[i + j * size.x];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offSet.x, 0, -j * offSet.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(_board[i+ j*size.x].status);
                
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }
    
    
    
    void MazeGenerator()
    {
        _board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                _board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            _board[currentCell].visited = true;
            if(currentCell == _board.Count - 1)
            {
                break;
            }
            
            List<int> neigbors = CheckNeighbors(currentCell);
            if (neigbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neigbors[Random.Range(0, neigbors.Count)];

                if (newCell > currentCell)
                {
                    // down or right
                    if (newCell - 1 == currentCell)
                    {
                        _board[currentCell].status[2] = true;
                        currentCell = newCell;
                        _board[currentCell].status[3] = true;
                    }
                    else
                    {
                        _board[currentCell].status[1] = true;
                        currentCell = newCell;
                        _board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // up or left
                    if (newCell + 1 == currentCell)
                    {
                        _board[currentCell].status[3] = true;
                        currentCell = newCell;
                        _board[currentCell].status[2] = true;
                    }
                    else
                    {
                        _board[currentCell].status[0] = true;
                        currentCell = newCell;
                        _board[currentCell].status[1] = true;
                    }
                }
            }
        }
        
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // checkUp
        if (cell - size.x >= 0 && !_board[cell - size.x].visited)
        {
            neighbors.Add(cell - size.x);
        }
        // checkDown
        if (cell + size.x < _board.Count && !_board[cell + size.x].visited)
        {
            neighbors.Add(cell + size.x);
        }
        // checkRight
        if ((cell+1) % size.x != 0 && !_board[cell + 1].visited)
        {
            neighbors.Add(cell + 1);
        }
        //checkLeft
        if (cell % size.x != 0 && !_board[cell - 1].visited)
        {
            neighbors.Add(cell -1);
        }
        return neighbors;
    }
}
