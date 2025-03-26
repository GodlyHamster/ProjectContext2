using UnityEngine;
using UnityEngine.Events;

public class QuestListener : MonoBehaviour
{
    [SerializeField]
    private QuestData questStepTrigger;

    public UnityEvent eventTriggered = new UnityEvent();

    private void Start()
    {
        CheckQuestTrigger(questStepTrigger.name);
        QuestManager.instance.OnQuestUpdated.AddListener(CheckQuestTrigger);
    }

    private void CheckQuestTrigger(string name)
    {
        if (QuestManager.instance.GetQuestData(name) == null) return;

        if (QuestManager.instance.GetQuestData(name).state == questStepTrigger.state && QuestManager.instance.GetQuestData(name).step == questStepTrigger.step)
        {
            Debug.Log("yeah it works here boss)");
            eventTriggered.Invoke();
        }
    }
}
