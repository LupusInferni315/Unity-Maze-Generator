public enum Direction
{
    North, East, South, West
}

public static class DirectionExtensions
{
    public static Direction Opposite ( this Direction direction ) => (Direction) ( ( (int) direction + 2 ) % 4 );

    public static int GetXOffset ( this Direction direction )
    {
        return direction switch
        {
            Direction.East => 1,
            Direction.West => -1,
            _ => 0,
        };
    }

    public static int GetYOffset ( this Direction direction )
    {
        return direction switch
        {
            Direction.South => -1,
            Direction.North => 1,
            _ => 0,
        };
    }
}
