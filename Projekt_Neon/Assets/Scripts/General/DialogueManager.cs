using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private string[] names;

    public Text nameText;
    public Text dialogueText;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
        GameObject.Find("DeathCanvas").GetComponent<CanvasGroup>().blocksRaycasts = false;

        GameObject.Find("Player").GetComponent<Player>().inDialogue = true;
        
        names = dialogue.names;
        nameText.text = names[0];

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        if(sentence == "0")
        {
            nameText.text = names[0];
            DisplayNextSentence();
        }
        else if(sentence == "1")
        {
            nameText.text = names[1];
            DisplayNextSentence();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        GameObject.Find("DeathCanvas").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("Player").GetComponent<Player>().inDialogue = false;
    }
}
