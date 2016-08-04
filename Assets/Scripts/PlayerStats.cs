using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	public int health = 100;
	public GameObject hurtParticle;
	public TextAsset ouchText;
	public GameObject textBox;
    public AudioClip deathSound;
	AudioSource m_audio;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		// debug all powerups code
//		PlayerPrefs.SetInt ("DashBoost", 1);
//		PlayerPrefs.SetInt ("TripleShot", 1);
//		PlayerPrefs.SetInt ("SpeedBoost", 1);
//		PlayerPrefs.SetInt ("Armor", 1);

		m_audio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();

		textBox = GameObject.Find ("Panel").transform.GetChild (0).gameObject;

		if (PlayerPrefs.GetInt ("Armor") != 0) {
			health = health + 100;
		}

		GameObject.Find ("Health").GetComponent<Text> ().text = health + "";
	}
	
	// Update is called once per frame
	void Update () {
		m_audio.volume = rb.velocity.magnitude * 0.01f;
	}

	public void GetHealth(){
		health += 25;
		GameObject.Find("Health").GetComponent<Text>().text = health + "";

		if (health > 25) {
			GameObject.Find("Health").GetComponent<Text>().color = Color.white;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Enemy" && col.transform.GetComponent<enemyAI>().isAttacking) {
			health -= 25;
            GameObject.Find("Health").GetComponent<Text>().color = Color.white;
            GameObject.Find ("Health").GetComponent<Text> ().text = health + "";
			transform.GetComponentInChildren<Movement>().StartCoroutine("Blink");
			GameObject clone = (GameObject) Instantiate (hurtParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
			Destroy (clone, 0.2f);

		} else if (col.transform.tag == "EnemyBullet")
        {
            health -= 25;
            GameObject.Find("Health").GetComponent<Text>().color = Color.white;
            GameObject.Find("Health").GetComponent<Text>().text = health + "";
            transform.GetComponentInChildren<Movement>().StartCoroutine("Blink");
            GameObject clone = (GameObject)Instantiate(hurtParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            Destroy(clone, 0.2f);
		}

        // use this to trigger dialogue!
        //textBox.GetComponent<Dialogue> ().TriggerDialogue (ouchText, true);

        if (health <= 25)
        {
            GameObject.Find("Health").GetComponent<Text>().color = Color.red;
            GameObject.Find("Health").GetComponent<Text>().text = 0 + "";
        }

        if (health <= 0)
        {
            Die();
        }
    }

	void Die(){

        // this needs to be fixed, when you go to the menu, the in game canvas is still there. :/


        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().volume = 1;
        GetComponent<AudioSource>().Play();
		GetComponentInChildren<Movement> ().isActive = false;
		LevelManager.GameOver ();
	}
}
