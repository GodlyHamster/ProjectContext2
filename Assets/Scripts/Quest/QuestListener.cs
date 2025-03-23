using UnityEngine;
using UnityEngine.Events;

public class QuestListener : MonoBehaviour
{
    [SerializeField]
    private QuestData questStepTrigger;

    private QuestManager questManager;

    public UnityEvent eventTriggered = new UnityEvent();

    private void Start()
    {
        questManager = QuestManager.instance;
        CheckQuestTrigger();
    }

    private void CheckQuestTrigger()
    {
        if (questManager.GetQuestData(questStepTrigger.name).state == questStepTrigger.state && questManager.GetQuestData(questStepTrigger.name).step == questStepTrigger.step)
        {
            Debug.Log("yeah it works here boss)");
            eventTriggered.Invoke();
        }
    }
}
