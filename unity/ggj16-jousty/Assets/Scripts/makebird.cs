using UnityEngine;
using System.Collections;

public class makebird : MonoBehaviour {

	public GameObject birdobj;

	public float randomnum =0;

	public float highnum =0;
	public float lownum =0;

	// Use this for initialization
	void Start () {

		randomnum = Random.Range(lownum,highnum);

		StartCoroutine(makeabird());
	
	}

	IEnumerator makeabird()
	{
		yield return new WaitForSeconds(randomnum);

		Instantiate(birdobj,transform.position,Quaternion.identity);

		makeloop();
	}

	public void makeloop()
	{
		randomnum = Random.Range(lownum,highnum);

		StartCoroutine(makeabird());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
