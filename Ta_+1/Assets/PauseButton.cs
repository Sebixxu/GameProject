using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseBlockImage;

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
                ResumeGame();
                break;
            case true:
                PauseGame();
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

    public void RevertPauseBlockImageState()
    {
        pauseBlockImage.SetActive(!pauseBlockImage.activeSelf);
    }
}
