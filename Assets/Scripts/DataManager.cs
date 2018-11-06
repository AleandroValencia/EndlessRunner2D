using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataManager : MonoBehaviour
{
    public PlayerScript Player;
    private int highScore = 0;
    private float highDistance = 0;
    private string highScoreKey = "HighScore";
    private string highScoreStringKey = "HighScoreString";
    private string highDistanceKey = "HighDistance";
    private string highDistanceStringKey = "HighDistanceString";
    private static string highScoreString;
    private static string highDistanceString;
    private static int score;
    private static float distance;
    private static string scoreString;
    private static string distanceString;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);

        // Singleton 
        // if there is more than one of this type of object, destroy it
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        scoreString = "Score: 0000";
        distanceString = "Distance: 000000";

        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreString = PlayerPrefs.GetString(highScoreStringKey, "High Score: 0000");
        highDistance = PlayerPrefs.GetFloat(highDistanceKey, 0);
        highDistanceString = PlayerPrefs.GetString(highDistanceStringKey, "High Distance: 000000");

        //Player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            if (Player.IsRunning() == true)
            {
                distance += 0.1f;
                int roundedDistance = Mathf.FloorToInt(distance);
                int distanceCharLength = roundedDistance.ToString().Length;
                int padding = 6 - distanceCharLength;

                distanceString = "Distance: ";

                for (int i = 0; i < padding; ++i)
                {
                    distanceString += "0";
                }

                distanceString += roundedDistance;

                if (distance > highDistance)
                {
                    highDistance = distance;
                    highDistanceString = "High " + distanceString;
                    PlayerPrefs.SetFloat(highDistanceKey, distance);
                    PlayerPrefs.SetString(highDistanceStringKey, highDistanceString);
                    PlayerPrefs.Save();
                }
            }
        }
        else
        {
            Player = FindObjectOfType<PlayerScript>();
            distance = 0.0f;
        }


    }

    public void IncrementScore()
    {
        score++;

        int scoreCharLength = score.ToString().Length;
        int padding = 4 - scoreCharLength;

        scoreString = "Score: ";

        for (int i = 0; i < padding; ++i)
        {
            scoreString += "0";
        }

        scoreString += score;

        // Check high score
        if (score > highScore)
        {
            highScore = score;
            highScoreString = "High " + scoreString;
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.SetString(highScoreStringKey, highScoreString);
            PlayerPrefs.Save();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public string GetScoreString()
    {
        return scoreString;
    }

    public string GetDistanceString()
    {
        return distanceString;
    }

    public string GetHighScoreString()
    {
        return highScoreString;
    }

    public string GetHighDistanceString()
    {
        return highDistanceString;
    }

    public void SetScore(int _score)
    {
        score = _score;
    }

    public void SetDistance(float _distance)
    {
        distance = _distance;
    }
}
