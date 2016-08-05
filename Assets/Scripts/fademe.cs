using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fademe : MonoBehaviour {

    public float waitTime = 0;
    public float stayTime = 2;
    public bool loadLevel = false;
    float alpha = 0;


	// Use this for initialization
	void Start () {
        StartCoroutine(FadeTo(waitTime, 1, 1.5f));
        StartCoroutine(FadeTo(waitTime + stayTime + 1, 0.0f, 1.5f));
	}


    IEnumerator FadeTo(float waitfor, float aValue, float aTime)
    {
        yield return new WaitForSeconds(waitfor);

        float alpha = GetComponent<Text>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<Text>().color = newColor;

            if(loadLevel == true && alpha <= 4f && aValue == 0.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                yield return null;
            }
        }
    }
}
