using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {

    public enum Direction
    {
        Forward,
        Backward,
        None
    };

    private Direction arrowDirection;

    public Direction ArrowDirection
    {
        get
        {
            return arrowDirection;
        }

        set
        {
            arrowDirection = value;
        }
    }
}
