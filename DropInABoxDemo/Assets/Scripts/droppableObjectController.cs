using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppableObjectController : MonoBehaviour {

    #region properties

    /// <summary>
    /// True if the object is near the player 
    /// </summary>
    bool grabbable = false;

    /// <summary>
    /// True if the object is grabbed by the player
    /// </summary>
    bool grabbed = false;

    /// <summary>
    /// The object type determines the color and the scoring value of each object
    /// </summary>
    [SerializeField]
    int objectType = 0;

    /// <summary>
    /// The player
    /// </summary>
    GameObject player ;

    #endregion

    #region Unity Methods
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (grabbable && Input.GetKeyDown(KeyCode.G))
        {
            grabbed = true;
        }

        if (grabbed && Input.GetKeyDown(KeyCode.R))
        {
            grabbed=false;
        }
	}

    void FixedUpdate()
    {
        //let the object follow the player
        if (grabbed)
        {
            Vector3 toPlayer = player.transform.position + new Vector3(0f, 2.2f, 0f); 
            transform.position=(toPlayer );
            gameObject.GetComponent<Rigidbody>().isKinematic = true;            
        }
        if (!grabbed)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            if(gameObject.transform.position.y < 1 )
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 11.0f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {        
            grabbable = true;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            grabbable = false;
        }            
    }

    #endregion


    #region Custom Methods
    /// <summary>
    /// Set the object "type";
    /// </summary>
    /// <param name="objectType"></param>
    public void setType(int oType)
    {
        this.objectType = oType;
    }
    #endregion

}
