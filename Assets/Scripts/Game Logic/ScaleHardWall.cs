using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHardWall : MonoBehaviour
{
    private float currentScaleSize;
    public float additionToScale;
    public int scaleSpeed;
    private float points;
    public static int pointsOut;

    private void Start()
    {
        //default max scale to at least current size
        additionToScale = Random.Range(0.4f, 0.9f);
        currentScaleSize = transform.localScale.x;
        currentScaleSize += additionToScale;
        scaleSpeed = Random.Range(5, 11);
        CalculateHardPoints();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * scaleSpeed) + currentScaleSize, transform.localScale.y, transform.localScale.z);

        transform.localScale = vec;
    }

    void CalculateHardPoints()
    {
        points += scaleSpeed;
        points += additionToScale * 2;
        points /= 2;
        pointsOut = Mathf.RoundToInt(points);
        //Debug.Log("POINTS FOR HARD WALL" + points);
    }

    //middle 1-4 speed rand
    //normal (left or right rand) 1-2 speed rand
    //hard (left or right rand) 6-10 speed rand

    //normal scale addition 0.1-0.3
    //middle scale addition 0.0-0.2
    //hard scale addition 0.5-0.8

    //normal side point always 1
    //hard side point determined by rounded number of (speed of hard side (maybe + mid) + (scale addition * 2) / 2)
}
