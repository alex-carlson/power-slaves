using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public TextAsset textFile;
	string[] dialogueLines;
	public string[] commands;
	int lineNumber = -1;

	public Text uiText;
	Canvas canvas;


	// Use this for initialization
	void Start () {
		if (textFile) {
			//Regex.Match (textFile.text, @"\(([^)]*)\)").Groups [1].Value;

			dialogueLines = (textFile.text.Split("\n"[0]));
			TriggerDialogue (textFile);

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

	void Empty(){

	}

	public void Next(){
		if (lineNumber < dialogueLines.Length - 1) {
			float stayTime = 0;
			StartCoroutine (FadeTo (0, stayTime, true));
		} else {
			lineNumber = -1;
			float stayTime = 0;
			StartCoroutine (FadeTo (0, stayTime, true));
		}
	}

	public void TriggerDialogue(TextAsset txt){
		if (textFile != txt) {
			lineNumber = -1;
			textFile = txt;
			dialogueLines = (textFile.text.Split ("\n" [0]));
			FadeTo (1, 0.1f, false);

		} else {
			Next ();
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
			uiText.text = dialogue;
			yield return new WaitForSeconds (aTime);
			StartCoroutine (FadeTo (1, 1f, false));
		}
	}
}
