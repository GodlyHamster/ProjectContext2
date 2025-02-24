using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

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
        if (currentSentence+1 == currentDialogueData.Sentences.Length)
        {
            ExitDialogue();
            return;
        }

        if (isSentenceOngoing) 
        {
            StopAllCoroutines();
        }

        currentSentence++;
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
        for (int i = 0; i <= currentDialogueData.Sentences[currentSentence].Length; i++)
        {
            dialogueText.text = currentDialogueData.Sentences[currentSentence].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        isSentenceOngoing = false;
        yield return null;
    }
}
