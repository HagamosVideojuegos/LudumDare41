using UnityEngine;

public class ChangeAnimation : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string animationName;

    private void Start()
    {
        animator.Play(animationName);
    }
}
