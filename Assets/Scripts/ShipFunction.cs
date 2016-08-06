using UnityEngine;
using System.Collections;

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

    void FloatMe()
    {

        GameObject go = GameObject.Find("ship");

        go.transform.position += (Vector3.up * 0.5f);

   //   go.GetComponent<BoxCollider2D>().enabled = false;
    }
}
