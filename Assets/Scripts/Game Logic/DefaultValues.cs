using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultValues : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject middleWall;
    public GameObject rightWall;
    public GameObject leftGoalWall;
    public GameObject rightGoalWall;

    public Vector3 leftTransform;
    public Vector3 rightTransform;
    public Vector3 middleTransform;
    public Vector3 leftGoalTransform;
    public Vector3 rightGoalTransform;

    public GameObject leftBall;
    public GameObject rightBall;

    public Vector3 leftBallTransform;
    public Vector3 rightBallTransform;

    // Start is called before the first frame update
    void Start()
    {
        leftTransform = leftWall.transform.localScale;
        rightTransform = rightWall.transform.localScale;
        middleTransform = middleWall.transform.localScale;
        leftGoalTransform = leftGoalWall.transform.localScale;
        rightGoalTransform = rightGoalWall.transform.localScale;

        leftBallTransform = leftBall.transform.localPosition;
        rightBallTransform = rightBall.transform.localPosition;
    }
}
