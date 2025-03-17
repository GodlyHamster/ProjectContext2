using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QuestNPC : MonoBehaviour, IInteractable, IQuestInterface
{
    [SerializeField]
    [Tooltip("Each dialogue sequence is linked to a quest state")]
    private DialogueSequence[] dialogueSequences;

    private int totalSteps = 0;
    private int currentStep = 0;

    public QuestState currentQuestState { get; private set; } = QuestState.CAN_START;

    private bool _currentlyInDialogue = false;

    public UnityEvent<QuestState> OnQuestStateUpdated = new UnityEvent<QuestState>();

    private void Start()
    {
        DialogueManager.Instance.OnEndDialogue.AddListener(DialogueEnded);

        for (int i = 0; i < dialogueSequences.Length; i++)
        {
            if (dialogueSequences[i].state == QuestState.STARTED) totalSteps++;
        }
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
        if (currentStep >= totalSteps)
        {
            currentQuestState = QuestState.CAN_FINISH;
        }
    }

    public void OnCompleteQuest()
    {
        if (currentQuestState != QuestState.CAN_FINISH) return;
        currentQuestState = QuestState.FINISHED;
    }

    public void OnInteract()
    {
        if (_currentlyInDialogue) return;

        _currentlyInDialogue = true;

        DialogueSequence currentDialogueSequence = GetCorrectDialogueSequence();
        if (currentDialogueSequence == null) return;

        DialogueManager.Instance.EnterDialogue(currentDialogueSequence.sentences);
    }

    private DialogueSequence GetCorrectDialogueSequence()
    {
        for (int i = 0; i < dialogueSequences.Length; i++)
        {
            DialogueSequence currentSequence = dialogueSequences[i];
            if (currentSequence.state == currentQuestState)
            {
                if (currentSequence.state == QuestState.STARTED && currentSequence.stepNumber != currentStep) continue;
                return currentSequence;
            }
        }
        return null;
    }
}
