using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        FadeManager.Instance.LoadLevel("Home", 2.0f);
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
