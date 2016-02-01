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
	public float timer;
	public bool gameOver;

	// Use this for initialization
	void Awake () {
		Game.manager = this;
		game_started = true;
		GameStart();
		gameOver = false;
		timer = 5;	
	}
	
	void GameStart() {
	
		pausingPlayer = -1;
	
		// Start music if not already started
	
		SpawnPlayers();

        //CreateUI(); // unneeded for now

        Debug.Log("Game Started");
	
	
	
	}
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				SceneManager.LoadScene(0);
			}
		}
	}

    private void CreateUI()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            Debug.Log(i);
            Game.ui.RegisterPlayer(i); // Note: UI is zero-based
        }
    }



    public void CheckPlayersAlive()
    {
        int numPlayersAlive = 0;
        int lastPlayerAlive = -1;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].GetComponent<PlayerBehavior>().IsAlive()) // is the player alive?
            {
                numPlayersAlive++;
                lastPlayerAlive = i;
            }
        }
        if (numPlayersAlive < 2)
        {
            GameOver(lastPlayerAlive+1);
        }
    }

    public void GameOver(int victor)
    {
        gameOver = true;
        if (victor == -1)
        {
            Game.victoryScreen.StartVictoryAnimation(Color.black);
            //Game.messageBox.DisplayMessage("DRAW", 6000f);
        }
        else
        {
            Color bgColorVictory = Color.clear;
            switch(victor)
            {
                case 1:
                    bgColorVictory = new Color(0f, 0f, 1f, 0.25f);
                    break;
                case 2:
                    bgColorVictory = new Color(1f, 0f, 0f, 0.25f);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
            Game.victoryScreen.StartVictoryAnimation(bgColorVictory); // Add player colors here
            //Game.messageBox.DisplayMessage("Player " + playerList[victor].GetComponent<PlayerBehavior>().m_PlayerNumber + " wins!", 1000f);
        }
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
		playerOne.GetComponent<PlayerBehavior>().ChangeColor (Color.blue);
		playerOne.transform.position = startPositions[positionNumbers[0]];
		GameObject playerTwo = Instantiate(JoustySapien);
		playerTwo.GetComponent<PlayerBehavior>().m_PlayerNumber = 2;
		playerTwo.GetComponent<PlayerBehavior>().ChangeColor (Color.red);
		playerTwo.transform.position = startPositions[positionNumbers[1]];
		
		playerList = new List<GameObject>();
		playerList.Add(playerOne);
		playerList.Add(playerTwo);
	}
}
