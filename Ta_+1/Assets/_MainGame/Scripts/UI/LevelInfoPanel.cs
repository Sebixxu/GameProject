using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPanel : Singleton<LevelInfoPanel>
{
    [SerializeField] private Text incomingValueText;
    [SerializeField] private Text passedValueText;

    // Start is called before the first frame update
    void Start()
    {
        passedValueText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIncomingValue(int incomingValue)
    {
        incomingValueText.text = incomingValue.ToString();
    }

    public void SetPassedValue(int passedValue)
    {
        incomingValueText.text = passedValue.ToString();
    }

    public void DecreaseIncomingValue()
    {
        var currentValue = int.Parse(incomingValueText.text);
        currentValue--;

        incomingValueText.text = currentValue.ToString();
    }

    public void IncreasePassedValue()
    {
        var currentValue = int.Parse(passedValueText.text);
        currentValue++;

        passedValueText.text = currentValue.ToString();
    }
}
