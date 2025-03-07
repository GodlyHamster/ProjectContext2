using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueText
{
    [TextArea(3, 5)]
    public string text;

    public DialogueOption[] options;
    public int textSpeed = 20;

    public UnityEvent OnDialogueEvent = new UnityEvent();
}
