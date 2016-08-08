using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

	public string levelToLoad;
	public string entrance;
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
            col.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//SceneManager.LoadScene (levelToLoad);
			//LevelManager.loadLevel(levelToLoad, entrance);
			FadeManager.Instance.LoadLevel (levelToLoad, 1.0f, entrance);
		}
	}
}
