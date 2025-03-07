using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

public class QuestNPC : MonoBehaviour, IInteractable, IQuestInterface
{
    [SerializeField]
    [Tooltip("Each dialogue sequence is linked to a quest state")]
    private DialogueSequence[] dialogueSequences;

    [SerializeField]
    private int totalSteps = 0;
    private int currentStep = 0;

    public QuestState currentQuestState { get; private set; } = QuestState.CAN_START;

    private bool _currentlyInDialogue = false;

    public UnityEvent<QuestState> OnQuestStateUpdated = new UnityEvent<QuestState>();

    private void Start()
    {
        DialogueManager.Instance.OnEndDialogue.AddListener(DialogueEnded);
    }

    private void DialogueEnded()
    {
        _currentlyInDialogue = false;
        OnQuestStateUpdated.Invoke(currentQuestState);
    }

    public void OnStartQuest()
    {
        currentQuestState = QuestState.STARTED;
    }

    public void OnQuestStepComplete(int step)
    {
        currentStep = step;
        if (currentStep >= totalSteps - 1)
        {
            currentQuestState = QuestState.CAN_FINISH;
        }
    }

    public void OnCompleteQuest()
    {
        currentQuestState = QuestState.FINISHED;
    }

    public void OnInteract()
    {
        if (_currentlyInDialogue) return;

        _currentlyInDialogue = true;

        int currentDialogueInt = (int)currentQuestState + currentStep;

        DialogueManager.Instance.EnterDialogue(dialogueSequences[currentDialogueInt].sentences);
    }
}
