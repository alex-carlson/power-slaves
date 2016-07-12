﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int health = 100;
	public GameObject hurtParticle;
	public TextAsset ouchText;
	public GameObject textBox;

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();

		// debug all powerups code
//		PlayerPrefs.SetInt ("DashBoost", 1);
//		PlayerPrefs.SetInt ("TripleShot", 1);
//		PlayerPrefs.SetInt ("SpeedBoost", 1);
//		PlayerPrefs.SetInt ("Armor", 1);

		if (PlayerPrefs.GetInt ("Armor") != 0) {
			health = health + 100;
		}

		GameObject.Find ("Health").GetComponent<Text> ().text = health + "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy" && !transform.GetComponentInChildren<Movement>().isAttacking) {
			health -= 25;
			GameObject.Find ("Health").GetComponent<Text> ().text = health + "";
			transform.GetComponentInChildren<Movement>().StartCoroutine("Blink");
			GameObject clone = (GameObject) Instantiate (hurtParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
			Destroy (clone, 0.2f);


			// use this to trigger dialogue!
			//textBox.GetComponent<Dialogue> ().TriggerDialogue (ouchText, true);

			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die(){
		Destroy (this.gameObject);
	}
}
