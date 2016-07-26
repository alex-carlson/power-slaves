using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public TextAsset newGameText;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("LastLevel", -1);
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
