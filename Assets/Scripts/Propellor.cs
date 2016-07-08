using UnityEngine;
using System.Collections;

public class Propellor : MonoBehaviour {

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (transform.right*0.3f, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D col){
		Destroy (this.gameObject, 0.2f);
	}
}
