using UnityEngine;
using System.Collections;

public class bloodbehavoir : MonoBehaviour {


	void OnEnable()
	{
		StartCoroutine(lifeanddeath());

	}

	IEnumerator lifeanddeath()
	{
		yield return new WaitForSeconds(3f);

		Destroy(transform.gameObject);

	}

}
