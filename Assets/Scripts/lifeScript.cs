using UnityEngine;
using System.Collections;

public class lifeScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			col.transform.GetComponent<PlayerStats> ().GetHealth ();
			Destroy (this.gameObject);
		}
	}
}
