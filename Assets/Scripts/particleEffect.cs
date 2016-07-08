using UnityEngine;
using System.Collections;

public class particleEffect : MonoBehaviour {

	public float lifetime = 1;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
