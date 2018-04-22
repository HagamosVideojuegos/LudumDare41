using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    [SerializeField]
    private Text endText;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Animator animator;

    CommandController commandController;

	private void Awake ()
    {
        commandController = FindObjectOfType<CommandController>();
    }

    private void Start()
    {
        StartCoroutine(StartEnd());
    }

    private IEnumerator StartEnd()
    {
        int score = Mathf.Clamp((((commandController.oranges + 1) * 10) * ((commandController.lives + 1) * 10)) + (Mathf.Clamp(240 - commandController.actions, 0, 240) * 10) - commandController.errors, 0 , int.MaxValue);
        if (commandController.oranges == 8)
        {
            Destroy(FindObjectOfType<InputField>());
        }

        yield return StartCoroutine(TypeText("> YOU HAVE GOT: \n"));
        yield return StartCoroutine(TypeText("> "));
        endText.text += string.Format("<color=cyan>{0} / 8</color>", commandController.oranges) ;
        yield return StartCoroutine(TypeText(" ORANGES\n"));
        yield return StartCoroutine(TypeText("> WITH "));
        endText.text += string.Format("<color=cyan>{0}</color>", commandController.lives);
        yield return StartCoroutine(TypeText(" LIVES\n"));
        yield return StartCoroutine(TypeText("> WITH "));
        endText.text += string.Format("<color=cyan>{0}</color>", commandController.actions);
        yield return StartCoroutine(TypeText(" TOTAL ACTIONS\n"));
        yield return StartCoroutine(TypeText("> WITH "));
        endText.text += string.Format("<color=cyan>{0}</color>", commandController.errors);
        yield return StartCoroutine(TypeText(" ERRORS TYPING\n"));
        yield return StartCoroutine(TypeText("> YOUR SCORE IS "));
        endText.text += string.Format("<b><color=cyan>{0}</color></b>", score);
        yield return StartCoroutine(TypeText("!"));

        yield return new WaitForSeconds(1f);

        animator.Play(commandController.oranges == 8 ? "Good" : "Bad");
    }

    private IEnumerator TypeText(string message)
    {
        foreach (char letter in message.ToCharArray())
        {
            endText.text += letter;
            audioSource.pitch = Random.Range(.8f, 1.2f);
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
        }

        
    }

    public void RestartGame()
    {
        commandController.ResetGame();
    }
}
