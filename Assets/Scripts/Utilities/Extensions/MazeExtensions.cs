using System.Collections.Generic;
using UnityEngine;
using Maze;
using System.Linq;

namespace Utilities.Extensions
{
    internal static class MazeExtensions
    {
        internal static List<Vector2> GetNeighbours(this Vector2 cell, int maxDistance, int[,] field)
        {
            List<Vector2> result = new List<Vector2>();

            for (int i = 0; i<4; i++)
            {
                Vector2 c = new Vector2(cell.x, cell.y);
                var currentDistance = maxDistance;
                while (currentDistance >= 1)
                {
                    switch (i)
                    {
                        case 0:
                            c.x = cell.x + currentDistance;
                            break;
                        case 1:
                            c.x = cell.x - currentDistance;
                            break;

                        case 2:
                            c.y = cell.y + currentDistance;
                            break;
                        case 3:
                            c.y = cell.y - currentDistance;
                            break;
                    }

                    currentDistance--;

                    if (!IsCellAvailable(c, field))
                    {
                        continue;
                    }
                    if (!result.Any(x => x.x == c.x && x.y == c.y))
                    {
                        result.Add(c);
                    }
                    break;
                }
            }
            return result;
        }

        internal static bool IsCellAvailable(Vector2 cell, int[,] field)
        {
            if (cell.x < 0 || cell.x >= field.GetLength(0))
            {
                return false;
            }
                
            if (cell.y < 0 || cell.y >= field.GetLength(1))
            {
                return false;
            }

            if (field[(int)cell.x, (int)cell.y] != (int)MazeSigns.EmptySpace)
            {
                return false;
            }

            return true;
        }

        internal static List<T> Shuffle<T>(this List<T> data)
        {
            for (int i = data.Count - 1; i >= 1; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }

            return data;
        }
    }
}
