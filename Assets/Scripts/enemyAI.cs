﻿using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class enemyAI : MonoBehaviour {

	public Transform target;
	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	public Path path;
	[Range(300, 1000)] public float speed = 700f;
	public ForceMode2D fMode;
	public int health = 100;
	[HideInInspector] public bool pathIsEnded = false;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;
	private float playerDist = 300;

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			return;
		}
			
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath());
	}

	IEnumerator UpdatePath(){
		if (target == null) {
			return false;
		}

		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	
	}

	public void OnPathComplete(Path p){
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate(){
		if (target == null)
			return;

		if (playerDist > 200)
			return;
		

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) 
				return;

			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;

		dir *= speed * Time.fixedDeltaTime;

		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);

		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}


	}
		
	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			Die ();
			return;
		}

		playerDist = Vector3.Distance (transform.position, target.transform.position);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Bullet") {
			health -= 25;
			Vector3 dir = (transform.position - col.transform.position);
			rb.AddForce (dir.normalized * 10, ForceMode2D.Impulse);
			Destroy (col.transform.gameObject, 0.2f);
		} else if (col.transform.tag == "Player") {
			Vector3 dir = (transform.position - col.transform.position);
			col.transform.GetComponent<Rigidbody2D> ().AddForce (-dir.normalized * 100, ForceMode2D.Impulse);
		}
	}

	void Die(){
		Destroy (this.gameObject);
	}
}
