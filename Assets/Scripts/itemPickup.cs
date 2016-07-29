using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemPickup : MonoBehaviour {

    public GameObject[] spawners;
    int spawnersDown = 0;
    bool canPickup = false;
    public enum Powerups {DashBoost, TripleShot, SpeedBoost, Armor};
    public Powerups Power;

    public TextAsset textFile;

    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().enabled = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player" && canPickup == true)
        {
            PlayerPrefs.SetInt(Power.ToString(), 1);
            GameObject.Find("Canvas").GetComponent<Dialogue>().TriggerDialogue(textFile);
            Destroy(this.gameObject);
        }
    }


    public void checkToSpawn()
    {
        spawnersDown++;
        if(spawnersDown == spawners.Length)
        {

            GetComponent<SpriteRenderer>().enabled = true;
            canPickup = true;
        }
    }
}
