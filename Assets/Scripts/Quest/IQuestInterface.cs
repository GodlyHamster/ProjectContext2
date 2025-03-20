using UnityEngine;

public interface IQuestInterface
{
    public QuestState currentQuestState { get; }

    public void OnStartQuest();
    public void OnQuestStepComplete(int step);
    public void OnCompleteQuest();
}
