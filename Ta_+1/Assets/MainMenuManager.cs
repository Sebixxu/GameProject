using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameClick()
    {
        levelLoader.LoadLevel("Level0");
    }

    public void ContinueClick()
    {
        //TODO
    }

    public void HighScoreClick()
    {
        //TODO
    }

    public void HelpClick()
    {
        //TODO
    }

    public void SettingsClick()
    {
        //TODO
    }

    public void CreditsClick()
    {
        //TODO
    }

    public void ExitClick()
    {
        Application.Quit(0);
    }
}
