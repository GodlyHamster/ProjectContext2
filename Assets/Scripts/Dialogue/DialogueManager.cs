using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EasyButtons;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private int currentSentence = 0;

    private bool isSentenceOngoing = false;

    private DialogueData currentDialogueData;

    private void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    [Button]
    public void EnterDialogue(DialogueData data)
    {
        currentDialogueData = data;
        currentSentence = 0;
        dialogueBox.SetActive(true);
        StartCoroutine(DisplaySentece());
    }

    [Button]
    public void ContinueDialogue()
    {
        if (currentSentence+1 == currentDialogueData.sentences.Length)
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

    [Button]
    public void ExitDialogue()
    {
        StopAllCoroutines();
        dialogueBox.SetActive(false);
        currentDialogueData = null;
    }

    private IEnumerator DisplaySentece()
    {
        isSentenceOngoing = true;
        for (int i = 0; i <= currentDialogueData.sentences[currentSentence].Length; i++)
        {
            dialogueText.text = currentDialogueData.sentences[currentSentence].Substring(0, i);
            yield return new WaitForSeconds(0.03f);
        }
        isSentenceOngoing = false;
        yield return null;
    }
}
