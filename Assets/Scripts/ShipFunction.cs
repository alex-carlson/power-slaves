using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShipFunction : MonoBehaviour {

    bool isFloating = false;

    public void FloatAway()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.parent = this.transform;
        player.SetActive(false);

        isFloating = true;
    }

    public void FixedUpdate()
    {
        if (!isFloating)
            return;
        else
        {
            FloatMe();
        }
    }

    void Update()
    {
        if(transform.position.y > 2800)
        {
            FadeManager.Instance.LoadLevel("Menu", 2.0f);
        }
    }

    void FloatMe()
    {

        GameObject go = GameObject.Find("ship");

        if(transform.position.y < 450)
        {
            go.transform.position += (Vector3.up * 1.5f);
        } else
        {
            go.transform.position += (Vector3.up * 0.5f);
        }

   //   go.GetComponent<BoxCollider2D>().enabled = false;
    }
}
