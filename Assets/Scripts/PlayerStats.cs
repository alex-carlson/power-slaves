using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int health = 100;
	public GameObject hurtParticle;
	public TextAsset ouchText;
	public GameObject textBox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy" && !transform.GetComponentInChildren<Movement>().isAttacking) {
			health -= 1;
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
