using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleNormalWall : MonoBehaviour
{
    private float currentScaleSize;
    public float additionToScale;
    public int scaleSpeed;

    private void Start()
    {
        //default max scale to at least current size
        additionToScale = Random.Range(0.0f, 0.4f);
        currentScaleSize = transform.localScale.x;
        currentScaleSize += additionToScale;
        scaleSpeed = Random.Range(1, 3);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * scaleSpeed) + currentScaleSize, transform.localScale.y, transform.localScale.z);

        transform.localScale = vec;
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
