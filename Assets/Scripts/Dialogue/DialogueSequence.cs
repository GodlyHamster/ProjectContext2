using System;
using UnityEngine;

[Serializable]
public class DialogueSequence
{
    [Tooltip("Use this to label quest progression: Can_Start, Step_3, Finished, etc.")]
    public string name;
    public DialogueText[] sentences;
}
