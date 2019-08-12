using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBomb : MonoBehaviour
{
    private CircleCollider2D myCircleCollider;
    private float activeExplosionTime;
    // Start is called before the first frame update
    void Start()
    {
        myCircleCollider = GetComponent<CircleCollider2D>();
        activeExplosionTime = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        activeExplosionTime = activeExplosionTime - 1* Time.deltaTime;
        if(activeExplosionTime <= 0)
        {
            myCircleCollider.enabled = false;
        }
    }
}
