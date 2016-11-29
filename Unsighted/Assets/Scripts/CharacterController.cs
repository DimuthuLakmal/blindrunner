using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {

    //movement variables
    public float maxSpeed;
    Rigidbody2D charbody;
    Animator charAnim;
    bool facingRight;
    bool isWalking;

    //touch Detection
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    public Text instructions;

    // Use this for initialization
    void Start () {
        isWalking = false;
        facingRight = true;
        charbody = GetComponent<Rigidbody2D>();
        instructions.text = "a";
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    //left side of the screen
                    Walk(touch);
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    //right side of the screen
                    Flip(touch);
                    Jump(touch);
                    //ShootNPunch();
                }
            }

        }
    }

    void FixedUpdate()
    {
        
    }

    void Flip(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;

            case TouchPhase.Ended:
                float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                if (swipeDistHorizontal > minSwipeDistX)
                {
                    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                    if ((swipeValue > 0 && !facingRight) || (swipeValue < 0 && facingRight))
                    {
                        facingRight = !facingRight;
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                }
                break;
        }
    }

    void Jump(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;

            case TouchPhase.Ended:
                float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                if (swipeDistVertical > minSwipeDistY)
                {
                    float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                    if (swipeValue > 0)
                    {   //up swipe
                        charbody.AddForce(new Vector2(0, 300));
                        //Jump ();
                    }
                    else if (swipeValue < 0)
                    {   //down swipe

                        //Shrink ();
                    }
                }
                break;
        }
    }

    void Walk(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                isWalking = true;
                break;

            case TouchPhase.Ended:
                isWalking = false;
                break;
        }

        if (facingRight)
        {
            charbody.velocity = new Vector2(200 * 0.02f, charbody.velocity.y);
        }
        else
        {
            charbody.velocity = new Vector2(-200 * 0.02f, charbody.velocity.y);
        }
    }
}
