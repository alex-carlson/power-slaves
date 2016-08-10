using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public int Speed = 60;
    public int maxSpeed = 220;
	public GameObject atkParticle;
	public GameObject hurtParticle;
	Direction playerDir = Direction.S;
	float x;
	float y;
	public GameObject objectiveMenu;
	float quitTime = 0;
	Rigidbody2D rb;
	[HideInInspector] public string entrance;
    public LayerMask layerMask;
    public bool isActive = true;

	public bool p_dashBoost = false;
	public bool p_tripleShot = false;
	public bool p_speedBoost = false;
	public bool p_armor = false;

	Sprite[] playerSprites;
	Texture2D playerSpriteSheet;

	public bool isAttacking = false;
	bool isBlocking = false;

	SpriteRenderer m_Sprite;
	ParticleSystem m_particle;
    ParticleSystem m_footsteps;
	Animator m_anim;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.transform.parent.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(transform.root.gameObject);
		}

		m_Sprite = GetComponent<SpriteRenderer> ();
		playerSprites = Resources.LoadAll<Sprite> ("playerAngles");
        m_particle = transform.parent.GetComponent<ParticleSystem>();
        m_footsteps = transform.GetComponentInChildren<ParticleSystem>();
        m_anim = GetComponent<Animator> ();
		objectiveMenu = GameObject.Find ("Objectives");

		p_dashBoost = Convert.ToBoolean(PlayerPrefs.GetInt ("DashBoost"));
		p_tripleShot = Convert.ToBoolean(PlayerPrefs.GetInt ("TripleShot"));
		p_speedBoost = Convert.ToBoolean(PlayerPrefs.GetInt ("SpeedBoost"));
		p_armor = Convert.ToBoolean(PlayerPrefs.GetInt ("Armor"));
		rb = transform.parent.GetComponent<Rigidbody2D> ();

        transform.position = Vector3.zero;
		transform.parent.position = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		bool atk = Input.GetButtonDown ("Fire1");
		bool block = Input.GetButtonDown ("Fire2");

        if (isActive == true)
        {
            m_anim.SetFloat("x", x);
            m_anim.SetFloat("y", y);
        }

		if(atk && isAttacking == false) {
            if (isActive == true)
                StartCoroutine ("Attack");
		}

		if(block && isBlocking == false){
            if (isActive == true)
                StartCoroutine("Block");
		}

		if (Input.GetKey (KeyCode.Q)) {
			objectiveMenu.SetActive (true);
            if (p_dashBoost)
            {
                objectiveMenu.transform.GetChild(0).GetComponent<Text>().color = Color.green;
            }
			if (p_tripleShot) {
				objectiveMenu.transform.GetChild (1).GetComponent<Text> ().color = Color.green;
			}
			if (p_armor) {
				objectiveMenu.transform.GetChild (2).GetComponent<Text> ().color = Color.green;
			}
			if (p_speedBoost) {
				objectiveMenu.transform.GetChild (3).GetComponent<Text> ().color = Color.green;
			}
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

        if (isActive == true)
        {
            if (y == 0 && x == 0)
            {
                return;
            }
            else if (y > 0 && x == 0)
            {
                playerDir = Direction.N;
            }
            else if (y > 0 && x > 0)
            {
                playerDir = Direction.NE;
            }
            else if (y == 0 && x > 0)
            {
                playerDir = Direction.E;
            }
            else if (y < 0 && x > 0)
            {
                playerDir = Direction.SE;
            }
            else if (y < 0 && x == 0)
            {
                playerDir = Direction.S;
            }
            else if (y < 0 && x < 0)
            {
                playerDir = Direction.SW;
            }
            else if (y == 0 && x < 0)
            {
                playerDir = Direction.W;
            }
            else if (y > 0 && x < 0)
            {
                playerDir = Direction.NW;
            }
        }


        if(isActive == true)
        {
            SpriteSwap();
        }
	}

	void FixedUpdate(){
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
        if (isActive == true)
            Move (x, y);
    }

	void OnLevelWasLoaded(){
        isActive = true;
        if (entrance != "") {
			transform.parent.transform.position = GameObject.Find (entrance).transform.GetChild(0).transform.position;
		} else
        {
            transform.parent.transform.position = Vector3.zero;
        }
	}

	void Move(float x, float y){

		if (p_speedBoost) {
			x *= 1.6f;
			y *= 1.6f;
		}

		//transform.parent.transform.Translate (new Vector3 (x * Speed, y * Speed, 0));

        if(rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector3(x * Speed, y * Speed, x * Speed), ForceMode2D.Impulse);
            m_particle.startSize = new Vector3(x, y, 0).magnitude;
            m_footsteps.emissionRate = rb.velocity.magnitude * 0.05f;
        }
	}

	void SpriteSwap(){

	}

	IEnumerator Attack(){
		isAttacking = true;
		ExtensionMethods.m_Dir (playerDir);
		Instantiate (atkParticle, transform.position+(ExtensionMethods.m_playerDir * 20), Quaternion.identity);

		if(p_tripleShot){
			Vector3 v = ExtensionMethods.m_playerDir;
			Vector3 rgt = Vector3.Cross (v, Vector3.forward);
			Vector3 lft = -rgt;

			Instantiate (atkParticle, transform.position + (ExtensionMethods.m_playerDir * 15) + rgt * 10, Quaternion.Euler(ExtensionMethods.m_playerDir));
			Instantiate (atkParticle, transform.position + (ExtensionMethods.m_playerDir * 15) + lft * 10, Quaternion.Euler(ExtensionMethods.m_playerDir));
		}

		yield return new WaitForSeconds (0.1f);
		isAttacking = false;
	}

    IEnumerator Block() {
        isBlocking = true;

        GetComponent<AudioSource>().Play();
        rb.AddForce(new Vector3(x * (Speed * 60), y * (Speed * 60), 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        if (!p_dashBoost)
        {
            yield return new WaitForSeconds(0.4f);
        }

        isBlocking = false;
    }

	IEnumerator Blink(){
		GetComponentInParent<PlayerStats> ().wasHit = true;
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
		GetComponentInParent<PlayerStats> ().wasHit = false;
	}

	float knockback(){
		return UnityEngine.Random.Range (0.05f, 0.5f);
	}
}
