using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.transform.GetComponentInChildren<Aura> ().gainPower ();
		} else {
			other.transform.GetComponentInChildren<Aura> ().losePower ();
		}
	}
}
