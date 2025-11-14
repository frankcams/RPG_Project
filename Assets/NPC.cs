using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName = "Villager";
    [TextArea(3, 5)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isTalking = false;

    public void Interact()
    {
        if (!isTalking)
        {
            currentLine = 0;
            isTalking = true;
            ShowDialogue();
        }
        else
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
            {
                ShowDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void ShowDialogue()
    {
        Debug.Log($"{npcName}: {dialogueLines[currentLine]}");
        // TODO: Connect to your UI system
    }

    private void EndDialogue()
    {
        isTalking = false;
        Debug.Log($"{npcName} has finished talking.");
        // TODO: Hide dialogue UI
    }
}