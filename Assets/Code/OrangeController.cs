using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeController : MonoBehaviour {

    public int index;

    private CommandController commandController;

    private void Awake()
    {
        commandController = FindObjectOfType<CommandController>();
        if (commandController.isOrangeGetted(index)) Destroy(gameObject);
    }
}
