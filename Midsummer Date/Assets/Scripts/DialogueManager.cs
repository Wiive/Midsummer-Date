using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;
using System.Linq;

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
    private Conversation conversation;
    private bool havingConversation;
    private bool speaker1Active;

    //Quick fix, will rework later
    public UnityEvent endOfDialogue;

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
        if (endOfDialogue == null)
        {
            endOfDialogue = new UnityEvent();
        }
        
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

        speakerName.text = dialogue.character.speakerName;

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

        if (havingConversation)
        {
            if (speaker1Active)
            {
                speakerName.text = conversation.speaker1.character.speakerName;
            }
            else
            {
                speakerName.text = conversation.speaker2.character.speakerName;
            }
            speaker1Active = !speaker1Active;
                            
        }

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
        endOfDialogue.Invoke();
    }

    public void StartConversation(Conversation conversation)
    {
        animator.SetBool("IsOpen", true);
        havingConversation = true;
        speaker1Active = true;

        if (conversation == null)
        {
            Debug.LogError("You havn't added a conversation to this speaker");
        }

        this.conversation = conversation;
        var speaker1 = conversation.speaker1;
        var speaker2 = conversation.speaker2;

        for (int i = 0; i < speaker1.sentences.Length; i++)
        {
           sentences.Enqueue(speaker1.sentences[i]);
           sentences.Enqueue(speaker2.sentences[i]);
        }

        DisplayNextSentence();
    }

    public void EndConversation()
    {
        animator.SetBool("IsOpen", false);
        havingConversation = false;
        endOfDialogue.Invoke();      
    }

}