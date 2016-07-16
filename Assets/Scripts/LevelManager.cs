using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("LastLevel", SceneManager.GetActiveScene ().buildIndex);
		DontDestroyOnLoad(transform.gameObject);
		Instantiate (player, Vector3.zero, Quaternion.identity);
	}

	public void NewGame(){
		PlayerPrefs.DeleteAll ();
		FadeManager.Instance.LoadLevel ("Home", 2.0f);
	}

	public void Load(){
		FadeManager.Instance.LoadLevel ("Home", 2.0f);
	}

	public void Quit(){
		Application.Quit ();
	}

	public static void loadLevel(string levelName, string e = ""){
		if (e != "") {
			//set the position for the player, then load the level
			GameObject plr = GameObject.FindGameObjectWithTag("Player");
			plr.GetComponentInChildren<Movement> ().entrance = e;
			SceneManager.LoadScene (levelName);
		} else {
			SceneManager.LoadScene (levelName);
		}
	}
}
