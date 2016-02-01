using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scorebox : MonoBehaviour {

    public Text nameText;
    public Text scoreText;
    private Color bgcolor;
    private Image bg;

    void Awake()
    {
        bg = GetComponent<Image>();
        //RectTransform t = GetComponent<RectTransform>().parent
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetColor(Color color)
    {
        bg.color = color;
    }

    
}
