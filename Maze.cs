using System;
using UnityEngine;
using Random = System.Random;

public class Maze
{
    #region Fields

    private readonly Cell [,] m_Cells;
    private Vector2Int m_Start;
    private Vector2Int m_End;

    #endregion

    #region Properties

    public int Width => m_Cells.GetLength ( 0 );

    public int Height => m_Cells.GetLength ( 1 );

    public Vector3Int Start => new ( m_Start.x, 0, m_Start.y );
    public Vector3Int End => new ( m_End.x, 0, m_End.y );

    #endregion

    #region Indexers

    public Cell this [ int x, int y ] => m_Cells [ x, y ];

    #endregion

    #region Constructors

    public Maze ( int width, int height )
    {
        m_Cells = new Cell [ width, height ];

        for ( int y = 0; y < height; y++ )
        {
            for ( int x = 0; x < width; x++ )
            {
                Cell cell = m_Cells [ x, y ] = new Cell ( x, y );

                if ( x > 0 )
                {
                    cell.SetNeighbor ( Direction.West, m_Cells [ x - 1, y ] );
                }

                if ( y > 0 )
                {
                    cell.SetNeighbor ( Direction.South, m_Cells [ x, y - 1 ] );
                }
            }
        }
    }

    #endregion

    #region Methods

    public bool InMazeRange ( int x, int y ) => x >= 0 && x < Width && y >= 0 && y < Height;
 
    public void SetStartAndEndPoints ( Vector2Int start, Vector2Int end )
    {
        m_Start = start;
        m_End = end;
    }

    #endregion
}
