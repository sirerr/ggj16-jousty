using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour {

    public GameObject messageBoxPanel;
    private Text text;
    
	void Awake () {
        Game.messageBox = this;
        text = messageBoxPanel.GetComponent<Text>();
        messageBoxPanel.SetActive(false);
	}
	
	public void DisplayMessage(string message, float seconds)
    {
        messageBoxPanel.SetActive(true);
        text.text = message;
        ShowMessageForSeconds(seconds);
    }

    private IEnumerator ShowMessageForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageBoxPanel.SetActive(false);
    }


}
