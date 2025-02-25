using System;
using UnityEngine;

[Serializable]
public class DialogueText
{
    [TextArea(3, 5)]
    public string text;

    public DialogueOption[] options;
}
