using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int cubeColorCode;
    ParticleSystem particleSystem;
    Material cubeMat;

    void Start()
    {
        cubeMat = GetComponent<Renderer>().material;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        SetCubeColor(cubeColorCode);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCubeColor(int colorCode)
    {
        cubeColorCode = colorCode;
        cubeMat.color = DataScript.colors[cubeColorCode];
        cubeMat.SetColor("_EmissionColor", DataScript.colors[cubeColorCode]);
        Collected();
    }

    public void Collected()
    {
        var mainModule = particleSystem.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(DataScript.colors[cubeColorCode]);
        particleSystem.Play();
    }

    public void FallBy(float fallAmount)
    {
       
        StartCoroutine(FallAnim(fallAmount));
    }

    IEnumerator FallAnim(float fallAmount)
    {
        
        yield return new WaitForSecondsRealtime(0.1f);
        Vector3 animPos = transform.position;
        animPos.y -= fallAmount;
        Vector3 aimedPos = transform.position;

        while (Mathf.Abs(animPos.y-transform.position.y) > 0.18f)
        {

            aimedPos = transform.position;
            aimedPos.y = animPos.y;
            transform.position = Vector3.MoveTowards(transform.position, aimedPos, 0.2f);
            yield return new WaitForEndOfFrame();
        }
        /*animPos.y += fallAmount / 2f;
        while (Vector3.SqrMagnitude(animPos - transform.position) > 0.001f)
        {
          
            transform.position = Vector3.MoveTowards(transform.position, animPos, 0.001f);
            yield return new WaitForEndOfFrame();
        }
        animPos.y -= fallAmount / 2f;
        while (Vector3.SqrMagnitude(animPos - transform.position) > 0.001f)
        {
        
            transform.position = Vector3.MoveTowards(transform.position, animPos, 0.001f);
            yield return new WaitForEndOfFrame();
        }
        */
     
        StopCoroutine(FallAnim(fallAmount));
    }
}
