using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region Singleton

    public static Player Instance;

    private new void Awake()
    {
        base.Awake();

        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of OreStorage!");
            return;
        }

        Instance = this;
    }

    #endregion

    public float PlayerRange
    {
        get { return playerRange; }
        set { playerRange = value; }
    }

    [SerializeField] 
    private float playerRange; //Defines transform.scale in each direction 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayerRange()
    {
        var rangeGameObject = transform.Find("Range");
        rangeGameObject.GetComponent<SpriteRenderer>().enabled = true;
        rangeGameObject.transform.localScale = new Vector3(playerRange * 2, playerRange * 2, 1);
    }
}
