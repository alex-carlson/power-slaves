using UnityEngine;
using System.Collections;

public class Propellor : MonoBehaviour {

	Rigidbody2D rb;
	Vector3 target;
	Vector3 dir;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform.position;
		Destroy (this.gameObject, 1.5f);
		dir = transform.position - target;
		rb.AddForce (-dir * 0.55f, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (-dir * 2.25f, ForceMode2D.Force);
	}

	void OnCollisionEnter2D(Collision2D col){
		Destroy (this.gameObject);
	}
}
