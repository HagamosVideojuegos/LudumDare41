using System.Collections;
using UnityEngine;

public class PlayerCollisionDetecter : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private AudioClip orangeAudio;

    [SerializeField]
    private PlaySound playSound;

    private CommandController commandController;

    private void Awake()
    {
        commandController = FindObjectOfType<CommandController>();
    }

    private void OnAnimatorMove()
    {
        if (Time.timeScale == 1 && animator.deltaPosition != Vector3.zero)
        {
            rigidbody2d.velocity = animator.deltaPosition / Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        commandController.SetAnimator(animator);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Enemy":
                animator.SetTrigger("Die");
                commandController.lives--;
                if (commandController.lives < 0) StartCoroutine(WaitToResetGame());
                else  StartCoroutine(WaitToResetLevel());
                collision.otherCollider.enabled = false;
                break;
            case "Goal":
                animator.SetTrigger("Win");
                StartCoroutine(WaitToNextLevel());
                collision.otherCollider.enabled = false;
                break;
            case "Orange":
                collision.collider.enabled = false;
                Destroy(collision.gameObject);
                playSound.Play(orangeAudio);
                commandController.addOrange(collision.collider.GetComponent<OrangeController>().index);
                break;
        }
    }

    private IEnumerator WaitToNextLevel()
    {
        yield return new WaitForSeconds(3f);
        commandController.NextLevel();
    }

    private IEnumerator WaitToResetLevel()
    {
        yield return new WaitForSeconds(3f);
        commandController.ResetLevel();
    }

    private IEnumerator WaitToResetGame()
    {
        yield return new WaitForSeconds(3f);
        commandController.ResetGame();
    }
}
