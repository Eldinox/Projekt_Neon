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
                if(SceneManager.GetActiveScene().name == "Level_1")sceneName = "Level_2";
                //else if(SceneManager.GetActiveScene().name == "Level_2")sceneName = "Level_3";
                //else if(SceneManager.GetActiveScene().name == "Level_3")sceneName = "Level_4";
            }
            else if(this.gameObject.name.Contains("Prev"))
            {
                if(SceneManager.GetActiveScene().name == "Level_2")sceneName = "Level_1";
                //else if(SceneManager.GetActiveScene().name == "Level_3")sceneName = "Level_2";
                //else if(SceneManager.GetActiveScene().name == "Level_4")sceneName = "Level_3";
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
