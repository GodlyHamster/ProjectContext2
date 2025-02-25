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
        SkipToSentence(currentSentence);
    }

    private void SkipToSentence(int sentenceNumber)
    {
        if (sentenceNumber >= currentDialogueData.Sentences.Length || sentenceNumber < 0)
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
            AddOptionButton(item.text, item.linksToNumber);
        }
        if (currentOptions.Count == 0)
        {
            AddOptionButton(". . .", currentSentence + 1);
        }

        for (int i = 0; i <= currentDialogueData.Sentences[currentSentence].text.Length; i++)
        {
            dialogueText.text = currentDialogueData.Sentences[currentSentence].text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
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
