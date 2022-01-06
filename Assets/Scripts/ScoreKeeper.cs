using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance; 
    private int playerScore = 0;
    private int aiScore = 0;

    public TextMeshPro textMeshPro; 


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playerHit()
    {
        playerScore++;
    }

    public void aiHit()
    {
        aiScore++;
    }



    public void clearScores()
    {
        playerScore = 0;
        aiScore = 0;
    }


    public int getPlayerScore()
    {
        return playerScore;
    }

    public int getAiScore()
    {
        return aiScore;
    }

}
