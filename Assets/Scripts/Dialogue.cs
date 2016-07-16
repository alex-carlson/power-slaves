using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public TextAsset textFile;
	string[] dialogueLines;
	int lineNumber = -1;

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

		if (textFile) {
			//Regex.Match (textFile.text, @"\(([^)]*)\)").Groups [1].Value;

			dialogueLines = (textFile.text.Split("\n"[0]));
			TriggerDialogue (textFile);

		}

		panel = uiText.transform.parent.gameObject;
		StartCoroutine (showPanel());
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

	public void Next(){
		if (lineNumber < dialogueLines.Length - 1) {
			float stayTime = 0;
			StartCoroutine (FadeTo (0, stayTime, true));
		} else {
			//no more dialogue.  back to bizznizz
			//float stayTime = 0;

			if (uiText.text != "") {
				uiText.text = "";
				StartCoroutine (hidePanel ());
			}
		}
	}

	public void TriggerDialogue(TextAsset txt, bool random = false){

		if (random) {
			StopCoroutine (hidePanel ());
			StartCoroutine (showPanel ());
			textFile = txt;
			dialogueLines = (textFile.text.Split ("\n" [0]));
			lineNumber = Mathf.RoundToInt(Random.Range(0, dialogueLines.Length) -1);
			Next ();
			StartCoroutine (hidePanel ());
		} else {
			if (textFile != txt) {
				lineNumber = -1;
				textFile = txt;
				dialogueLines = (textFile.text.Split ("\n" [0]));
				FadeTo (1, 0.1f, false);
				Next ();
				StartCoroutine (showPanel ());

			} else {
				Next ();
			}
		}
		uiText.transform.parent.gameObject.SetActive (true);
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
			uiText.text = dialogue;
			yield return new WaitForSeconds (aTime);
			StartCoroutine (FadeTo (1, 1f, false));
		}
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
