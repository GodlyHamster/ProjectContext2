using System;
using UnityEngine;

[Serializable]
public class DialogueSequence
{
    [Tooltip("What state this dialogue will be played in")]
    public QuestState state;
    [Tooltip("Is only used when the state is STARTED")]
    public int stepNumber;
    public DialogueText[] sentences;
}
