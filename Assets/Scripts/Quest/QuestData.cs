using System;

[Serializable]
public class QuestData
{
    public string name;
    public QuestState state = QuestState.CAN_START;
    public int step = 0;
}
