using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour {

    public TextAsset newGameText;
	public TextAsset textFile;
	string[] dialogueLines;
	int lineNumber = -1;
    [HideInInspector]
    public string name = "Maverick";

	public Text uiText;
	Canvas canvas;
	GameObject panel;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(this.gameObject);
		}

		panel = uiText.transform.parent.gameObject;

        if (PlayerPrefs.GetInt("LastLevel") == -1)
        {
            TriggerDialogue(newGameText);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().Speed = 0;
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (lineNumber < 0) {
			lineNumber = -1;
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			Next ();
		}
	}

    public void TriggerDialogue(TextAsset txt, bool random = false)
    {

        if (random)
        {
            StopCoroutine(hidePanel());
            StartCoroutine(showPanel());
            textFile = txt;
            dialogueLines = (textFile.text.Split("\n"[0]));
            lineNumber = Mathf.RoundToInt(Random.Range(0, dialogueLines.Length) - 1);
            Next();
            StartCoroutine(hidePanel());
        }
        else
        {
            if (textFile != txt)
            {
                lineNumber = -1;
                textFile = txt;
                dialogueLines = (textFile.text.Split("\n"[0]));
                FadeTo(1, 0.1f, false);
                Next();
                StartCoroutine(showPanel());

            }
            else
            {
                Next();
            }
        }
        uiText.transform.parent.gameObject.SetActive(true);
    }

    public void Next(){
		if (lineNumber < dialogueLines.Length - 1) {
			float stayTime = 0;

			StartCoroutine (FadeTo (0, stayTime, true));
		} else {
			//no more dialogue.  back to bizznizz

			if (uiText.text != "") {
				uiText.text = "";
				StartCoroutine (hidePanel ());
			}
		}
	}

	IEnumerator FadeTo(float aValue, float aTime, bool changeText)
	{
		float alpha = uiText.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			uiText.color = newColor;
			yield return null;
		}

		if (changeText) {
			lineNumber++;
			string dialogue = dialogueLines[lineNumber];

            if (dialogue.Contains(":HOME:"))
            {
                lineNumber++;
                FadeManager.Instance.LoadLevel("Home", 1.0f);
            } else if (dialogue.Contains(":ENABLE_CONROLS:")){
                lineNumber++;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().Speed = 5;
            }
            else if (dialogue.Contains(":INPUT NAME:"))
            {
                lineNumber++;
                GameObject.Find("Name").GetComponentInChildren<Animation>().Play("panelSlideIn");
            } else if(dialogue.Contains("[NAME]"))
            {
                Debug.Log("WE GOT A NAME, HERE!");
                string d = dialogue.Replace("[NAME]", PlayerPrefs.GetString("Name"));
                uiText.text = d;
            } else
            {
                uiText.text = dialogue;
            }

			yield return new WaitForSeconds (aTime);
			StartCoroutine (FadeTo (1, 1f, false));
		}
	}

    public void SaveName()
    {
        GameObject.Find("Name").GetComponent<Animation>().Play("panelSlideOut");
        PlayerPrefs.SetString("Name", GameObject.Find("Name").GetComponentInChildren<Text>().text);
        name = GameObject.Find("Name").GetComponentInChildren<Text>().text;
        Debug.Log("Player Name set to: "+name);
        GameObject.Find("Name").SetActive(false);
    }

	IEnumerator hidePanel(){
		//uiText.transform.parent.gameObject.SetActive (false);

		Animation ani = panel.transform.GetComponent<Animation> ();

		ani.Play ("panelSlideOut");
		yield return null;
	}

	IEnumerator showPanel(){
		//uiText.transform.parent.gameObject.SetActive (false);

		Animation ani = panel.transform.GetComponent<Animation> ();

		ani.Play ("panelSlideIn");
		yield return null;
	}
}
