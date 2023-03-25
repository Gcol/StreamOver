using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{

    public string[] staticDirection = { "Intro", "Extro"};
    public int state = -1;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            state += 1;
            if (state < staticDirection.Length)
            {
                anim.Play(staticDirection[state]);
            }

        }
    }
}
