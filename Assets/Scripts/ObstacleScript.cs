using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ObstacleScript : MonoBehaviour {

    enum Lane
    {
        LEFT = 1,
        MIDDLE = 2,
        RIGHT = 3
    }

    public GameObject particles;

    private Camera mainCamera;
    private GameObject Player;

    // Private member variables
    //private UIManager uiManager;
    private float alpha = 0.0f;
    private float color = 1.0f;
    private float yPosition = 4.0f;
    private float xPosition = 0.0f;
    private float size = 0.4f;
    private int CurrentLane = (int)Lane.MIDDLE;
    private bool inFrontOfPlayer = true;
    private SpriteRenderer sprite;
    private Color originalColor;

    // Use this for initialization
    void Start()
    {
        //uiManager = FindObjectOfType<UIManager>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        mainCamera = Camera.main;
        Player = GameObject.Find("Player");
        //CurrentLane = Random.Range(1, 4);
        xPosition = transform.position.x;
        yPosition = 4.0f;
        if (xPosition == -0.5f)
        {
            CurrentLane = (int)Lane.LEFT;
        }
        else if (xPosition == 0.0f)
        {
            CurrentLane = (int)Lane.MIDDLE;
        }
        else
        {
            CurrentLane = (int)Lane.RIGHT;
        }
        ResetObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerScript>().IsRunning() == true)
        {
            if (color > 0.4)
            {
                color -= 0.01f;
            }
            alpha += 0.05f;
            sprite.color = new Color(color, color, color, alpha);

           // if (transform.position.y < Player.transform.position.y)
            if (transform.position.y < -2)
            {
                //sprite.color = originalColor - new Color(0.5f, 0.5f, 0.5f, 0.0f);
                sprite.sortingOrder = 11;
                inFrontOfPlayer = false;
            }

            // Increment position and size
            if (CurrentLane == (int)Lane.LEFT)
            {
                xPosition -= 0.05f;
                yPosition -= 0.1f;
                size += 0.01f;
            }
            else if (CurrentLane == (int)Lane.MIDDLE)
            {
                yPosition -= 0.1f;
                size += 0.01f;
            }
            else if (CurrentLane == (int)Lane.RIGHT)
            {
                xPosition += 0.05f;
                yPosition -= 0.1f;
                size += 0.01f;
            }

            // Goes off the screen
            if (yPosition + size + 1 < (-1 * mainCamera.orthographicSize))
            {
                Destroy(gameObject);
            }

            transform.position = new Vector3(xPosition, yPosition, 0);
            transform.localScale = new Vector3(size, size / 2, 1);
        }
    }
     
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (inFrontOfPlayer == true)
            {
                if (Player.GetComponent<PlayerScript>().GetJumping() == false)
                {
                    //Player.GetComponent<PlayerScript>().SetRunning(false);
                    Instantiate(particles, transform.position, transform.rotation);
                    //PUT A DELAY HERE-------------------------
                    // mabye instead of this, call a function in the player script and put this there
                    SceneManager.LoadScene("Gameover");
                }
            }
        }
    }

    void ResetObstacle()
    {
        // Reset position and size
        if (CurrentLane == (int)Lane.LEFT)
        {
            xPosition = -0.75f;
        }
        else if (CurrentLane == (int)Lane.MIDDLE)
        {
            xPosition = 0.0f;
        }
        else if (CurrentLane == (int)Lane.RIGHT)
        {
            xPosition = 0.75f;
            
        }

        yPosition = 4.0f;
        size = 0.3f;
        inFrontOfPlayer = true;
        sprite.sortingOrder = 1;
        sprite.color = originalColor;
    }

}
