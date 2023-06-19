using UnityEngine;

public class WallsAndPillarsVisualizer : MazeVisualizer
{
    [SerializeField] private GameObject m_FloorPrefab;
    [SerializeField] private GameObject m_WallPrefab;
    [SerializeField] private GameObject m_PillarPrefab;
    [SerializeField] private float m_CellSize;

    public override void GenerateMazeVisual ( Maze maze )
    {
        Vector3 pillrot = new ( 0, 180, 0 );
        for ( int y = 0; y < maze.Height; y++ )
        {
            for ( int x = 0; x < maze.Width; x++ )
            {
                Cell cell = maze [ x, y ];
                Vector3 floorPosition = new ( x * m_CellSize, 0f, y * m_CellSize );
                GameObject go = Instantiate ( m_FloorPrefab, floorPosition, Quaternion.identity, transform );
                MeshRenderer renderer = go.GetComponent<MeshRenderer> ();

                if ( renderer )
                {
                    if ( floorPosition == (Vector3) maze.Start )
                        renderer.material.color *= Color.green;
                    else if ( floorPosition == (Vector3) maze.End )
                        renderer.material.color *= Color.red;
                }

                SpawnWallIfTrue ( cell, Direction.South, floorPosition );
                SpawnWallIfTrue ( cell, Direction.West, floorPosition );

                if ( x == maze.Width - 1 )
                    SpawnWallIfTrue ( cell, Direction.East, floorPosition );

                if ( y == maze.Height - 1 )
                    SpawnWallIfTrue ( cell, Direction.North, floorPosition );

                /* this isn't working right, so I've opted for a 'brute' force approach of spawning pillars at every corner as seen below
                if ( x < maze.Width - 1 && y < maze.Height - 1 )
                {
                    bool e = maze [ x, y ].GetWall ( Direction.East );
                    bool n = maze [ x + 1, y ].GetWall ( Direction.North );
                    bool w = maze [ x + 1, y + 1 ].GetWall ( Direction.West );
                    bool s = maze [ x, y + 1 ].GetWall ( Direction.South );

                    int c = ( e ? 1 : 0 ) + ( n ? 1 : 0 ) + ( w ? 1 : 0 ) + ( s ? 1 : 0 );

                    if ( (c == 1 || c == 2) && ( !e || !w ) && ( !n || !s ) )
                    {
                        _= Instantiate ( m_PillarPrefab, floorPosition, Quaternion.Euler ( pillrot ), transform );
                    }
                }
                */
            }
        }

        for ( int y = -1; y < maze.Height; y++ )
        {

            for ( int x = -1; x < maze.Width; x++ )
            {
                Vector3 pillpos = new ( x * m_CellSize, 0, y * m_CellSize );
                _ = Instantiate ( m_PillarPrefab, pillpos, Quaternion.Euler ( pillrot ), transform );
            }
        }
    }

    private void SpawnWallIfTrue ( Cell cell, Direction direction, Vector3 position )
    {
        if ( cell.GetWall ( direction ) )
        {
            Vector3 euler = Vector3.zero;

            switch ( direction )
            {
                case Direction.North:
                    euler = new Vector3 ( 0, 180, 0 );
                    break;
                case Direction.East:
                    euler = new Vector3 ( 0, 270, 0 );
                    break;
                case Direction.South:
                    euler = new Vector3 ( 0, 0, 0 );
                    break;
                case Direction.West:
                    euler = new Vector3 ( 0, 90, 0 );
                    break;
            }

            _ = Instantiate ( m_WallPrefab, position, Quaternion.Euler ( euler ), transform );
        }
    }

    public override void Clear ()
    {
        for ( int i = transform.childCount; i >= 0; i-- )
        {
            Destroy ( transform.GetChild ( 0 ).gameObject );
        }
    }
}
