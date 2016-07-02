using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	GameObject plr;
	float dist;

	// Use this for initialization
	void Start () {
		plr = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (transform.position, plr.transform.position);

		if (dist < 30f) {
			transform.Translate (Vector3.MoveTowards(transform.position, plr.transform.position, 0.0001f));
		}
	}
}
