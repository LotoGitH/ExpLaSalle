using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI actualScore, bestScore;

    private int _bestScore;

    public int bestScoreEnc
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            bestScore.text = "Best: " + _bestScore;
            PlayerPrefs.SetInt("BestScore",_bestScore);
        }
    }

    private int _scoreInLevel;

    public int scoreInLevenEnc
    {
        get => _scoreInLevel;
        set
        {
            _scoreInLevel = value;
            actualScore.text = "Score: " + _scoreInLevel.ToString();
            if (_scoreInLevel > _bestScore)
            {
                bestScoreEnc = _scoreInLevel;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreEnc = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            bestScoreEnc = 0;
        }

        scoreInLevenEnc = 0;
    }

    public void RestartLevelScore()
    {
        scoreInLevenEnc = 0;
    }

    public void AddScore(int s)
    {
        scoreInLevenEnc = _scoreInLevel + s;
    }

}