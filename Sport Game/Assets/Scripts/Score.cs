using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    

    public int orangeScore = 0;

    public int purpleScore = 0;

    // Start is called before the first frame update
    public void AddScore(string team, int points)
    {

        if(team == "Orange")
        {
            orangeScore += points;
        }
        else if (team == "Purple")
        {
            purpleScore += points;
        }

        UpdateScoreboard();


        if (purpleScore >= 5) PurpleWin();
        if (orangeScore >= 5) OrangeWin();


    }

    // Update is called once per frame
    void UpdateScoreboard()
    {

        scoreText.text = string.Format("{0} | {1}", purpleScore, orangeScore);

    }

    void OrangeWin()
    {
        scoreText.text = "ORANGE WINS!";
    }

    void PurpleWin()
    {
        scoreText.text = "PURPLE WINS!";
    }

}
