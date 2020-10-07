using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDissolve : MonoBehaviour
{
    bool isDissolving = false;
    float fade = 1.0f;

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
        if (isDissolving)
        {
            fade -= Time.deltaTime * 5f;
            if (fade <= 0.0f)
            {
                isDissolving = false;
                Destroy(gameObject.GetComponent<UnDissolve>());
                gameObject.SetActive(true);
                Debug.Log("done");
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
