using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CheckForWin;
using static DetailTracker;

public class BallLogic : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    bool hasClicked = false;
    bool leftCollidedWithWall;
    bool rightCollidedWithWall;
    bool leftTriggerWin;
    bool rightTriggerWin;
    public float ballSpeed = 550f;

    private GameObject pivot;

    float min = -90.0f;
    float max = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        pivot = GameObject.Find("Pivot");
        Vector3 arrowPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z);
        pivot.transform.position = arrowPos;
    }
    // Update is called once per frame
    void Update()
    {
        pivot.transform.Rotate(Vector3.forward, 90f * Time.deltaTime);
        transform.Rotate(Vector3.forward, 90f * Time.deltaTime);

        //Debug.LogError("HAs Clicked: " + hasClicked);
        if (Input.GetKeyDown(KeyCode.Mouse0) && !hasClicked)
        {
            //Debug.Log("clicked");
            m_Rigidbody.AddForce(m_Rigidbody.transform.up * ballSpeed);
            hasClicked = true;
        }

        //once collides with black wall - move back to original pos

        if (leftCollidedWithWall)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform, 75f * Time.deltaTime);
            if(transform.position == GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform)
            {
                hasClicked = false;
                leftCollidedWithWall = false;
                m_Rigidbody.velocity = Vector3.zero;
            }
        }
        else if (rightCollidedWithWall)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform, 75f * Time.deltaTime);
            if (transform.position == GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform)
            {
                hasClicked = false;
                rightCollidedWithWall = false;
                m_Rigidbody.velocity = Vector3.zero;
            }
        }

        if(leftTriggerWin)
        {
            hasClicked = false;
            Destroy(gameObject.GetComponent<BallLogic>());
        }
        else if (rightTriggerWin)
        {
            hasClicked = false;
            Destroy(gameObject.GetComponent<BallLogic>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (name == "Left Ball" && other.gameObject.name == "GoalWallLeft")
            {
                other.gameObject.AddComponent<DissolveWall>();
                leftTriggerWin = true;
                leftWin = true;
            }
            else if (name == "Right Ball" && other.gameObject.name == "GoalWallRight")
            {
                other.gameObject.AddComponent<DissolveWall>();
                rightTriggerWin = true;
                rightWin = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            if (name == "Left Ball")
            {
                leftCollidedWithWall = true;
            }
            else if (name == "Right Ball")
            {
                rightCollidedWithWall = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(gameObject.name == "Left Ball")
        {
            transform.position = GameObject.Find("Defaults").GetComponent<DefaultValues>().leftBallTransform;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            hasClicked = false;
        }
        else if(gameObject.name == "Right Ball")
        {
            transform.position = GameObject.Find("Defaults").GetComponent<DefaultValues>().rightBallTransform;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            hasClicked = false;
        }
    }
}
