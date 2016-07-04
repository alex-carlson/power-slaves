using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	[Range(0, 1)] public float Speed = 0.1f;
	public int health = 100;

	enum Direction {North, South, East, West, None};

	Direction myDirection;

	GameObject plr;
	float dist;
	bool inView = true;

	// Use this for initialization
	void Start () {
		plr = GameObject.FindGameObjectWithTag ("Player");
		InvokeRepeating ("Brain", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (transform.position, plr.transform.position);

		if (dist < 10 && inView == true) {
			//chase
			transform.position = Vector3.MoveTowards(transform.position, plr.transform.position, Speed  * Time.deltaTime);

		} else {
			if (myDirection == Direction.North) {
				transform.Translate (Vector3.up * Speed);
			} else if (myDirection == Direction.East) {
				transform.localRotation = Quaternion.Euler(0, 0, 0);
				transform.Translate (Vector3.right * Speed);
			} else if (myDirection == Direction.South) {
				transform.Translate (-Vector3.up * Speed);
			} else if(myDirection == Direction.West){
				transform.Translate (Vector3.right * Speed);
				transform.localRotation = Quaternion.Euler(0, 180, 0);
			}
		}

		if (health <= 0) {
			Die ();
		}
	}

	void Brain(){
		myDirection = (Direction)Random.Range (0, 4);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag != "Player" && col.transform.tag != "Bullet") {
			myDirection = Direction.None;
			Invoke ("Brain", 0);
		} else if (col.transform.tag == "Bullet"){
			health -= 25;
		}
	}

	void Die(){
		Destroy (this.gameObject);
	}
}
