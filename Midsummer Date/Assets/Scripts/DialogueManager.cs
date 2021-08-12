using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;
    private float dialogueSpeed = 0.005f;
    public QuestionController questionController;

    public Animator animator;
    public GameObject hideButton;
    public GameObject showButton;

    public bool canSkip;

    public GameObject loveMeter;
    private float loveMeterValue;

    private Queue<string> sentences;
    private string activeSentenc;
    private Conversation conversation;
    private bool havingConversation;
    private bool speaker1Active;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();

        loveMeterValue = OptionsManager.Instnace.myData.loveMeter;

        loveMeter.SetActive(false);
    }

    private void Update()
    {       
        if (canSkip)
        {
            if (Input.anyKeyDown)
            {
                if (activeSentenc != null)
                {
                    SkipTypeSentence();
                }
            }
        }    
    }

    public void SkipTypeSentence()
    {
        StopAllCoroutines();
        dialogueText.text = activeSentenc;
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

        sentences.Clear();

        for (int i = 0; i < speaker1.sentences.Length; i++)
        {
            sentences.Enqueue(speaker1.sentences[i]);
            sentences.Enqueue(speaker2.sentences[i]);
        }

        UpdateLoveMeter();

        DisplayNextSentence();

        canSkip = true;
    }

    public void UpdateLoveMeter()
    {
        Slider slider = loveMeter.GetComponent<Slider>();
        float loveValue = conversation.loveScore;

        if (loveValue == 0)
        {
            loveMeter.SetActive(false);
        }
        else
        {
            loveMeter.SetActive(true);
        }
        loveMeterValue += loveValue;
        slider.value = loveMeterValue;
    }

    public void DisplayNextSentence()
    {
        dialogueSpeed = OptionsManager.Instnace.UpdateTypeSpeed(OptionsManager.Instnace.myData.textSpeed);
        audioSource.Play();

        if (sentences.Count == 0)
        {
            if (havingConversation)
            {
                EndConversation();
            }
            else
            {
                EndDialogue();
            }
            return;
        }

        string sentence = sentences.Dequeue();

        activeSentenc = sentence;

        StopAllCoroutines();

        if (havingConversation)
        {
            SetSpeakerName();
        }

        StartCoroutine(TypeSentence(sentence));
    }

    public void EndConversation()
    {
        loveMeter.SetActive(false);
        if (conversation.nextConversation != null)
        {
            StartConversation(conversation.nextConversation);
        }
        else if (conversation.question != null)
        {
            Question question = conversation.question;
            questionController.Change(question);
        }
        else
        {
            animator.SetBool("IsOpen", false);
            havingConversation = false;
            DayManager.Instance.ChangeTime((int)DayManager.Instance.currentTime + 1); //Just testing
            HUDManager hudManager = FindObjectOfType<HUDManager>();
            hudManager.UpdateCanvas();
        }
        activeSentenc = null;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        canSkip = false;
        loveMeter.SetActive(false);
        activeSentenc = null;
        HUDManager hudManager = FindObjectOfType<HUDManager>();
        hudManager.UpdateCanvas();
    }

    public void SetSpeakerName()
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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        if (sentence == "") // Enables monolog parts for one character
        {
            DisplayNextSentence();
        }

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    public void HidWindow()
    {
        animator.SetBool("IsOpen", false);
        hideButton.SetActive(false);
        showButton.SetActive(true);
    }

    public void ShowWindow()
    {
        animator.SetBool("IsOpen", true);
        hideButton.SetActive(true);
        showButton.SetActive(false);
    }

    public int GetLoveValue()
    {
        return (int)loveMeterValue;
    }

    public void ResetLoveMeter()
    {
        Slider slider = loveMeter.GetComponent<Slider>();
      
        loveMeterValue = 0;
        slider.value = loveMeterValue;
    }
}