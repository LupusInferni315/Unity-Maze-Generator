using System;
using UnityEngine;
using Random = System.Random;

public sealed class RecursiveBacktrackingGenerator : MazeGenerator
{
    #region Fields

    [SerializeField] private bool m_UseRandomSeed = false;
    [SerializeField] private int m_Seed = 315;

    #endregion

    #region Methods

    public override void Generate ( Maze maze )
    {
        Random rng = new ( m_UseRandomSeed ? Guid.NewGuid ().GetHashCode () : m_Seed );
        Direction d = (Direction) rng.Next ( 4 );
        Direction e = d.Opposite ();

        Vector2Int start = GeneratePointOnWall ( d, rng, maze );
        Vector2Int end = GeneratePointOnWall ( e, rng, maze );

        maze.SetStartAndEndPoints ( start, end );
        
        Cell startCell = maze [ start.x, start.y ];
        startCell.SetWall ( d, false );

        Cell endCell = maze [ end.x, end.y ];
        endCell.SetWall ( e, false );

        GenerateFromCell ( maze, start.x, start.y, rng );
    }

    private void GenerateFromCell ( Maze maze, int x, int y, Random rng )
    {
        maze [ x, y ].Visited = true;

        Direction [] directions = ShuffleDirections ( rng );

        foreach ( Direction direction in directions )
        {
            int nx = x + direction.GetXOffset ();
            int ny = y + direction.GetYOffset ();

            if ( maze.InMazeRange ( nx, ny ) && !maze [ nx, ny ].Visited )
            {
                maze [ x, y ].SetWall ( direction, false );
                GenerateFromCell ( maze, nx, ny, rng );
            }
        }
    }

    private Direction [] ShuffleDirections ( Random rng )
    {
        // Create an array of directions and shuffle it
        Direction [] directions = new Direction [] { Direction.North, Direction.East, Direction.South, Direction.West };
        int n = directions.Length;
        while ( n > 1 )
        {
            int k = rng.Next ( n );
            n--;
            (directions [ k ], directions [ n ]) = (directions [ n ], directions [ k ]);
        }

        return directions;
    }

    private Vector2Int GeneratePointOnWall ( Direction wall, Random rng, Maze maze )
    {
        int sx = 0;
        int sy = 0;

        switch ( wall )
        {
            case Direction.North:
                sx = rng.Next ( 0, maze.Width );
                sy = maze.Height - 1;
                break;
            case Direction.East:
                sx = maze.Width - 1;
                sy = rng.Next ( 0, maze.Height );
                break;
            case Direction.South:
                sx = rng.Next ( 0, maze.Width );
                sy = 0;
                break;
            case Direction.West:
                sx = 0;
                sy = rng.Next ( 0, maze.Height );
                break;
        }

        return new ( sx, sy );

    }

    #endregion
}
