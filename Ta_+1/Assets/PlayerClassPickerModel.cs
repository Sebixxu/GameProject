using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerClassPickerModel //TODO Zastanowić się czy nie zrezygnować z tego modelu na rzecz dostarczania prefabów danych klas.
{
    public int id;
    public bool isAvailable;
    public Sprite icon;
    public Sprite clickedIcon;
    public string name;
    public string description;
    public FullStatistics playerStatistics;
}
