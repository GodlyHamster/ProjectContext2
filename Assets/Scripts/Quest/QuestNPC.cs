using UnityEngine;
using System.Collections.Generic;
using System;

public class QuestNPC : MonoBehaviour, IInteractable, IQuestInterface
{
    [SerializeField]
    [Tooltip("Each dialogue sequence is linked to a quest state")]
    public DialogueSequence[] dialogueSequences;

    public QuestState currentQuestState { get; private set; } = QuestState.CAN_START;

    private bool _currentlyInDialogue = false;

    private void Start()
    {
        DialogueManager.Instance.OnEndDialogue.AddListener(DialogueEnded);
    }

    private void DialogueEnded()
    {
        _currentlyInDialogue = false;
    }

    public void OnStartQuest()
    {
        currentQuestState = QuestState.STARTED;
    }

    public void OnQuestStepComplete(int step)
    {
        Debug.Log($"Completed step {step}");
        //TODO: track steps and only set state to can_finish on last step
        currentQuestState = QuestState.CAN_FINISH;
    }

    public void OnCompleteQuest()
    {
        currentQuestState = QuestState.FINISHED;
    }

    public void OnInteract()
    {
        if (_currentlyInDialogue) return;

        _currentlyInDialogue = true;
        DialogueManager.Instance.EnterDialogue(dialogueSequences[(int)currentQuestState].sentences);
    }
}
