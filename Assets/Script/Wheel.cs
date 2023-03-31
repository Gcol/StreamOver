using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public float genSpeed;
    public float speedModifier;


    // Start is called before the first frame update
    void Start()
    {
        genSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (genSpeed > 0)
        {
            transform.Rotate(genSpeed, 0, 0, Space.World);
            genSpeed -= speedModifier;
        }
    }

    public void SpinWheel()
    {
        genSpeed = Random.Range(2.000f, 5.0000f);
        speedModifier = Random.Range(0.003f, 0.009f);

    }
}
