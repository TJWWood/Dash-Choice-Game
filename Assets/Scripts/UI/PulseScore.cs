using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulseScore : MonoBehaviour
{
    private TextMeshProUGUI score;
    public int scoreValue;
    // Start is called before the first frame update
    void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
        CheckForWin.RoundWin += RunCoroutine;
    }

    void Start()
    {
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {

        score.text = scoreValue.ToString();
    }

    private IEnumerator Pulse()
    {
        for (float i = 1f; i <= 1.4f; i += 0.05f)
        {
            score.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        score.rectTransform.localScale = new Vector3(1.4f, 1.4f, 1.4f);

        scoreValue += DetailTracker.pointsForRound;

        for (float i = 1.4f; i >= 1.0f; i -= 0.05f)
        {
            score.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        score.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void RunCoroutine()
    {
        StartCoroutine(Pulse());
    }

    private void OnDestroy()
    {
        CheckForWin.RoundWin -= RunCoroutine;
    }
}
