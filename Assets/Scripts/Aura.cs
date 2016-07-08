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
		if(Input.GetKeyDown(KeyCode.O) && auraLevel > 0){
			auraLevel--;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		} else if (Input.GetKeyDown(KeyCode.P) && auraLevel < aura.Length-1){
			auraLevel++;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		}
	}

	public void gainPower(){
		if (auraLevel < aura.Length - 1) {
			auraLevel++;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		}
	}

	public void losePower(){
		if (auraLevel > 0) {
			auraLevel--;
			GetComponent<SpriteRenderer> ().sprite = aura [auraLevel];
		}
	}
}
