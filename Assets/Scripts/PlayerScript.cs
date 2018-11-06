using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    enum LANE
    {
        LEFT = 1,
        MIDDLE = 2,
        RIGHT = 3
    };

    // Public member variables
    public Camera mainCamera;
    public GameObject pauseMenu;

    // Private member variables
    private Animator animator;
    private float xPosition = 0.0f;
    private float yPosition = -2.0f;
    private float maxXPos = 0.0f;
    private float minXPos = 0.0f;
    private float vertSpeed = 0.0f;
    private float size = 1.0f;
    private int currentLane = (int)LANE.MIDDLE;
    private const int maxJumpHeight = 2;
    private const int startYPos = -2;
    private bool jumping = false;
    private bool falling = false;
    private bool running = true;
    private bool verticalTransition = false;

    private Vector2 touchOrigin = -Vector2.one;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        vertSpeed = 0.25f;
        size = 1.0f;
        maxXPos = xPosition + 4;
        minXPos = xPosition - 4;
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {
            GetInput();
            GetComponent<Animator>().StopPlayback();
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
            GetComponent<Animator>().StartPlayback();
        }

        if (verticalTransition == true)
        {
            transform.localScale = new Vector3(size, 0.5f, 1);
            verticalTransition = false;
        }
        else
        {
            transform.localScale = new Vector3(size, size, 1);
        }

        CheckLane();
        Jump();
        Fall();

        transform.position = new Vector3(xPosition, yPosition, 0);

    }

    void GetInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        if (Input.GetButtonDown("Left"))
        {
            MoveLeft();
        }
        else if (Input.GetButtonDown("Right"))
        {
            MoveRight();
        }
        else if (Input.GetButtonDown("Jump") && falling == false)
        {
            MoveUp();
        }
        else if (Input.GetButtonDown("Roll"))
        {

        }

#else
        // touch input
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            //else if (myTouch.phase == TouchPhase.Moved && touchOrigin.x >= 0)
            //{
            //    float x = myTouch.position.x - touchOrigin.x;
            //    float y = myTouch.position.y - touchOrigin.y;

            //    if (Mathf.Abs(x) > Mathf.Abs(y))
            //    {
            //        // move horizontal
            //        if (x > 200)
            //        {
            //            // Right
            //            MoveRight();
            //            touchOrigin.x = -1;
            //        }
            //        else if (x < -200)
            //        {
            //            // Left
            //            MoveLeft();
            //            touchOrigin.x = -1;
            //        }
            //    }
            //    else
            //    {
            //        if (y > 200)
            //        {
            //            MoveUp();
            //            touchOrigin.x = -1;
            //        }
            //    }
            //}
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // move horizontal
                    if (x > 0)
                    {
                        // Right
                        MoveRight();
                    }
                    else
                    {
                        // Left
                        MoveLeft();
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        MoveUp();
                    }
                }
            }
        }

#endif
    }

    void MoveRight()
    {
        if (currentLane == (int)LANE.MIDDLE)
        {
            currentLane = (int)LANE.RIGHT;
        }
        else if (currentLane == (int)LANE.LEFT)
        {
            currentLane = (int)LANE.MIDDLE;
        }
    }

    void MoveLeft()
    {
        if (currentLane == (int)LANE.MIDDLE)
        {
            currentLane = (int)LANE.LEFT;
        }
        else if (currentLane == (int)LANE.RIGHT)
        {
            currentLane = (int)LANE.MIDDLE;
        }
    }

    void MoveUp()
    {
        animator.SetBool("jumping", true);
        vertSpeed = 0.25f;
        jumping = true;
        verticalTransition = true;
    }

    void CheckLane()
    {
        if (currentLane == (int)LANE.LEFT)
        {
            xPosition = minXPos;
            animator.SetBool("middleLane", false);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (currentLane == (int)LANE.MIDDLE)
        {
            xPosition = 0;
            animator.SetBool("middleLane", true);
        }
        else if (currentLane == (int)LANE.RIGHT)
        {
            xPosition = maxXPos;
            animator.SetBool("middleLane", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Jump()
    {
        if (jumping)
        {
            if (yPosition < maxJumpHeight)
            {
                yPosition += vertSpeed;
                if (vertSpeed > 0.15f)
                {
                    vertSpeed -= 0.01f;
                }

                if (size < 1.5f)
                {
                    size += 0.02f;
                }

            }
            else
            {
                jumping = false;
                falling = true;
            }
        }
    }

    void Fall()
    {
        if (falling)
        {
            if (yPosition > startYPos)
            {
                yPosition -= vertSpeed;
                if (vertSpeed < 0.25f)
                {
                    vertSpeed += 0.01f;
                }

                if (size > 1.0f)
                {
                    size -= 0.02f;
                }
            }
            else
            {
                falling = false;
                animator.SetBool("jumping", false);
            }
        }
    }

    public bool GetJumping()
    {
        return jumping;
    }
    public bool GetFalling()
    {
        return falling;
    }
    public bool IsRunning()
    {
        return running;
    }
    public void SetRunning(bool _running)
    {
        running = _running;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
