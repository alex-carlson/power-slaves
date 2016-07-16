using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

    public float spawnRate = 1;
    public bool isSpawning = false;
    public GameObject enemy;
    public int maxEnemies = 5;
    int health = 100;

	// Update is called once per frame
	void Update () {
	    if(isSpawning == true)
        {
            isSpawning = false;
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            InvokeRepeating("SpawnEnemies", 0, spawnRate);
        }
	}

    void SpawnEnemies()
    {
        if(transform.childCount > maxEnemies -1)
            return;

        GameObject clone = (Instantiate(enemy, transform.position, Quaternion.identity)) as GameObject;
        clone.transform.parent = this.gameObject.transform;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Bullet")
        {
            health -= 20;

            if(health <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (Transform child in transform)
                {
                    child.transform.parent = null;
                }
                GameObject.Find("Pickup").GetComponent<itemPickup>().checkToSpawn();
                transform.gameObject.SetActive(false);
            }
        }
    }
}
