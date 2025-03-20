using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QuestNPC : MonoBehaviour, IInteractable, IQuestInterface
{
    [SerializeField]
    private DialogueSequence[] dialogueSequences;

    [SerializeField]
    private QuestData questData = new QuestData();
    private int totalSteps = 0;

    public QuestState currentQuestState { get { return questData.state; } }

    private bool _currentlyInDialogue = false;

    public UnityEvent<QuestState> OnQuestStateUpdated = new UnityEvent<QuestState>();

    private void Start()
    {
        if (QuestManager.instance?.GetQuestData(questData.name) == null)
        {
            QuestManager.instance?.AddQuest(questData);
        }
        else
        {
            questData = QuestManager.instance?.GetQuestData(questData.name);
            CheckCompletionAvailable();
            OnQuestStateUpdated.Invoke(questData.state);
        }

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
        questData.state = QuestState.STARTED;
    }

    public void OnQuestStepComplete(int step)
    {
        questData.step = step;
        CheckCompletionAvailable();
    }

    private void CheckCompletionAvailable()
    {
        if (questData.step >= totalSteps)
        {
            questData.state = QuestState.CAN_FINISH;
        }
    }

    public void OnCompleteQuest()
    {
        if (currentQuestState != QuestState.CAN_FINISH) return;
        questData.state = QuestState.FINISHED;
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
                if (currentSequence.state == QuestState.STARTED && currentSequence.stepNumber != questData.step) continue;
                return currentSequence;
            }
        }
        return null;
    }
}
