using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {

    public GameObject victoryScreenContainer;
    public GameObject bg;
    public GameObject shield;
    public GameObject spear;
    public GameObject scroll;
    public GameObject text;
    public GameObject flash;

	// Use this for initialization
	void Awake ()
    {
        // disable all
        bg.SetActive(false);
        shield.SetActive(false);
        spear.SetActive(false);
        scroll.SetActive(false);
        text.SetActive(false);
        flash.SetActive(false);

        Game.victoryScreen = this;
    }

    public void StartVictoryAnimation(Color bgColor)
    {
        bg.GetComponent<Image>().color = bgColor;
        StartCoroutine("ShowVictoryAnimations");
    }

    private IEnumerator ShowVictoryAnimations()
    {
        Debug.Log("Point");
        // enable bg
        bg.SetActive(true);
        Debug.Log("Point");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Point");
        // enable shield, wait .5 seconds
        shield.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        // enable spear, wait for anim to finish
        spear.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        // flash and enable scroll
        flash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scroll.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // enable and anim in text
        text.SetActive(true);
        yield return new WaitForSeconds(5f);
    }
}
