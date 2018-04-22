using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Animator helpAnimator;

    [SerializeField]
    private Animator creditsAnimator;

    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text orangesText;

    public GameObject currentLevel;

    public int lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            livesText.text = "X" + _lives.ToString();
        }
    }

    private int _lives;

    public int oranges
    {
        get
        {
            int[] array = new int[1];
            _oranges.CopyTo(array, 0);
            return _oranges.OfType<bool>().Count(p => p);
        }

        set
        {
            _oranges = new BitArray(new int[] { value });
        }
    }

    private BitArray _oranges = new BitArray(new int[] { 0 });

    public int actions;

    public int errors;

    public void addOrange(int index)
    {
         _oranges[index] = true;
        orangesText.text = oranges.ToString() + "/8";
    }

    public bool isOrangeGetted(int index)
    {
        return _oranges[index];
    }

    public void NextLevel()
    {
        LoadLevel("Level" + (int.Parse(currentLevel.name.Substring(5).Replace("(Clone)", "")) + 1));
    }

    public void ResetLevel()
    {
        LoadLevel(currentLevel.name.Replace("(Clone)", ""));
    }

    public void ResetGame()
    {
        lives = 3;
        oranges = 0;
        actions = 0;
        errors = 0;
        LoadLevel("Level0");
    }

    public void SetAnimator(Animator animator)
    {
        this.playerAnimator = animator;
    }

    public void SetMayus(string command)
    {
        inputField.text = command.ToUpper();
    }

    public void SendCommand(string command)
    {
        switch(command)
        {
            case "HELP":
                Time.timeScale = 0;
                helpAnimator.ResetTrigger("Hide");
                helpAnimator.SetTrigger("Show");
                break;
            case "START":
                StartGame();
                break;
            case "CLOSE":
                Time.timeScale = 1;
                helpAnimator.ResetTrigger("Show");
                helpAnimator.SetTrigger("Hide");
                creditsAnimator.ResetTrigger("Show");
                creditsAnimator.SetTrigger("Hide");
                break;
            case "LOAD":
                LoadGame();
                break;
            case "SAVE":
                SaveGame();
                break;
            case "CREDITS":
                Time.timeScale = 0;
                creditsAnimator.ResetTrigger("Hide");
                creditsAnimator.SetTrigger("Show");
                break;
            default:
                if (playerAnimator != null && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    switch (command)
                    {
                        case "JUMP":
                            if (!playerAnimator.GetBool("Down"))
                            {
                                actions++;
                                playerAnimator.SetTrigger("Jump");
                            }
                            break;
                        case "WALK":
                            actions++;
                            playerAnimator.SetTrigger("Walk");
                            break;
                        case "ROTATE":
                            actions++;
                            playerAnimator.transform.localScale = new Vector3(playerAnimator.transform.localScale.x * -1, 1, 1);
                            break;
                        case "IMPULSE":
                            actions++;
                            playerAnimator.SetTrigger("Impulse");
                            break;
                        default:
                            errors++;
                            playerAnimator.SetTrigger("Wait");
                            break;
                    }
                }
                break;
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void OnMouseUp()
    {
        inputField.ActivateInputField();
    }

    private void Start()
    {
        if (currentLevel == null)
        {
            LoadLevel("Level0");
        }
        else
        {
            lives = 100;
        }
    }

    private void SaveGame()
    {
        int[] array = new int[1];
        _oranges.CopyTo(array, 0);
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.SetInt("oranges", array[0]);
        PlayerPrefs.SetInt("actions", actions);
        PlayerPrefs.SetInt("errors", errors);
        PlayerPrefs.SetString("level", currentLevel.name.Replace("(Clone)", ""));
    }

    private void LoadGame()
    {
        lives = PlayerPrefs.GetInt("lives", 3);
        oranges = PlayerPrefs.GetInt("oranges", 0);
        PlayerPrefs.SetInt("actions", 0);
        LoadLevel(PlayerPrefs.GetString("level", "level1"));
    }

    private void StartGame()
    {
        PlayerPrefs.DeleteAll();
        lives = 3;
        oranges = 0;
        actions = 0;
        errors = 0;
        LoadLevel("Level1");
    }

    private void LoadLevel(string levelName)
    {
        orangesText.text = oranges + "/8";
        livesText.text = "X" + _lives.ToString();
        GameObject level = Resources.Load<GameObject>(levelName);
        DestroyImmediate(currentLevel);
        if(level != null) currentLevel = Instantiate(level);
    }
}
