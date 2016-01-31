using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Body")) {
			//other.gameObject.transform.parent.gameObject.SetActive (false);
			other.gameObject.transform.parent.gameObject.GetComponent<PlayerBehavior>().Damage(1);
		}
		
	}
}
