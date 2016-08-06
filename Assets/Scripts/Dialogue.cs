using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour {

    public TextAsset newGameText;
	public TextAsset textFile;
    public TextAsset endText;
	string[] dialogueLines;
	int lineNumber = -1;
    [HideInInspector]
    public string name = "Maverick";
    private float skipTimer = 0f;
    private float skipTime = 1.5f;

	public Text uiText;
	Canvas canvas;
	GameObject panel;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(this.gameObject);
		}

		panel = uiText.transform.parent.gameObject;

        if (PlayerPrefs.GetInt("LastLevel") == -1)
        {
            TriggerDialogue(newGameText);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().isActive = false;
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (lineNumber < 0) {
			lineNumber = -1;
		}

        if (Input.GetKey(KeyCode.Return))
        {
            skipTimer += 0.01f;
        } else
        {
            skipTimer = 0;
        }

		if (Input.GetKeyDown (KeyCode.Return)) {
			Next ();
		}

        if(skipTimer > skipTime)
        {
            uiText.text = "";
            StartCoroutine(hidePanel());
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().isActive = true;
            PlayerPrefs.SetString("Name", "Mav");
            skipTimer = 0;
        }
	}

    public void TriggerDialogue(TextAsset txt, bool random = false)
    {

        if (random)
        {
            StopCoroutine(hidePanel());
            StartCoroutine(showPanel());
            textFile = txt;
            dialogueLines = (textFile.text.Split("\n"[0]));
            lineNumber = Mathf.RoundToInt(Random.Range(0, dialogueLines.Length) - 1);
            Next();
            StartCoroutine(hidePanel());
        }
        else
        {
            if (textFile != txt)
            {
                lineNumber = -1;
                textFile = txt;
                dialogueLines = (textFile.text.Split("\n"[0]));
                FadeTo(1, 0.1f, false);
                Next();
                StartCoroutine(showPanel());

            }
            else
            {
                Next();
            }
        }
        uiText.transform.parent.gameObject.SetActive(true);
    }

    public void Next(){
		if (lineNumber < dialogueLines.Length - 1) {
			float stayTime = 0;

			StartCoroutine (FadeTo (0, stayTime, true));
		} else {
			//no more dialogue.  back to bizznizz

			if (uiText.text != "") {
				uiText.text = "";
				StartCoroutine (hidePanel ());
			}
		}
	}

	IEnumerator FadeTo(float aValue, float aTime, bool changeText)
	{
		float alpha = uiText.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			uiText.color = newColor;
			yield return null;
		}

		if (changeText) {
			lineNumber++;
			string dialogue = dialogueLines[lineNumber];

            if (dialogue.Contains(":HOME:"))
            {
                lineNumber++;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().entrance = "";
                if (PlayerPrefs.GetInt("Armor") == 1)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health = 200;
                } else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health = 100;
                }

                GameObject.Find("Health").GetComponent<Text>().color = Color.white;
                GameObject.Find("Health").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health + "";
                FadeManager.Instance.LoadLevel("Home", 1.0f);
                return false;
            } else if (dialogue.Contains(":ENABLE_CONROLS:")) {
                lineNumber++;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().isActive = true;
            }
            else if (dialogue.Contains("[TAKEOFF]"))
            {
                Animation myAnim = GameObject.Find("ship").GetComponent<Animation>();
                myAnim["FlyAway_01"].speed = 1;
                GameObject.Find("ship").GetComponent<Animation>().Play("FlyAway_01");
            }
            else if (dialogue.Contains("[RETURN]"))
            {
                Animation myAnim = GameObject.Find("ship").GetComponent<Animation>();
                myAnim["FlyAway_01"].speed = -1;
                GameObject.Find("ship").GetComponent<Animation>().Play("FlyAway_01");
            }
            else if (dialogue.Contains("[SHORTPAUSE]"))
            {
                Debug.Log("shortpause");
                Animation myAnim = GameObject.Find("ship").GetComponent<Animation>();
                myAnim["Ship_Spin"].speed = 1;
                GameObject.Find("ship").GetComponent<Animation>().Play("Ship_Spin");
            }
            else if (dialogue.Contains("[ENDSEQUENCE]"))
            {
                Invoke("BeamUp", 0);
            }
            else if (dialogue.Contains(":INPUT NAME:"))
            {
                lineNumber++;
                GameObject.Find("Name").GetComponentInChildren<Animation>().Play("NameSlideIn");
                return false;
            }
            else if (dialogue.Contains("[GOTAPPLE]"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().p_dashBoost = true;
                PlayerPrefs.SetInt("DashBoost", 1);
                PlayerPrefs.SetInt("CollectedItems", PlayerPrefs.GetInt("CollectedItems") + 1);
            }
            else if (dialogue.Contains("[GOTFOSSIL]"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().p_tripleShot = true;
                PlayerPrefs.SetInt("TripleShot", 1);
                PlayerPrefs.SetInt("CollectedItems", PlayerPrefs.GetInt("CollectedItems") + 1);
            }
            else if (dialogue.Contains("[GOTBOOK]"))
            {
                Debug.Log("Got a book, setting armor to true");
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().p_armor = true;
                PlayerPrefs.SetInt("Armor", 1);
                PlayerPrefs.SetInt("CollectedItems", PlayerPrefs.GetInt("CollectedItems") + 1);
            }
            else if (dialogue.Contains("[GOTCOMPASS]"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().p_speedBoost = true;
                PlayerPrefs.SetInt("SpeedBoost", 1);
                PlayerPrefs.SetInt("CollectedItems", PlayerPrefs.GetInt("CollectedItems") + 1);
            }
            else if (dialogue.Contains("[ITEMCOUNT]"))
            {
                string d = dialogue.Replace("[ITEMCOUNT]", 4 - PlayerPrefs.GetInt("CollectedItems") + "");

                if (PlayerPrefs.GetInt("CollectedItems") == 4)
                {
                    TriggerDialogue(endText);
                } else
                {
                    uiText.text = d;
                }
            }
            else
            {
                if (dialogue.Contains("[NAME]"))
                {
                    string d = dialogue.Replace("[NAME]", PlayerPrefs.GetString("Name"));
                    uiText.text = d;
                }
					
                if (dialogue.Contains("[NAME]:") || dialogue.Contains("Mav:"))
                {
					string d = dialogue.Replace("[NAME]", PlayerPrefs.GetString("Name"));
					uiText.text = d;
                    uiText.alignment = TextAnchor.UpperLeft;
                } else if (dialogue.Contains("[CENTER]")){
                    string d = dialogue.Replace("[CENTER]", "");
                    uiText.text = d;
                    uiText.alignment = TextAnchor.UpperCenter;
                } else
                {
					string d = dialogue.Replace("[NAME]", PlayerPrefs.GetString("Name"));
					uiText.text = d;
                    uiText.alignment = TextAnchor.UpperRight;
                }
            }

			yield return new WaitForSeconds (aTime);
			StartCoroutine (FadeTo (1, 1f, false));
		}
	}

    void BeamUp()
    {
        GameObject ship = GameObject.Find("ship");
        ship.GetComponent<Animation>().Play("Ship_Center");
    }

    public void SaveName()
    {
        GameObject.Find("Name").GetComponent<Animation>().Play("NameSlideOut");
        PlayerPrefs.SetString("Name", GameObject.Find("Name").GetComponentInChildren<Text>().text);
        name = GameObject.Find("Name").GetComponentInChildren<Text>().text;
        Debug.Log("Player Name set to: "+name);
        GameObject.Find("Name").SetActive(false);
    }

	IEnumerator hidePanel(){
		//uiText.transform.parent.gameObject.SetActive (false);

		Animation ani = panel.transform.GetComponent<Animation> ();

		ani.Play ("panelSlideOut");
		yield return null;
	}

	IEnumerator showPanel(){
		//uiText.transform.parent.gameObject.SetActive (false);

		Animation ani = panel.transform.GetComponent<Animation> ();

		ani.Play ("panelSlideIn");
		yield return null;
	}
}
