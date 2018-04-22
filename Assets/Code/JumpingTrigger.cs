using UnityEngine;

public class JumpingTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Floor"))
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Down", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Floor")) animator.SetBool("Down", true);
    }
}
