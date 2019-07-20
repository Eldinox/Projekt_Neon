using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator transitionAnim;

    void Start()
    {
        transitionAnim = GameObject.Find("TransitionPanel").GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		string sceneName = "";
            if(this.gameObject.name.Contains("Next"))
            {
                //if(SceneManager.GetActiveScene().name == "Level_1")sceneName = "Level_2";
                if (SceneManager.GetActiveScene().name == "Level_8 (Intro)") sceneName = "Level_6 (Nr 3)";
                //if (SceneManager.GetActiveScene().name == "Level_8 (Intro)") sceneName = "Level_1_25";
               // if (SceneManager.GetActiveScene().name == "Level_8 (Intro)") sceneName = "Level_7 (Boss)";
                else if (SceneManager.GetActiveScene().name == "Level_6 (Nr 3)") sceneName = "Level_5 (Nr2)";
                else if (SceneManager.GetActiveScene().name == "Level_5 (Nr2)") sceneName = "Level_4 (Nr1)";
                else if (SceneManager.GetActiveScene().name == "Level_4 (Nr1)")sceneName = "Level_Hub1";
                else if (SceneManager.GetActiveScene().name == "Level_Hub1")sceneName = "Level_1_22";
                else if (SceneManager.GetActiveScene().name == "Level_1_22") sceneName = "Level_1_23";
                else if (SceneManager.GetActiveScene().name == "Level_1_23") sceneName = "Level_1_24";
                else if (SceneManager.GetActiveScene().name == "Level_1_24") sceneName = "Level_1_25";
                else if (SceneManager.GetActiveScene().name == "Level_1_25") sceneName = "Level_1_21";
                else if (SceneManager.GetActiveScene().name == "Level_1_21") sceneName = "Level_7 (Boss)";
            }
            else if(this.gameObject.name.Contains("Prev"))
            {
                //if(SceneManager.GetActiveScene().name == "Level_2")sceneName = "Level_1";
                if(SceneManager.GetActiveScene().name == "Level_6 (Nr 3)") sceneName = "Level_8 (Intro)";
                else if (SceneManager.GetActiveScene().name == "Level_5 (Nr2)") sceneName = "Level_6 (Nr 3)";
                else if (SceneManager.GetActiveScene().name == "Level_4 (Nr1)") sceneName = "Level_5 (Nr2)";
                else if (SceneManager.GetActiveScene().name == "Level_Hub1") sceneName = "Level_4 (Nr1)";
                else if (SceneManager.GetActiveScene().name == "Level_1_22") sceneName = "Level_Hub1";
                else if (SceneManager.GetActiveScene().name == "Level_1_23") sceneName = "Level_1_22";
                else if (SceneManager.GetActiveScene().name == "Level_1_24") sceneName = "Level_1_23";
                else if (SceneManager.GetActiveScene().name == "Level_1_25") sceneName = "Level_1_24";
                else if (SceneManager.GetActiveScene().name == "Level_1_21") sceneName = "Level_1_25";
                else if (SceneManager.GetActiveScene().name == "Level_7 (Boss)") sceneName = "Level_1_21";

            }
            LoadScene(sceneName);
    	}
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
