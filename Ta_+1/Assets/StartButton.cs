using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisableButton();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableButton()
    {
        GetComponent<Button>().interactable = true;
        GetComponent<Image>().color = Color.white;
    }

    public void DisableButton()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = Color.gray;
    }
}
