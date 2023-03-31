using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{

    public string[] staticDirection = { "Intro", "Extro"};
    public int state = -1;

    public Animator anim;
    public bool changeStateOk = true;

    public string nextAnimation;


    // Start is called before the first frame update
    void Start()
    {
        changeStateOk = true;
        anim.Play("Waiting");
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Waiting") && nextAnimation != null)
        {
            Debug.LogFormat("Change animation {0}", nextAnimation);
            StartCoroutine(CoRoutineAnimationCamera(nextAnimation));
            nextAnimation = null;
        }

        if (Input.GetButtonDown("Jump") && nextAnimation == null)
        {
            if (changeStateOk == true)
            {
                state += 1;
                nextAnimation = staticDirection[state];
                if (state >= 1)
                {
                    state = -1;
                }

            }

        }
        if (Input.GetButtonDown("Cancel"))
        {
            quitFonction();
        }
    }

    public void quitFonction()
    {
        SubManager currentSub;
        currentSub = GetComponent<SubManager>();
        currentSub.SaveSub();

#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadScene(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }


    IEnumerator CoRoutineAnimationCamera(string animationName)
    {
        changeStateOk = false;
        anim.Play(animationName);
        yield return new WaitForSeconds(1f);
        changeStateOk = true;
    }
}
