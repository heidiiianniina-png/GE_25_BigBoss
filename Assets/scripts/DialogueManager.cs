using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private string[] lines;
    private int index = 0;
    private bool typing = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        // Press Enter to go to next line
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;

        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine(lines[index]));
    }

    System.Collections.IEnumerator TypeLine(string line)
    {
        typing = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        typing = false;
    }

    public void NextLine()
    {
        if (typing) return;

        index++;

        if (index < lines.Length)
            ShowLine();
        else
            EndDialogue();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
