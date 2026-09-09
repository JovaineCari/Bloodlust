using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score;
    public Text Mytext;
    public static ScoreCounter Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Mytext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Mytext.text = "" + score;
    }
}
