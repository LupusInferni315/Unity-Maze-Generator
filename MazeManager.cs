using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private MazeGenerator m_Generator;
    [SerializeField] private MazeVisualizer [] m_Visualizers;

    #endregion

    #region Methods

    private void Awake ()
    {
        Maze maze = m_Generator.Generate ( 16, 16 );

        foreach ( MazeVisualizer visualizer in m_Visualizers )
        {
            visualizer.GenerateMazeVisual ( maze );
        }
    }

    #endregion
}
