using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	public GameObject player;
	Camera m_Cam;

	void Start(){
		m_Cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = m_Cam.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, m_Cam.nearClipPlane));

		if (player.transform.position.x > p.x + 64) {
			m_Cam.transform.position += (Vector3.right * 128);
		} else if (player.transform.position.x < p.x - 64) {
			m_Cam.transform.position += (-Vector3.right * 128);
		} else if (player.transform.position.y > p.y + 64) {
			m_Cam.transform.position += (Vector3.up * 128);
		} else if (player.transform.position.y < p.y - 64) {
			m_Cam.transform.position += (-Vector3.up * 128);
		}
	}
}
