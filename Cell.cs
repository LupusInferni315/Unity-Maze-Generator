public class Cell
{
    #region Fields

    private readonly int m_X;
    private readonly int m_Y;
    private readonly DirectionArray<Cell> m_Neighbors;
    private readonly DirectionArray<bool> m_Walls;
    private bool m_Visited = false;

    #endregion

    #region 'Properties'

    public int X => m_X;
    public int Y => m_Y;

    public Cell GetNeighbor ( Direction direction ) => m_Neighbors [ direction ];

    public void SetNeighbor ( Direction direction, Cell cell )
    {
        m_Neighbors [ direction ] = cell;
        cell.m_Neighbors [ direction.Opposite () ] = this;
    }

    public bool GetWall ( Direction direction ) => m_Walls [ direction ];

    public void SetWall ( Direction direction, bool value )
    {
        m_Walls [ direction ] = value;
        Cell neighbor = m_Neighbors [ direction ];

        if ( neighbor != null )
            neighbor.m_Walls [ direction.Opposite () ] = value;
    }

    public bool Visited { get => m_Visited; set => m_Visited = value; }

    #endregion

    #region Constructors

    public Cell ( int x, int y )
    {
        m_X = x;
        m_Y = y;
        m_Neighbors = new ();
        m_Walls = new ();
        
        for ( Direction d = Direction.North; d <= Direction.West; d++ )
        {
            m_Walls [ d ] = true;
        }
    }

    #endregion
}
