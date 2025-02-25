using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [Header("Options")]
    [SerializeField]
    private Transform dialogueOptionParent;
    [SerializeField]
    private GameObject optionButtonPrefab;
    private List<GameObject> currentOptions = new List<GameObject>();

    private int currentSentence = 0;

    private bool isSentenceOngoing = false;

    private DialogueNPC currentDialogueData;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    public void EnterDialogue(DialogueNPC data)
    {
        currentDialogueData = data;
        currentSentence = 0;
        dialogueBox.SetActive(true);
        StartCoroutine(DisplaySentece());
    }

    public void ContinueDialogue()
    {
        SkipToSentence(currentSentence + 1);
    }

    private void SkipToSentence(int sentenceNumber)
    {
        if (sentenceNumber >= currentDialogueData.Sentences.Length)
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
    }

    private IEnumerator DisplaySentece()
    {
        isSentenceOngoing = true;
        float textSpeed = 1f / currentDialogueData.TextSpeed;

        foreach (var item in currentDialogueData.Sentences[currentSentence].options)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, dialogueOptionParent);
            currentOptions.Add(buttonObj);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = item.text;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SkipToSentence(item.linksToNumber));
        }
        for (int i = 0; i <= currentDialogueData.Sentences[currentSentence].text.Length; i++)
        {
            dialogueText.text = currentDialogueData.Sentences[currentSentence].text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        isSentenceOngoing = false;
        yield return null;
    }
}
