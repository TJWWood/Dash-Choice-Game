using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWallPoints : MonoBehaviour
{
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        points = ScaleHardWall.pointsOut;
        Debug.Log("HARD POINTS" + points);
        //rounded number of(speed of hard side(maybe + mid) + (scale addition * 2) / 2)
    }
}
