using UnityEngine;
using System.Collections;

public enum MovementType { Floating, Shaking, Spinning, SpinFloat };

public class passiveMovement : MonoBehaviour {

	public MovementType myMotion;
    public float intensity = 2f;

    // Use this for initialization
    void Update () {
		if (myMotion == MovementType.Floating) {
			Floating ();
		} if (myMotion == MovementType.SpinFloat){
			FloatingAndSpinning();
		} if (myMotion == MovementType.Shaking) {
			Shaking ();
		}
	}

	void Floating(){
		transform.Translate (new Vector3(0, Mathf.Sin(intensity * Mathf.PI*Time.time) - Mathf.Sin(intensity * Mathf.PI*(Time.time - Time.deltaTime)), 0));
	}

	void FloatingAndSpinning(){
		transform.Translate (new Vector3(0, Mathf.Sin(2*Mathf.PI*Time.time) - Mathf.Sin(2*Mathf.PI*(Time.time - Time.deltaTime)), 0));
		transform.localRotation = Quaternion.Euler (new Vector3(0, 0, Time.time * Mathf.PI));
	}

	void Shaking(){
		transform.Translate (new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0));
	}
}
