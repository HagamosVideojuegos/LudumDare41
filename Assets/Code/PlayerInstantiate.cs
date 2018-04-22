using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        var playerInstantiate = Instantiate(player);
        playerInstantiate.transform.parent = transform.parent;
        playerInstantiate.transform.localPosition = transform.localPosition;
        Destroy(gameObject);
    }
}
