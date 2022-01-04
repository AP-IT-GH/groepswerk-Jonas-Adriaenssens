using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHelper : MonoBehaviour
{

    private int score = 0;
    private int punishment = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addSucces()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }


    public void resetScore()
    {
        score = 0;
    }

    public void addPunishment()
    {
        punishment++;
    }

    public int getPunishment()
    {
        return punishment;
    }


    public void resetPunishment()
    {
        punishment = 0;
    }

}
