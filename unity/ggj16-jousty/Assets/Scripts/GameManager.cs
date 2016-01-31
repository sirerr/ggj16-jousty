using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private bool game_started = false;
	public GameObject JoustySapien;
	public Vector3[] startPositions;
	private List<GameObject> playerList;
	private int pausingPlayer;
	public bool isPaused = false;

	// Use this for initialization
	void Awake () {
		Game.manager = this;
		game_started = true;
		GameStart();
		Debug.Log("Hello World");
		
		
	
	}
	
	void GameStart() {
	
		pausingPlayer = -1;
	
		// Start music if not already started
	
		// Spawn UI
	
		SpawnPlayers();
		
		Debug.Log("Game Started");
	
	
	
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void Foo() {
	

	}
	
	public void CheckPlayersAlive()
	{
		int numPlayersAlive = 0;
		for (int i = 0; i < playerList.Count; i++)
		{
			if(playerList[i].GetComponent<PlayerBehavior>().IsAlive()) // is the player alive?
			{
				numPlayersAlive++;
			}
		}
		if (numPlayersAlive < 2)
		{
			GameOver();
		}
	}
	
	public void GameOver() {
		
		// Fancy end of level stuff here
		// Announce winner, etc
		// May also immediately restart scene in the future
		SceneManager.LoadScene(0);
	
	}
	
	public void TogglePause(int player)
	{
		Debug.Log("Player that actually paused: " + player);
		if (!isPaused)
		{
			pausingPlayer = player;
			Time.timeScale = 0f;
			isPaused = true;
			Debug.Log("Paused by " + player);
		}
		else if (isPaused && player == pausingPlayer) 
		{
			pausingPlayer = -1;
			Time.timeScale = 1f;
			isPaused = false;
			Debug.Log("Unpaused by " + player);
		}
	}
	
	public void SpawnPlayers()
	{
		int[] positionNumbers = new int[] {0, 1, 2, 3};
	
		// Knuth shuffle algorithm from Wikipedia
		// So no two players spawn in the same place
        for (int t = 0; t < positionNumbers.Length; t++ )
        {
            int temp = positionNumbers[t];
            int r = Random.Range(t, positionNumbers.Length);
            positionNumbers[t] = positionNumbers[r];
            positionNumbers[r] = temp;
        }
	
		GameObject playerOne = Instantiate(JoustySapien);
		playerOne.GetComponent<PlayerBehavior>().m_PlayerNumber = 1;
		playerOne.transform.position = startPositions[positionNumbers[0]];
		GameObject playerTwo = Instantiate(JoustySapien);
		playerTwo.GetComponent<PlayerBehavior>().m_PlayerNumber = 2;
		playerTwo.transform.position = startPositions[positionNumbers[1]];
		
		playerList = new List<GameObject>();
		playerList.Add(playerOne);
		playerList.Add(playerTwo);
	}
}
