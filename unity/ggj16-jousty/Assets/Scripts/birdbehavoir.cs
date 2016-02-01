using UnityEngine;
using System.Collections;

public class birdbehavoir : MonoBehaviour {

	public float birdspeed =0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.back * birdspeed * Time.deltaTime);
	
	}
}
