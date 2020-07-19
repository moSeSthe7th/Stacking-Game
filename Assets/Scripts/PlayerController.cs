using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtmostInput;

public class PlayerController : MonoBehaviour
{
    public Transform bezierRoute;

    bool isGameStarted;

    public float tParam;
    public float playerSpeed;

    Transform t0;
    Transform t1;
    Transform t2;
    Transform t3;

    Vector3 v0;
    Vector3 v1;
    Vector3 v2;
    Vector3 v3;

    public Transform rightLowerArm;
    public Transform leftLowerArm;
 
    public GameObject stackArea;

    InputX inputX;

    bool moveHandsToRight;
    bool moveHandsToLeft;

    Vector2 touchStartPos;
    Vector3 targetPos;

    Vector3 armPos;

    UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType(typeof(UIManager)) as UIManager;

        playerSpeed = 0.4f;
        isGameStarted = false;

        t0 = bezierRoute.GetChild(0);
        t1 = bezierRoute.GetChild(1);
        t2 = bezierRoute.GetChild(2);
        t3 = bezierRoute.GetChild(3);

        tParam = 0.5f;


        inputX = new InputX();
        moveHandsToRight = false;
        moveHandsToLeft = false;

        armPos = Vector3.zero;
    }

    
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, playerSpeed);

        if (inputX.IsInput())
        {
            isGameStarted = true;

            GeneralInput gInput = inputX.GetInput(0);

            if(gInput.phase == IPhase.Began || gInput.phase == IPhase.Stationary || gInput.phase == IPhase.Ended || gInput.phase == IPhase.Canceled)
            {
                touchStartPos = gInput.currentPosition;
            }
            else
            {

                Vector2 touchAlpha = gInput.currentPosition - touchStartPos;
                if(touchAlpha.x > 1f && ! moveHandsToRight)
                {
                    moveHandsToRight = true;
                    moveHandsToLeft = false;
                }
                else if(touchAlpha.x < -1f && !moveHandsToLeft)
                {
                    moveHandsToLeft = true;
                    moveHandsToRight = false;
                }
            }
        }


    }

    private void LateUpdate()
    {
        if (!isGameStarted)
            return;

        v0 = t0.position;
        v1 = t1.position;
        v2 = t2.position;
        v3 = t3.position;



        if (moveHandsToRight)
        {
            if (tParam <= 0.95f)
            {
                tParam += 0.1f;
            }
            /*targetPos = transform.position;
            targetPos.x += 3f;
            targetPos.y += 0.3f;
            rightLowerArm.position = targetPos;
            leftLowerArm.position = targetPos;
            stackArea.transform.position = Vector3.MoveTowards(stackArea.transform.position, targetPos, 0.5f);

            rightLowerArm.position = stackArea.transform.position;
            leftLowerArm.position = stackArea.transform.position;*/
        }
        else if (moveHandsToLeft)
        {
            if (tParam >= 0.05f)
            {
                tParam -= 0.1f;
                /*targetPos = transform.position;
                targetPos.x -= 3f;
                targetPos.y += 0.3f;
                stackArea.transform.position = Vector3.MoveTowards(stackArea.transform.position, targetPos, 0.5f);

                rightLowerArm.position = stackArea.transform.position;
                leftLowerArm.position = stackArea.transform.position;*/
            }
        }

       
        Vector3 armPos = Mathf.Pow(1 - tParam, 3) * v0 +
                        3 * Mathf.Pow(1 - tParam, 2) * tParam * v1 +
                        3 * (1 - tParam) * Mathf.Pow(tParam, 2) * v2 +
                        Mathf.Pow(tParam, 3) * v3;

        stackArea.transform.position = armPos;

        rightLowerArm.position = stackArea.transform.position;
        leftLowerArm.position = stackArea.transform.position;
    }

    public void GameOverAnimations()
    {
        uiManager.GameOver();
    }

    public void LevelPassedAnimations()
    {
        uiManager.LevelPassed();
    }
}
