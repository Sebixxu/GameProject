using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private ChangeableImageButton _changeableImageButton;

    // Start is called before the first frame update
    void Awake()
    {
        _changeableImageButton = GetComponent<ChangeableImageButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformClick()
    {
        switch (_changeableImageButton.IsDefaultState)
        {
            case false:
                GameManager.Instance.ResumeGame();
                break;
            case true:
                GameManager.Instance.PauseGame();
                break;
        }

        _changeableImageButton.SwitchButtonSprites();
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
