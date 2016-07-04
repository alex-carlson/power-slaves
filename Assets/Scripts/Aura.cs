using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

	public int auraLevel = 0;
	public Sprite[] aura;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){
			auraLevel--;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		} else if (Input.GetKeyDown(KeyCode.P)){
			auraLevel++;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		}
	}

	public void gainPower(){
		auraLevel++;
		GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
	}

	public void losePower(){
		auraLevel--;
		GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
	}
}
