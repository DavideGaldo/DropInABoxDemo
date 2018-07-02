using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour {


    #region properties


    GameObject player;

    /// <summary>
    /// The object type determines what droppable object this dropBox will accept
    /// </summary>
    [SerializeField]
    int objectType = 0;



    #endregion

    #region Unity Methods

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    #endregion

    #region Custom Methods
    
    /// <summary>
    /// Set the object "type";
    /// </summary>    
    public void SetBoxType(int oType)
    {
        this.objectType = oType;
    }

    /// <summary>
    /// Returns the type of dropableObject accepted
    /// </summary>
    public int GetBoxType()
    {
        return objectType;
    }

    #endregion


}
