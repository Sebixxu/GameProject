using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPickerPool : Singleton<ClassPickerPool>
{
    public PlayerClassPickerModel[] PlayerClassPickerModels => playerClassPickerModels;
    [SerializeField] private PlayerClassPickerModel[] playerClassPickerModels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
