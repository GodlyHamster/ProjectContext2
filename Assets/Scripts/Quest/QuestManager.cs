using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private Dictionary<string, QuestData> questDataList;

    private void Awake()
    {
        if (instance != null)
        {
            questDataList = instance.questDataList;
            Destroy(instance.gameObject);
        }
        else
        {
            questDataList = new Dictionary<string, QuestData>();
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (questDataList.Count > 0)
        {
            Debug.Log($"first quest in list is {questDataList.First().Value.name} quest");
        }
    }

    public void AddQuest(QuestData questData)
    {
        if (this.questDataList.ContainsKey(questData.name)) return;
        questDataList.Add(questData.name, questData);
        Debug.Log($"Added {questData.name} quest to list");
    }

    public QuestData GetQuestData(string questName)
    {
        return questDataList.ContainsKey(questName) ? questDataList[questName] : null;
    }

    public void NextStep(string questName)
    {
        questDataList[questName].step += 1;
    }
}
