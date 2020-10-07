using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideSideDifficulty : MonoBehaviour
{
    private int side;

    public GameObject leftWall;
    public GameObject middleWall;
    public GameObject rightWall;
    public GameObject leftGoalWall;
    public GameObject rightGoalWall;
    void OnEnable()
    {
        side = Random.Range(0, 2);
        //Debug.Log("ENABLED");
        switch(side)
        {
            case 0:
                Debug.Log("LEFT HARD");
                leftWall.AddComponent<ScaleHardWall>();
                rightWall.AddComponent<ScaleNormalWall>();
                middleWall.AddComponent<ScaleMiddleWall>();

                if(leftGoalWall.TryGetComponent(out HardWallPoints hwp))
                {
                    Destroy(hwp);
                    leftGoalWall.AddComponent<HardWallPoints>();
                }
                else
                {
                    leftGoalWall.AddComponent<HardWallPoints>();
                }

                if (rightGoalWall.TryGetComponent(out NormalWallPoints nwp))
                {
                    Destroy(nwp);
                    rightGoalWall.AddComponent<NormalWallPoints>();
                }
                else
                {
                    rightGoalWall.AddComponent<NormalWallPoints>();
                }
                break;

            case 1:
                Debug.Log("RIGHT HARD");

                leftWall.AddComponent<ScaleNormalWall>();
                rightWall.AddComponent<ScaleHardWall>();
                middleWall.AddComponent<ScaleMiddleWall>();

                if (rightGoalWall.TryGetComponent(out HardWallPoints hwp2))
                {
                    Destroy(hwp2);
                    rightGoalWall.AddComponent<HardWallPoints>();
                }
                else
                {
                    rightGoalWall.AddComponent<HardWallPoints>();
                }

                if (leftGoalWall.TryGetComponent(out NormalWallPoints nwp2))
                {
                    Destroy(nwp2);
                    leftGoalWall.AddComponent<NormalWallPoints>();
                }
                else
                {
                    leftGoalWall.AddComponent<NormalWallPoints>();
                }
                break;
        }
        enabled = false;
        //Debug.Log("DISABLED");

    }
}
