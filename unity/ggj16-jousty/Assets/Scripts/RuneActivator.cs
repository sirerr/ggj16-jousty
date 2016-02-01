using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RuneActivator : MonoBehaviour {
	Dictionary<string, int> dictionary = new Dictionary<string, int>();
	Dictionary<string, string> ritualdictionary = new Dictionary<string, string>();
	private Queue<int> queue;

	


	// Use this for initialization
	void Start () {
		dictionary.Add("MichaelSmellsLikeCylinder", 5);
		dictionary.Add("PaulEatsPoop", 1);
		dictionary.Add("StephenLikesNickelback", 2);
		dictionary.Add("LeonardWalksBackwards", 3);
		dictionary.Add("CDUsesCheatCodes", 4);
		
		ritualdictionary.Add("1234", "TestRitual");
		ritualdictionary.Add("12", "Speed");
		ritualdictionary.Add("34", "Speed");
		ritualdictionary.Add("13", "Fire");
		ritualdictionary.Add("24", "Fire");
		
		
		queue = new Queue<int>();
		
		queue.Enqueue(0);
		queue.Enqueue(0);
		//queue.Enqueue(0);
		//queue.Enqueue(0);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
	void OnCollisionEnter(Collision collision){
	
		//Debug.Log("test");
		if( dictionary.ContainsKey(collision.gameObject.name) && queue.ToArray()[1] != dictionary[collision.gameObject.name]){
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
				switch (ritualstring) {
					case "TestRitual":
						Debug.Log("TestRitual");
						break;
					case "Speed":
						gameObject.GetComponent<PlayerBehavior> ().SpeedBoost ();
						break;
					case "Fire":
						gameObject.GetComponent<PlayerBehavior> ().FireSpear ();
						break;
					default:
						Debug.Log("Should Never Happen");
						break;
				}

				//Game.manager.Foo();
			
			
			}
			
		}

		
		
	
	
	}
	
}
