using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RuneActivator : MonoBehaviour {
	Dictionary<string, int> dictionary = new Dictionary<string, int>();
	Dictionary<string, string> ritualdictionary = new Dictionary<string, string>();
	private Queue<int> queue;

	


	// Use this for initialization
	void Start () {
		dictionary.Add("MichaelSmellsLikeCylinder", 1);
		dictionary.Add("Pillar2", 2);
		dictionary.Add("Pillar3", 3);
		dictionary.Add("Pillar4", 4);
		
		ritualdictionary.Add("1234", "TestRitual");
		
		
		queue = new Queue<int>();
		
		queue.Enqueue(0);
		queue.Enqueue(0);
		queue.Enqueue(0);
		queue.Enqueue(0);
	
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
	void OnCollisionEnter(Collision collision){
	
		//Debug.Log("test");
		if( dictionary.ContainsKey(collision.gameObject.name)){
			int value = dictionary[collision.gameObject.name];
			Debug.Log("Hit" + value);
			
			queue.Enqueue(value);
			queue.Dequeue();
			
			string queuestring = "";
			foreach ( int obj in queue)
				queuestring += obj;
			
			Debug.Log(queuestring);
			
			if( ritualdictionary.ContainsKey(queuestring)){
				string ritualstring = ritualdictionary[queuestring];
				Debug.Log("YAY");
				Game.manager.Foo();
			
			
			}
			
		}

		
		
	
	
	}
	
}
