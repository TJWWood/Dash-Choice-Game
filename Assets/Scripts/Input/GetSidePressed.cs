using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSidePressed : MonoBehaviour
{
    public GameObject leftBall;
    public GameObject rightBall;

    public GameObject leftTouch;
    public GameObject rightTouch;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name.Equals("LeftTouch"))
                {
                    Debug.Log("Chose left side");
                    leftBall.AddComponent<BallLogic>();

                    rightBall.AddComponent<DissolveWall>();

                    leftTouch.SetActive(false);
                    rightTouch.SetActive(false);
                }
                else if (hit.transform.name.Equals("RightTouch"))
                {
                    Debug.Log("Chose right side");
                    rightBall.AddComponent<BallLogic>();

                    leftBall.AddComponent<DissolveWall>();

                    leftTouch.SetActive(false);
                    rightTouch.SetActive(false);
                }
            }
        }
    }
}
