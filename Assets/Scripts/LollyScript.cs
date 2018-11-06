using UnityEngine;
using System.Collections;

public class LollyScript : MonoBehaviour {
    
    enum Lane
    {
        LEFT = 1,
        MIDDLE = 2,
        RIGHT = 3
    }

    // Public member variables
    //public Camera mainCamera;
    private Camera mainCamera;
    private GameObject Player;
    public GameObject Particle;
    public Sprite lollieBlue;
    public Sprite lollieBR;
    public Sprite lollieGP;
    public Sprite lolliePurple;
    public Sprite lollieRed;
    public Sprite lollieTiger;
    public Sprite lollieYellow;
    private Sprite selectedLollie;
    private int lollieSelector;

    // Private member variables
    private DataManager dataManager;
    private float yPosition = 4.0f;
    private float xPosition = 0.0f;
    private float xSpeed = 0.05f;
    private float ySpeed = 0.1f;
    private float size = 0.1f;
    private float color = 1.0f;
    private float alpha = 0.0f;
    private int CurrentLane = (int)Lane.MIDDLE;
    private int health = 20;
    private bool inAir = false;
    private bool fadeAway = false;

	// Use this for initialization
	void Start() 
    {
        dataManager = FindObjectOfType<DataManager>();
        mainCamera = Camera.main;
        Player = GameObject.Find("Player");

        xPosition = transform.position.x;
        if (xPosition == -0.5f)
        {
            CurrentLane = (int)Lane.LEFT;
        }
        else if (xPosition == 0.0f)
        {
            CurrentLane = (int)Lane.MIDDLE;
        }
        else if (xPosition  == 0.5f)
        {
            CurrentLane = (int)Lane.RIGHT;
        }

        yPosition = 4.0f;
        size = 0.1f;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        alpha = GetComponent<SpriteRenderer>().color.a;

        lollieSelector = Random.Range(1, 8);

        switch(lollieSelector)
        {
            case 1:
                selectedLollie = lollieBlue;
                break;
            case 2:
                selectedLollie = lollieBR;
                break;
            case 3:
                selectedLollie = lollieGP;
                break;
            case 4:
                selectedLollie = lolliePurple;
                break;
            case 5:
                selectedLollie = lollieRed;
                break;
            case 6:
                selectedLollie = lollieTiger;
                break;
            case 7:
                selectedLollie = lollieYellow;
                break;
            default:
                selectedLollie = lollieRed;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = selectedLollie;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Player.GetComponent<PlayerScript>().IsRunning() == true)
        {
            alpha += 0.01f;
            if (color > 0.4f)
            {
                color -= 0.005f;
            }

            if (transform.position.y < -2)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 11;
            }

            // Increment position and size
            if (CurrentLane == (int)Lane.LEFT)
            {
                xPosition -= xSpeed;
                yPosition -= ySpeed;
                size += 0.01f;
            }
            else if (CurrentLane == (int)Lane.MIDDLE)
            {
                yPosition -= ySpeed;
                size += 0.01f;
            }
            else if (CurrentLane == (int)Lane.RIGHT)
            {
                xPosition += xSpeed;
                yPosition -= ySpeed;
                size += 0.01f;
            }

            // Goes off the screen
            if (yPosition + size < (-1 * mainCamera.orthographicSize))
            {
                Destroy(gameObject);
            }

            if (fadeAway == true)
            {
                yPosition += 0.2f;
                health--;
                alpha -= 0.05f;
                GetComponent<SpriteRenderer>().color = new Color(color, color, color, alpha);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            GetComponent<SpriteRenderer>().color = new Color(color, color, color, alpha);
            transform.position = new Vector3(xPosition, yPosition, 0);
            transform.localScale = new Vector3(size, size, 1);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // if lolly is within z-axis range of player
            if (inAir == false && Player.GetComponent<PlayerScript>().GetJumping() == false)
            {
                fadeAway = true;
                // Increment score
                dataManager.IncrementScore();
                Instantiate(Particle, transform.position, transform.rotation);
            }
            else if (inAir == true && (Player.GetComponent<PlayerScript>().GetFalling() == true || Player.GetComponent<PlayerScript>().GetJumping() == true))
            {
                Destroy(gameObject);
                // Increment score
                dataManager.IncrementScore();
                Instantiate(Particle, transform.position, transform.rotation);
            }
        }
    }

    public void SetLane(int _whichLane)
    {
        CurrentLane = _whichLane;
    }

    public void SetXSpeed(float _speed)
    {
        xSpeed = _speed;
    }

    public void SetYSpeed(float _speed)
    {
        ySpeed = _speed;
    }
}
