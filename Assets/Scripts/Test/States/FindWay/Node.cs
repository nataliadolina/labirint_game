using UnityEngine;

public class Node
{
    public Vector3 WorldPosition;
    public Vector2 Cell;
    public int PathLengthFromStart;

    /// <summary>
    /// The node, which we came from
    /// </summary>
    public Node CameFrom;

    /// <summary>
    /// Full distance to target
    /// </summary>
    public float HeuristicEstimatePathLength;

    /// <summary>
    /// Full distance to target
    /// </summary>
    public float EstimateFullPathLength
    {
        get
        {
            return PathLengthFromStart + HeuristicEstimatePathLength;
        }
    }
    
}
