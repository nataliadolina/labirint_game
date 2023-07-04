using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extensions;
using Maze;
using Interfaces;

internal class PathGenerator
{
    #region описание алгоритма нахождения пути
    //1. Создается 2 списка вершин — ожидающие рассмотрения и уже рассмотренные.
    //В ожидающие добавляется точка старта, список рассмотренных пока пуст.
    //2. Для каждой точки рассчитывается F = G + H. G — расстояние от старта до точки,
    //H — примерное расстояние от точки до цели. О расчете этой величины я расскажу позднее.
    //Так же каждая точка хранит ссылку на точку, из которой в нее пришли.
    //3. Из списка точек на рассмотрение выбирается точка с наименьшим F. Обозначим ее X.
    //4. Если X — цель, то мы нашли маршрут.
    //5. Переносим X из списка ожидающих рассмотрения в список уже рассмотренных.
    //6. Для каждой из точек, соседних для X (обозначим эту соседнюю точку Y), делаем следующее:
    //7. Если Y уже находится в рассмотренных — пропускаем ее.
    //8. Если Y еще нет в списке на ожидание — добавляем ее туда,
    //запомнив ссылку на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.
    //9. Если же Y в списке на рассмотрение — проверяем, если X.G + расстояние от X до Y < Y.G,
    //значит мы пришли в точку Y более коротким путем, заменяем Y.G на X.G + расстояние от X до Y,
    //а точку, из которой пришли в Y на X.
    //10. Если список точек на рассмотрение пуст, 
    //а до цели мы так и не дошли — значит маршрут не существует.
#endregion

    private IMazeDataGenerator _mazeDataGenerator;

    internal PathGenerator(IMazeDataGenerator mazeDataGenerator)
    {
        _mazeDataGenerator = mazeDataGenerator;
    }

    public List<Vector2> GeneratePath(Vector2 startCell, Vector2 targetCell)
    {
        // Шаг 1.
        var closedSet = new List<Node>();
        var openSet = new List<Node>();
        // Шаг 2.
        Node startNode = new Node()
        {
            Cell = startCell,
            HeuristicEstimatePathLength = GetHeuristicPathLength(startCell, targetCell),
            PathLengthFromStart = 0,
            CameFrom = null,
        };
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            // Шаг 3.
            Node currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
            // Шаг 4.
            if (isEquel(currentNode.Cell, targetCell))
            {
                List<Vector2> path = GetPathForNode(currentNode);
                return path;
            }
                
            // Шаг 5.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // Шаг 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, targetCell, _mazeDataGenerator.Maze))
            {
                // Шаг 7. 
                if (closedSet.Count(node => isEquel(node.Cell, neighbourNode.Cell)) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                  isEquel(node.Cell, neighbourNode.Cell));
                // Шаг 8.
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else
                {
                    if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
        }

        // Шаг 10.
        return null;
    }
    private bool isEquel(Vector2 cell1, Vector2 cell2)
    {
        return cell1.x == cell2.x && cell1.y == cell2.y;
    }
    private int GetDistanceBetweenNeighbours()
    {
        return 1;
    }
    private float GetHeuristicPathLength(Vector2 from, Vector2 to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }
    private List<Vector2> GetPathForNode(Node pathNode)
    {
        var result = new List<Vector2>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Cell);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }
    private List<Node> GetNeighbours(Node pathNode, Vector2 goal, int[,] field)
    {
        var result = new List<Node>();
        List<Vector2> neighbours = pathNode.Cell.GetNeighbours(1, field);
        foreach (Vector2 cell in neighbours)
        {
            var neighbourNode = new Node()
            {
                Cell = cell,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(),
                HeuristicEstimatePathLength = GetHeuristicPathLength(cell, goal)
            };
            result.Add(neighbourNode);
        }
        return result;
    }
}
