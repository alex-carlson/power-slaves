﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int Speed = 5;
	public GameObject atkParticle;
	public GameObject hurtParticle;
	Direction playerDir = Direction.S;
	float x;
	float y;
	public GameObject objectiveMenu;
	float quitTime = 0;

	Sprite[] playerSprites;
	Texture2D playerSpriteSheet;

	public bool isAttacking = false;
	bool isBlocking = false;

	SpriteRenderer m_Sprite;
	ParticleSystem m_particle;
	Animator m_anim;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent<SpriteRenderer> ();
		playerSprites = Resources.LoadAll<Sprite> ("playerAngles");
		m_particle = transform.parent.GetComponent<ParticleSystem> ();
		m_anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		bool atk = Input.GetButtonDown ("Fire1");
		bool block = Input.GetButtonDown ("Fire2");

		m_anim.SetFloat ("x", x);
		m_anim.SetFloat ("y", y);
	
		if(atk && isAttacking == false) {
			StartCoroutine ("Attack");
		}

		if(block && isBlocking == false){
			StartCoroutine("Block");
		}

		if (Input.GetKey (KeyCode.Q)) {
			objectiveMenu.SetActive (true);
		} else {
			objectiveMenu.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Escape)) {
			quitTime += Time.deltaTime;

			if(quitTime > 1.5f){
				Application.Quit();
			}
		}

		if(Input.GetKeyUp(KeyCode.Escape)){
			quitTime = 0;
		}

		if (y == 0 && x == 0) {
			return;
		} else if (y > 0 && x == 0) {
			playerDir = Direction.N;
		} else if (y > 0 && x > 0) {
			playerDir = Direction.NE;
		} else if (y == 0 && x > 0) {
			playerDir = Direction.E;
		} else if (y < 0 && x > 0) {
			playerDir = Direction.SE;
		} else if (y < 0 && x == 0) {
			playerDir = Direction.S;
		} else if (y < 0 && x < 0) {
			playerDir = Direction.SW;
		} else if (y == 0 && x < 0) {
			playerDir = Direction.W;
		} else if (y > 0 && x < 0) {
			playerDir = Direction.NW;
		}

		SpriteSwap ();
	}

	void FixedUpdate(){
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		Move (x, y);
	}

	void Move(float x, float y){

		transform.parent.transform.Translate (new Vector3 (x * Speed, y * Speed, 0));
		m_particle.startSize = new Vector3 (x, y, 0).magnitude;
	}

	void SpriteSwap(){
		if (playerDir == Direction.N) {
			m_Sprite.sprite = playerSprites[1];
		} else if (playerDir == Direction.NE) {
			m_Sprite.sprite = playerSprites[2];
		} else if (playerDir == Direction.E) {
			m_Sprite.sprite = playerSprites[0];
		} else if (playerDir == Direction.SE) {
			m_Sprite.sprite = playerSprites[5];
		} else if (playerDir == Direction.S) {
			m_Sprite.sprite = playerSprites[4];
		} else if (playerDir == Direction.SW) {
			m_Sprite.sprite = playerSprites[6];
		} else if (playerDir == Direction.W) {
			m_Sprite.sprite = playerSprites[7];
		} else {
			m_Sprite.sprite = playerSprites[3];
		}
	}

	IEnumerator Attack(){
		isAttacking = true;
		ExtensionMethods.m_Dir (playerDir);
		Instantiate (atkParticle, transform.position+(ExtensionMethods.m_playerDir * 20), Quaternion.identity);
		yield return new WaitForSeconds (0.1f);
		isAttacking = false;
	}

	IEnumerator Block(){
		isBlocking = true;
		yield return new WaitForSeconds (0.2f);
		isBlocking = false;
	}

	IEnumerator Blink(){
		SpriteRenderer sp = GetComponent<SpriteRenderer> ();
		sp.enabled = false;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = true;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = false;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = true;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = false;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = true;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = false;
		yield return new WaitForSeconds (0.18f);
		sp.enabled = true;
	}

	float knockback(){
		return Random.Range (0.05f, 0.5f);
	}
}
