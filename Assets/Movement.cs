using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int Speed = 1;
	public Sprite atkSprite;
	public Sprite blkSprite;

	bool isAttacking = false;
	bool isBlocking = false;

	SpriteRenderer m_Sprite;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		bool atk = Input.GetButtonDown ("Fire1");
		bool block = Input.GetButtonDown ("Fire2");

		Move (x, y);
	
		if(atk && isAttacking == false) {
			StartCoroutine ("Attack");
		}

		if(block && isBlocking == false){
			StartCoroutine("Block");
		}
	}

	void Move(float x, float y){
		if (x < 0) {
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		} else {
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		transform.parent.transform.Translate (new Vector3 (x, y, 0));
	}

	IEnumerator Attack(){
		isAttacking = true;
		Sprite tempSprite = m_Sprite.sprite;
		m_Sprite.sprite = atkSprite;
		yield return new WaitForSeconds (0.2f);
		m_Sprite.sprite = tempSprite;
		isAttacking = false;
	}

	IEnumerator Block(){
		isBlocking = true;
		Sprite tempSprite = m_Sprite.sprite;
		m_Sprite.sprite = blkSprite;
		yield return new WaitForSeconds (0.2f);
		m_Sprite.sprite = tempSprite;
		isBlocking = false;
	}
}
