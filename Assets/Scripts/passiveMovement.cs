using UnityEngine;
using System.Collections;

public enum MovementType { Floating, Shaking, Spinning };

public class passiveMovement : MonoBehaviour {

	public MovementType myMotion;

	// Use this for initialization
	void Update () {
		if (myMotion == MovementType.Floating) {
			Floating ();
		}
	}

	void Floating(){
		transform.Translate (new Vector3(0, Mathf.Sin(2*Mathf.PI*Time.time) - Mathf.Sin(2*Mathf.PI*(Time.time - Time.deltaTime)), 0));
	}
}
