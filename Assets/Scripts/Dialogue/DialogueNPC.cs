using UnityEngine;
using EasyButtons;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 7)]
    private string[] sentences;
    [SerializeField]
    [Tooltip("Resembles the amount of letters per second")]
    private int textSpeed;
    public int TextSpeed { get { return textSpeed; } }
    public string[] Sentences {  get { return sentences; } }

    [Button]
    public void TriggerDialogue()
    {
        DialogueManager.Instance.EnterDialogue(this);
    }

    [Button]
    public void ExitDialogue()
    {
        DialogueManager.Instance.ExitDialogue();
    }
}
