using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

    public GameObject Bush;
    public GameObject TreeOne;
    public GameObject TreeTwo;
    public GameObject Lolly;
    public GameObject Obstacle;
    public GameObject Player;
    private DataManager dataManager;
    private int score;
    private int maxRandNumb;

    private int whichLane;
    private int obstacleLane;
    private float lollyXPos = 0.0f;
    private float oldLollyXPos = 0.0f;
    private float obsXPos = 0.0f;
    private float obsXPos2 = 0.0f;

    private float timer = 0.0f;
    private float delay = 0.6f;

	// Use this for initialization
	void Start () {
        dataManager = FindObjectOfType<DataManager>();
        maxRandNumb = 5;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (Player.GetComponent<PlayerScript>().IsRunning() == true)
        {
            score = dataManager.GetScore();
            if (score < 35)
            {
                delay = 0.5f;
            }
            else if (score >= 35 && score < 100)
            {
                maxRandNumb = 4;
                delay = 0.4f;
            }
            else if (score >= 100 && score < 300)
            {
                maxRandNumb = 3;
                delay = 0.2f;
            }
            else
            {
                delay = 0.10f;
            }
            
            if (timer >= delay)
            {
                // check if coin spawns two lanes away from previous coin
                do
                {
                    whichLane = Random.Range(1, 4);

                    if (whichLane == 1)
                    {
                        lollyXPos = -0.5f;
                    }
                    else if (whichLane == 2)
                    {
                        lollyXPos = 0.0f;
                    }
                    else
                    {
                        lollyXPos = 0.5f;
                    }
                } while (lollyXPos == oldLollyXPos + 1 || lollyXPos == oldLollyXPos - 1);

                oldLollyXPos = lollyXPos;
                
                Instantiate(Lolly, new Vector3(lollyXPos, 4.0f, 0.0f), transform.rotation);

                // Spawn obstacle 
                if (Random.Range(1, maxRandNumb) == 1)
                {
                    do
                    {
                        obstacleLane = Random.Range(1, 4);
                    } while (obstacleLane == whichLane);
                    
                    if(obstacleLane == 1)
                    {
                        obsXPos = -0.5f;
                    }
                    else if (obstacleLane == 2)
                    {
                        obsXPos = 0.0f;
                    }
                    else
                    {
                        obsXPos = 0.5f;
                    }
                    
                    Instantiate(Obstacle, new Vector3(obsXPos, 10.0f, 0.0f), transform.rotation);

                    // spawn second obstacle
                    if (Random.Range(1, maxRandNumb) == 1)
                    {
                        if (whichLane == 1)
                        {
                            if (obstacleLane == 2)
                            {
                                obsXPos2 = 0.5f;
                            }
                            else if (obsXPos2 == 3)
                            {
                                obsXPos2 = 0.0f;
                            }
                        }
                        else if (whichLane == 2)
                        {
                            if (obstacleLane == 1)
                            {
                                obsXPos2 = 0.5f;
                            }
                            else if (obstacleLane == 3)
                            {
                                obsXPos2 = -0.5f;
                            }
                        }
                        else if (whichLane == 3)
                        {
                            if (obstacleLane == 1)
                            {
                                obsXPos2 = 0.0f;
                            }
                            else if (obstacleLane == 2)
                            {
                                obsXPos2 = -0.5f;
                            }
                        }
                        Instantiate(Obstacle, new Vector3(obsXPos2, 10.0f, 0.0f), transform.rotation);
                    }
                }
                
                timer = 0.0f;
            }

        }
	}
}
