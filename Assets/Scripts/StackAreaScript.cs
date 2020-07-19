using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAreaScript : MonoBehaviour
{
    Vector3 stackPos;
    float height;

    IKControl ikControl;

    bool isFallingOnGround;
    bool isFallingFinished;

    List<Transform> collectedCubes;

    public int stackColorCode;

    GameMngr gameMngr;
    PlayerController playerController;
    void Start()
    {
        stackColorCode = DataScript.currentColorCode;
        isFallingFinished = false;
        isFallingOnGround = false;
        height = 0;
        ikControl = GetComponentInParent<IKControl>();
        collectedCubes = new List<Transform>();

        gameMngr = FindObjectOfType(typeof(GameMngr)) as GameMngr;
        playerController = GetComponentInParent<PlayerController>();

    }

  
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "CollectibleCube")
        {
            if(other.gameObject.GetComponent<CubeScript>().cubeColorCode == stackColorCode)
            {
                DataScript.cubeCount++;
                other.GetComponent<Collider>().enabled = false;
                stackPos = transform.position;
                stackPos.y += height;
                collectedCubes.Add(other.transform);
                other.gameObject.transform.SetParent(transform);
                other.gameObject.transform.position = stackPos;


                height += other.transform.localScale.y / 2;

                ikControl.lookObj = other.gameObject.transform;

                other.gameObject.GetComponent<CubeScript>().Collected();
            }
            else
            {
                DataScript.cubeCount--;
                other.GetComponent<Collider>().enabled = false;
                if (DataScript.cubeCount <= 0)
                {
                    playerController.GameOverAnimations();
                    return;
                }

                GameObject go = collectedCubes[collectedCubes.Count - 1].gameObject;
                collectedCubes.RemoveAt(collectedCubes.Count - 1);
                go.transform.SetParent(null);
                
                height -= 0.2f;

            }
        }
        else if(other.gameObject.tag == "FinishLine")
        {
            other.GetComponent<Collider>().enabled = false;
            GetComponentInParent<PlayerController>().LevelPassedAnimations();
        }
        else if(other.gameObject.tag == "GroundObstacle")
        {
           

            if (!isFallingOnGround)
            {
                isFallingFinished = false;
                isFallingOnGround = true;
                StartCoroutine(FallOnGroundObstacle());
            }
        }

        else if (other.gameObject.tag == "Obstacle")
        {
            int rowC = other.gameObject.GetComponent<ObstacleScript>().rowCount;

            if (DataScript.cubeCount <= rowC)
            {
                playerController.GameOverAnimations();
                return;
            }

            other.GetComponent<Collider>().enabled = false;

            
            Debug.Log("rowc" + rowC);

            for(int i = 0; i < rowC; i++)
            {
                collectedCubes[0].parent = null;
                collectedCubes.RemoveAt(0);
                height -= 0.2f;
                DataScript.cubeCount--;
            }

           
            StartCoroutine(FallTheCubes(rowC));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "GroundObstacle")
        {
            Debug.Log("Trigger Exit ground obstacle");
            isFallingOnGround = false;

            isFallingFinished = true;
        }

        else if(other.gameObject.tag == "ColorChangerArea")
        {
            stackColorCode = other.gameObject.GetComponent<ColorChangerAreaScript>().colorCode;
            DataScript.currentColorCode = stackColorCode;
            gameMngr.ChangeGroundMatColor(stackColorCode);
            gameMngr.ChangePlayerMatColor();
            StartCoroutine(ChangeCubeColors());
        }
    }

    IEnumerator ChangeCubeColors()
    {
        foreach(Transform collectedCube in collectedCubes)
        {
            if(collectedCube != transform)
            {
                collectedCube.GetComponent<CubeScript>().SetCubeColor(stackColorCode);
                yield return new WaitForEndOfFrame();
            }
        }
        
    }

    IEnumerator FallTheCubes(int rowC)
    {
        foreach (Transform collectedCube in collectedCubes)
        {
            if (collectedCube != transform)
            {
                collectedCube.gameObject.GetComponent<CubeScript>().FallBy(rowC * 0.2f);
            }
           
            yield return new WaitForSecondsRealtime(0.001f);
           
        }
      
        StopCoroutine(FallTheCubes(rowC));
    }

    IEnumerator FallOnGroundObstacle()
    {
        while(!isFallingFinished)
        {
            if (DataScript.cubeCount <= 1)
            {
                playerController.GameOverAnimations();
                StopCoroutine(FallOnGroundObstacle());
            }
            else
            {
                collectedCubes[0].parent = null;
                collectedCubes.RemoveAt(0);
                height -= 0.2f;
                DataScript.cubeCount--;

                foreach (Transform collectedCube in collectedCubes)
                {
                    if (collectedCube != transform)
                    {

                        collectedCube.gameObject.GetComponent<CubeScript>().FallBy(0.2f);
                    }
                }
            }

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
