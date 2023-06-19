using System;
using UnityEngine;

[Serializable]
public class DirectionArray<T>
{
    #region Fields

    [SerializeField] private T m_North;
    [SerializeField] private T m_East;
    [SerializeField] private T m_South;
    [SerializeField] private T m_West;

    #endregion

    #region Indexers

    public T this [ Direction direction ]
    {
        get
        {
            switch ( direction )
            {
                case Direction.North:
                    return m_North;
                case Direction.East:
                    return m_East;
                case Direction.South:
                    return m_South;
                case Direction.West:
                    return m_West;
                default:
                    throw new ArgumentException ( $"Invalid direction: {direction}", nameof ( direction ) );
            }
        }

        set
        {
            switch ( direction )
            {
                case Direction.North:
                    m_North = value;
                    break;
                case Direction.East:
                    m_East = value;
                    break;
                case Direction.South:
                    m_South = value;
                    break;
                case Direction.West:
                    m_West = value;
                    break;
                default:
                    throw new ArgumentException ( $"Invalid direction: {direction}", nameof ( direction ) );
            }
        }
    }

    #endregion
}
