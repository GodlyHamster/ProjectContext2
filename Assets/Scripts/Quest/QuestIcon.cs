using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] icons;
    [SerializeField]
    private QuestNPC npcParent;

    private QuestState currentState;


    private void Start()
    {
        if (npcParent == null)
        {
            npcParent = GetComponentInParent<QuestNPC>();
        }
        npcParent.OnQuestStateUpdated.AddListener(UpdateState);

        UpdateState(npcParent.currentQuestState);
    }

    public void UpdateState(QuestState state)
    {
        currentState = state;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].SetActive(false);
        }
        icons[(int)currentState].SetActive(true);
    }
}
