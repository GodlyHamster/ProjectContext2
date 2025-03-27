using UnityEngine;
using UnityEngine.Events;

public class QuestListener : MonoBehaviour
{
    [SerializeField]
    private QuestData questStepTrigger;
    [SerializeField]
    private bool triggerAfterCompletion;

    public UnityEvent eventTriggered = new UnityEvent();

    private void Start()
    {
        CheckQuestTrigger(questStepTrigger.name);
        QuestManager.instance.OnQuestUpdated.AddListener(CheckQuestTrigger);
    }

    private void CheckQuestTrigger(string name)
    {
        QuestData data = QuestManager.instance.GetQuestData(name);
        if (data == null) return;
        if (name != questStepTrigger.name) return;

        if (questStepTrigger.state == data.state && questStepTrigger.step == data.step)
        {
            eventTriggered.Invoke();
            return;
        }
        if (triggerAfterCompletion)
        {
            if (data.state > questStepTrigger.state)
            {
                eventTriggered.Invoke();
                return;
            }
            if (questStepTrigger.state == QuestState.STARTED && data.state == QuestState.STARTED)
            {
                if (data.step >= questStepTrigger.step)
                {
                    eventTriggered.Invoke();
                    return;
                }
            }
        }
    }
}