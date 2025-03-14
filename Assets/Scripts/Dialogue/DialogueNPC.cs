using UnityEngine;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private DialogueText[] sentences;
    [SerializeField]
    [Tooltip("Resembles the amount of letters per second")]
    private int textSpeed;
    public int TextSpeed { get { return textSpeed; } }
    public DialogueText[] Sentences {  get { return sentences; } }

    private bool currentlyInDialogue = false;

    private void Start()
    {
        DialogueManager.Instance.OnEndDialogue.AddListener(DialogueEnded);
    }

    [ContextMenu("Trigger Dialogue")]
    public void TriggerDialogue()
    {
        DialogueManager.Instance.EnterDialogue(this);
        currentlyInDialogue = true;
    }

    [ContextMenu("Exit Dialogue")]
    public void ExitDialogue()
    {
        DialogueManager.Instance.ExitDialogue();
    }

    private void DialogueEnded()
    {
        currentlyInDialogue = false;
    }

    public void OnInteract()
    {
        if (currentlyInDialogue) return;
        TriggerDialogue();
    }
}
