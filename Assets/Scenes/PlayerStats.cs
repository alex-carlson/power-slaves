using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy" && !transform.GetComponentInChildren<Movement>().isAttacking) {
			health -= 25;
			transform.GetComponentInChildren<Movement>().StartCoroutine("Blink");

			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die(){
		Destroy (this.gameObject, 2f);
	}
}
