using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	public GameObject player;
	//Camera m_Cam;
	private Vector3 velocity = Vector3.zero;

	void Start(){
        //m_Cam = GetComponent<Camera> ();
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		Vector3 dir = player.transform.position - transform.position;

		transform.position = Vector3.SmoothDamp (transform.position + (dir / 8) + (Vector3.back * 20), player.transform.position + (Vector3.back * 20), ref velocity, 2f);
	}
}
