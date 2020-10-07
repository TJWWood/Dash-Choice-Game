using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWin : MonoBehaviour
{
    public GameObject walls;
    public static bool leftWin;
    public static bool rightWin;
    public GameObject leftWall;
    public GameObject middleWall;
    public GameObject rightWall;
    public GameObject leftGoalWall;
    public GameObject rightGoalWall;
    public GameObject defaults;
    public GameObject leftBall;
    public GameObject rightBall;

    public GameObject leftTouch;
    public GameObject rightTouch;
    public GameObject touchSides;

    bool rightBallMovedToLeftBall;
    bool movingLeftBall;
    bool rightBallMovedToNewRightPos;

    bool leftBallMovedToRightBall;
    bool movingRightBall;
    bool leftBallMovedToNewLeftPos;

    Vector3 newWallsPos;
    Vector3 newCam;
    Vector3 newTouch;

    bool setWallLoc;
    bool done;

    float pieceResetSpeed = 10.0f;

    public static event Action RoundWin = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DecideSideDifficulty>().enabled = true;

        leftWin = false;
        rightWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftWin || rightWin)
        {
            if (leftWin)
            {
                if(leftGoalWall.TryGetComponent(out HardWallPoints hwp))
                {
                    DetailTracker.pointsForRound = hwp.points;
                }
                else
                {
                    DetailTracker.pointsForRound = 1;
                }
                //right win false
                movingLeftBall = true;
                movingRightBall = false;
                leftGoalWall.AddComponent<UnDissolve>();
            }
            else if (rightWin)
            {
                if (rightGoalWall.TryGetComponent(out HardWallPoints hwp))
                {
                    DetailTracker.pointsForRound = hwp.points;
                }
                else
                {
                    DetailTracker.pointsForRound = 1;
                }
                //right win true
                movingRightBall = true;
                movingLeftBall = false;
                
            }

            if (leftWall.TryGetComponent(out ScaleHardWall shwL))
            {
                Destroy(shwL);
                Destroy(rightWall.GetComponent<ScaleNormalWall>());
            }
            else if (leftWall.TryGetComponent(out ScaleNormalWall snwL))
            {
                Destroy(snwL);
                Destroy(rightWall.GetComponent<ScaleHardWall>());
            }

            leftWall.transform.localScale = defaults.GetComponent<DefaultValues>().leftTransform;
            rightWall.transform.localScale = defaults.GetComponent<DefaultValues>().rightTransform;
            middleWall.transform.localScale = defaults.GetComponent<DefaultValues>().middleTransform;
            rightGoalWall.transform.localScale = defaults.GetComponent<DefaultValues>().rightGoalTransform;
            leftGoalWall.transform.localScale = defaults.GetComponent<DefaultValues>().leftGoalTransform;
        
            if (movingLeftBall)
            {
                //leftBall.GetComponent<Rigidbody>().isKinematic = true;
                rightBall.GetComponent<Rigidbody>().isKinematic = true;

                MoveLeftBallToNewPos();
            }

            if(movingRightBall)
            {
                leftBall.GetComponent<Rigidbody>().isKinematic = true;
                MoveRightBallToNewPos();
            }

            Destroy(middleWall.GetComponent<ScaleMiddleWall>());
        }

        if (rightBallMovedToLeftBall)
        {
            rightBall.AddComponent<UnDissolve>();
            rightBall.SetActive(true);
            MoveRightBallToNewPosFromLeftBall();
        }
        else if(leftBallMovedToRightBall)
        { 
            leftBall.SetActive(true);
            MoveLeftBallToNewPosFromRightBall();
        }

        if(rightBallMovedToNewRightPos || leftBallMovedToNewLeftPos)
        {
            MoveWallsUp();
        }

        if(done)
        {
            RoundWin();
            gameObject.GetComponent<DecideSideDifficulty>().enabled = true;
            done = false;
            //Debug.LogError("done");
            leftTouch.SetActive(true);
            rightTouch.SetActive(true);
        }
    }

    void MoveLeftBallToNewPos()
    {
        ////Debug.Log("left win");
        Vector3 newPosL = new Vector3(GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.x, GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.y + 4f, GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.z);
        leftBall.transform.position = Vector3.MoveTowards(leftBall.transform.position, newPosL, pieceResetSpeed * Time.deltaTime);
        if (Vector3.Distance(leftBall.transform.position, newPosL) < 0.001f)
        {
            ////Debug.Log("Left moved check 1");
            GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform = newPosL;
            leftBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            movingLeftBall = false;

            ////Debug.Log("left moved to new pos check 2");
            if (!rightBallMovedToLeftBall)
            {
                rightBall.transform.position = new Vector3(leftBall.transform.position.x, leftBall.transform.position.y, leftBall.transform.position.z);
                rightBallMovedToLeftBall = true;
                //Debug.Log("right moved to left ball pos check 1");

                leftBall.GetComponent<Rigidbody>().isKinematic = true;
            }
            leftWin = false;
        }
    }

    void MoveRightBallToNewPosFromLeftBall()
    {
        ////Debug.Log("right moved to left ball pos check 2 + right ball moving" + rightBallMovedToLeftBall);
        Vector3 newPosLtoR = new Vector3(GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.x, GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.y + 4f, GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.z);
        rightBall.transform.position = Vector3.MoveTowards(rightBall.transform.position, newPosLtoR, pieceResetSpeed * Time.deltaTime);
        if (Vector3.Distance(rightBall.transform.position, newPosLtoR) < 0.001f)
        {
            GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform = newPosLtoR;
            //////Debug.LogWarning("RIght moved to new pos");
            rightBall.GetComponent<Rigidbody>().velocity = Vector3.zero;

            leftBall.GetComponent<Rigidbody>().isKinematic = false;
            rightBall.GetComponent<Rigidbody>().isKinematic = false;

            rightBallMovedToLeftBall = false;
            rightBallMovedToNewRightPos = true;
            setWallLoc = true;
        }
    }


    //these two causing issues for right side
    void MoveRightBallToNewPos()
    {
        ////Debug.Log("left win");
        Vector3 newPosR = new Vector3(GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.x, GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.y + 4f, GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform.z);
        rightBall.transform.position = Vector3.MoveTowards(rightBall.transform.position, newPosR, pieceResetSpeed * Time.deltaTime);
        if (Vector3.Distance(rightBall.transform.position, newPosR) < 0.001f)
        {
            ////Debug.Log("Left moved check 1");
            GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform = newPosR;
            rightBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            movingRightBall = false;

            ////Debug.Log("left moved to new pos check 2");
            if (!leftBallMovedToRightBall)
            {
                leftBall.transform.position = new Vector3(rightBall.transform.position.x, rightBall.transform.position.y, rightBall.transform.position.z);
                leftBallMovedToRightBall = true;
                //Debug.Log("right moved to left ball pos check 1");

                rightBall.GetComponent<Rigidbody>().isKinematic = true;
            }
            rightWin = false;
        }
    }

    void MoveLeftBallToNewPosFromRightBall()
    {
        ////Debug.Log("right moved to left ball pos check 2 + right ball moving" + rightBallMovedToLeftBall);
        Vector3 newPosRtoL = new Vector3(GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.x, GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.y + 4f, GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform.z);
        leftBall.transform.position = Vector3.MoveTowards(leftBall.transform.position, newPosRtoL, pieceResetSpeed * Time.deltaTime);
        if (Vector3.Distance(leftBall.transform.position, newPosRtoL) < 0.001f)
        {
            GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform = newPosRtoL;
            //////Debug.LogWarning("RIght moved to new pos");
            leftBall.GetComponent<Rigidbody>().velocity = Vector3.zero;

            rightBall.GetComponent<Rigidbody>().isKinematic = false;
            leftBall.GetComponent<Rigidbody>().isKinematic = false;

            leftBallMovedToRightBall = false;
            leftBallMovedToNewLeftPos = true;
            setWallLoc = true;
        }
    }

    void MoveWallsUp()
    {
        if (setWallLoc)
        {
            leftBall.GetComponent<Rigidbody>().isKinematic = true;
            rightBall.GetComponent<Rigidbody>().isKinematic = true;
            leftBall.GetComponent<SphereCollider>().enabled = false;
            rightBall.GetComponent<SphereCollider>().enabled = false;

            //leftGoalWall.GetComponent<BoxCollider>().enabled = false;
            //rightGoalWall.GetComponent<BoxCollider>().enabled = false;

            newWallsPos = new Vector3(walls.transform.position.x, walls.transform.position.y + 4.0f, walls.transform.position.z);
            newCam = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 4f, Camera.main.transform.position.z);
            newTouch = new Vector3(touchSides.transform.position.x, touchSides.transform.position.y + 4f, touchSides.transform.position.z);

            //Debug.LogError("WallLoc");
            setWallLoc = false;
        }
         
        walls.transform.position = Vector3.MoveTowards(walls.transform.position, newWallsPos, pieceResetSpeed * Time.deltaTime);
        
        if(Vector3.Distance(walls.transform.position, newWallsPos) < 0.001f)
        {
            touchSides.transform.position = newTouch;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newCam, pieceResetSpeed * Time.deltaTime);
            if(Vector3.Distance(Camera.main.transform.position, newCam) < 0.001f)
            {
                rightBallMovedToNewRightPos = false;
                leftBallMovedToNewLeftPos = false;
                done = true;
                leftBall.GetComponent<Rigidbody>().isKinematic = false;
                rightBall.GetComponent<Rigidbody>().isKinematic = false;
                leftBall.GetComponent<SphereCollider>().enabled = true;
                rightBall.GetComponent<SphereCollider>().enabled = true;
                leftBall.AddComponent<UnDissolve>();
                rightGoalWall.AddComponent<UnDissolve>(); 
            }
            //Camera.main.transform.position = new Vector3(0f, Camera.main.transform.position.y + 4f, 0f);
        }
    }
}
