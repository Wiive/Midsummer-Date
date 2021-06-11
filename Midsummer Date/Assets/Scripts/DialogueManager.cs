using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.PlayerLoop;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;
    private float dialogueSpeed = 0.01f;

    public Animator animator;

    public bool canSkip;
    public bool skipped;

    private Queue<string> sentences;

    public static DialogueManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (canSkip)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("You skipped clicked");
                skipped = true;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        speakerName.text = dialogue.speakerName;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        canSkip = true;
    }


    public void DisplayNextSentence()
    {
        skipped = false;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();

        if (skipped)
        {
            Debug.Log("Trying to skip type");
            StopAllCoroutines();
            dialogueText.text = sentence;
        }
        else
        {
            StartCoroutine(TypeSentence(sentence));
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        canSkip = false;
        skipped = false;
    }

    public void StartConversation(Conversation conversation)
    {

    }

}