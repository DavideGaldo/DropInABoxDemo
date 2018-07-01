using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBoxController : MonoBehaviour {


    #region properties


    GameObject player;
    /// <summary>
    /// The object type determines what droppable object this dropBox will accept
    /// </summary>
    [SerializeField]
    int objectType = 0;

    /// <summary>
    /// The dropBox can evaluate the droppable object
    /// </summary>
    bool dropObjectEvaluable = false;

    #endregion

    #region Unity Methods

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }


	// Update is called once per frame
	void FixedUpdate () {

	}

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "DroppableObject" && !other.gameObject.GetComponent<droppableObjectController>().IsGrabbed())
    //    {
    //        Vector3 pos = gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
    //        other.gameObject.GetComponent<Rigidbody>().transform.Translate(pos * Time.deltaTime);
    //    }
    //}



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
    /// Return the type of dropableObject accepted
    /// </summary>
    /// <returns></returns>
    public int GetBoxType()
    {
        return objectType;
    }

    #endregion


}
