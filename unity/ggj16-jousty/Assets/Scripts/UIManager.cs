using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public GameObject scoreboxPrefab;
    private List<GameObject> playerList;
    public Canvas canvas;
    public Vector2[] scoreboxPositions; // list of possible places for the UI

    void Awake ()
    {
        Game.ui = this;
        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            Game.canvas = canvasObj.GetComponent<Canvas>();
        }
        else
        {
            Debug.Log("UIManager ERROR - canvas not found");
            // spawn canvas if it is not already created?
        }
        playerList = new List<GameObject>();
    }
    
	void Start () {
        
	}

    public void SetupPlayerList(List<GameObject> list)
    {
        //playerList = list;
    }

    public void RegisterPlayer(int id, string playerName, Color color) // Adds a player to the UI; note that this is zero-based
    {
        Debug.Log("Step 1");
        GameObject scoreboxObj = Instantiate<GameObject>(scoreboxPrefab);
        scoreboxObj.transform.SetParent(Game.canvas.transform);
        Debug.Log("Step 2");
        playerList.Add(scoreboxObj);
        Debug.Log("Step 3");
        Scorebox scorebox = scoreboxObj.GetComponent<Scorebox>();
        Debug.Log("Step 4");
        scorebox.SetName(playerName);
        scorebox.SetScore(0);
        scorebox.SetColor(color);
        if(id < scoreboxPositions.Length)
        {
            scoreboxObj.GetComponent<RectTransform>().anchoredPosition = scoreboxPositions[id];
        }
        else // This should not happen - this just prevents the game from crashing
        {
            scoreboxObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            Debug.Log("UIManager ERROR - too many players; please add positions to UIManager.scoreboxPositions");
        }
        
    }

    public void RegisterPlayer(int id, string playerName)
    {
        RegisterPlayer(id, playerName, Color.gray);
    }

    public void RegisterPlayer(int id, Color color)
    {
        RegisterPlayer(id, "Player " + id, color);
    }

    public void RegisterPlayer(int id)
    {
        RegisterPlayer(id, "Player " + id, Color.gray);
    }

    private Scorebox GetPlayerScorebox(int id)
    {
        return playerList[id].GetComponent<Scorebox>();
    }

    public void SetPlayerScore(int id, int score)
    {
        GetPlayerScorebox(id).SetScore(score);
    }

    public void SetPlayerName(int id, string name)
    {
        GetPlayerScorebox(id).SetName(name);
    }

    public void SetPlayerColor(int id, Color color)
    {
        GetPlayerScorebox(id).SetColor(color);
    }
}
