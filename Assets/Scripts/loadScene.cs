using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

	public string levelToLoad;
	public string entrance;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			//SceneManager.LoadScene (levelToLoad);
			//LevelManager.loadLevel(levelToLoad, entrance);
			FadeManager.Instance.LoadLevel (levelToLoad, 2.0f);
		}
	}
}
