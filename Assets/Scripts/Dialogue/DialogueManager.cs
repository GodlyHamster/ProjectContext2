using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [Header("Character")]
    [SerializeField]
    private TextMeshProUGUI characterNameText;
    [SerializeField]
    private Image characterImage;

    [Header("Options")]
    [SerializeField]
    private Transform dialogueOptionParent;
    [SerializeField]
    private GameObject optionButtonPrefab;
    private List<GameObject> currentOptions = new List<GameObject>();

    [Header("Audio")]
    [SerializeField]
    private StudioEventEmitter soundEmitter;

    private int currentSentence = 0;

    private bool isSentenceOngoing = false;

    private DialogueText[] currentDialogueData;

    public UnityEvent OnStartDialogue = new UnityEvent();
    public UnityEvent OnEndDialogue = new UnityEvent();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    public void EnterDialogue(DialogueText[] text)
    {
        currentDialogueData = text;
        currentSentence = 0;
        dialogueBox.SetActive(true);
        OnStartDialogue.Invoke();
        SkipToSentence(currentSentence);
    }

    private void SkipToSentence(int sentenceNumber)
    {
        if (sentenceNumber >= currentDialogueData.Length || sentenceNumber < 0)
        {
            ExitDialogue();
            return;
        }

        if (isSentenceOngoing)
        {
            StopAllCoroutines();
        }

        if (currentOptions.Count > 0)
        {
            for (int i = 0; i < currentOptions.Count; i++)
            {
                Destroy(currentOptions[i]);
            }
            currentOptions.Clear();
        }

        currentSentence = sentenceNumber;
        StartCoroutine(DisplaySentece());
    }

    public void ExitDialogue()
    {
        StopAllCoroutines();
        dialogueBox.SetActive(false);
        currentDialogueData = null;
        OnEndDialogue.Invoke();
        soundEmitter.Stop();
    }

    private IEnumerator DisplaySentece()
    {
        isSentenceOngoing = true;

        currentDialogueData[currentSentence].OnDialogueEvent?.Invoke();

        float textSpeed = 1f / currentDialogueData[currentSentence].textSpeed;

        //set character info and scale image correctly
        characterNameText.text = currentDialogueData[currentSentence].character?.characterName;
        characterImage.sprite = currentDialogueData[currentSentence].character?.characterSprite;
        characterImage.SetNativeSize();
        RectTransform imageRect = characterImage.gameObject.GetComponent<RectTransform>();
        imageRect.sizeDelta = (imageRect.sizeDelta / Mathf.Max(imageRect.sizeDelta.x, imageRect.sizeDelta.y)) * 100f;

        //display option buttons
        foreach (var item in currentDialogueData[currentSentence].options)
        {
            AddOptionButton(item.text, item.linksToNumber);
        }
        if (currentOptions.Count == 0)
        {
            AddOptionButton(". . .", currentSentence + 1);
        }

        //typewriter effect
        soundEmitter.Play();
        for (int i = 0; i <= currentDialogueData[currentSentence].text.Length; i++)
        {
            dialogueText.text = currentDialogueData[currentSentence].text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        soundEmitter.Stop();
        isSentenceOngoing = false;
        yield return null;
    }

    private void AddOptionButton(string text, int linksToSentence)
    {
        GameObject buttonObj = Instantiate(optionButtonPrefab, dialogueOptionParent);
        currentOptions.Add(buttonObj);
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = text;
        buttonObj.GetComponent<Button>().onClick.AddListener(() => SkipToSentence(linksToSentence));
    }
}
