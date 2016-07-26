using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("LastLevel", SceneManager.GetActiveScene ().buildIndex);
		DontDestroyOnLoad(transform.gameObject);
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

	public static void loadLevel(string e = ""){
		if (e != "") {
            GameObject plr = GameObject.FindGameObjectWithTag("Player");
			plr.GetComponentInChildren<Movement> ().entrance = e;
		}
	}
}
