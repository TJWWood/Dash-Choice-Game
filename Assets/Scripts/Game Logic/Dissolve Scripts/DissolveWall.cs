using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveWall : MonoBehaviour
{
    bool isDissolving = false;
    float fade = 0f;

    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        isDissolving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolving)
        {
            fade += Time.deltaTime * 2f;
            if(fade >= 1.0f)
            {
                isDissolving = false;
                Destroy(gameObject.GetComponent<DissolveWall>());
                //gameObject.SetActive(false);
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
