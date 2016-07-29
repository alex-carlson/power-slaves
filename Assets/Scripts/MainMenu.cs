using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public TextAsset newGameText;
    bool isPlaying = false;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (Input.anyKeyDown && isPlaying == false)
        {
            isPlaying = true;
            NewGame();
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("LastLevel", -1);
        FadeManager.Instance.LoadLevel("Home", 2.0f);
        GetComponent<AudioSource>().Play();
    }

    public void Load()
    {
        FadeManager.Instance.LoadLevel("Home", 2.0f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
