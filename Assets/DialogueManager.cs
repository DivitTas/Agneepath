using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public RectTransform leftCharacter;
    public RectTransform rightCharacter;
    public TextMeshProUGUI dialogueText;

    public RectTransform dialogueBox;
    public string nextSceneName;

    void LateUpdate()
    {
        float textHeight = dialogueText.preferredHeight;
        dialogueBox.sizeDelta = new Vector2(dialogueBox.sizeDelta.x, textHeight + 80);
    }

    public string[] dialogueLines;
    public bool[] isLeftSpeaker;

    int currentLine = 0;

    Vector2 leftOnScreen = new Vector2(200, 0);
    Vector2 leftOffScreen = new Vector2(-600, 0);

    Vector2 rightOnScreen = new Vector2(-40, 0);
    Vector2 rightOffScreen = new Vector2(600, 0);

    float slideSpeed = 8f;

    void Start()
    {
        ShowLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                Debug.Log("Dialogue ended. Next scene name: " + nextSceneName);
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    Debug.Log("Loading scene: " + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogWarning("Next scene name is empty! Set it in the Inspector.");
                }
            }
        }

        AnimateCharacters();
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    void AnimateCharacters()
    {
        if (currentLine >= isLeftSpeaker.Length)
            return;

        if (isLeftSpeaker[currentLine])
        {
            leftCharacter.anchoredPosition =
                Vector2.Lerp(leftCharacter.anchoredPosition, leftOnScreen, Time.deltaTime * slideSpeed);

            rightCharacter.anchoredPosition =
                Vector2.Lerp(rightCharacter.anchoredPosition, rightOffScreen, Time.deltaTime * slideSpeed);
        }
        else
        {
            rightCharacter.anchoredPosition =
                Vector2.Lerp(rightCharacter.anchoredPosition, rightOnScreen, Time.deltaTime * slideSpeed);

            leftCharacter.anchoredPosition =
                Vector2.Lerp(leftCharacter.anchoredPosition, leftOffScreen, Time.deltaTime * slideSpeed);
        }
    }
}