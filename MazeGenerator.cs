using UnityEngine;

public abstract class MazeGenerator : MonoBehaviour
{
    public Maze Generate ( int width, int height )
    {
        Maze maze = new ( width, height );
        Generate ( maze );
        return maze;
    }

    public abstract void Generate ( Maze maze );
}
