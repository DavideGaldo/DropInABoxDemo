
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableObject : MonoBehaviour {

    #region properties

    /// <summary>
    /// True if the object is near the player 
    /// </summary>
    bool grabbable = false;

    /// <summary>
    /// True if the object is near some DropBox
    /// </summary>
    bool droppable = false;

    /// <summary>
    /// If the object has been dropped in the correct dropBox this property is set to true
    /// </summary>
    bool dropped = false;

    /// <summary>
    /// True if the object is grabbed by the player
    /// </summary>
    bool grabbed = false;

    /// <summary>
    /// The object type determines the color and the scoring value of each droppable object
    /// </summary>
    [SerializeField]
    int objectType = 0;

    [SerializeField]
    int healthModifier = 10;

    /// <summary>
    /// The player
    /// </summary>
    GameObject player ;

    /// <summary>
    /// Drop Box in which the user can drop the object into
    /// </summary>
    GameObject dropBox;

    #endregion

    #region Unity Methods
    void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        //If the object has not been dropped in the box
        if(!dropped)
        {
            if (grabbable && Input.GetKeyDown(KeyCode.G))
            {
                grabbed = true;
            }

            if (grabbed && Input.GetKeyDown(KeyCode.R))
            {
                grabbed = false;
            }
        }
	}

    void FixedUpdate()
    {

        if (!dropped)
        {
            //let the object follow the player
            if (grabbed)
            {
                Vector3 toPlayer = player.transform.position + new Vector3(0f, 2.2f, 0f);
                transform.position = (toPlayer);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else //!grabbed
            {
                //let the object flow if is not grabbed by the player nor dropped in the correct dropBox
                if (!droppable)
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    if (gameObject.transform.position.y < 1)
                    {
                        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 11.0f);
                    }
                }
                else
                {
                    //The box can accept this kind of object
                    if (dropBox != null && dropBox.GetComponent<DropBox>().GetBoxType() == objectType )
                    {
                        gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        Drop();
                        Vector3 pos = dropBox.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                        transform.position=pos;
                    }else if(dropBox != null && dropBox.GetComponent<DropBox>().GetBoxType() != objectType){
                        //randomly trow away the object
                        gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        Vector3 randPos = new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(20.0f, 40.0f), Random.Range(-30.0f, 30.0f));
                        gameObject.GetComponent<Rigidbody>().AddForce(randPos);
                    }
                }
            }
        }            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {        
            grabbable = true;            
        }

        if (other.gameObject.tag == "DropBox")
        {
            droppable = true;
            dropBox = other.gameObject;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            grabbable = false;
        }

        if (other.gameObject.tag == "DropBox")
        {
            droppable = false;
            dropBox = null;
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

    /// <summary>
    /// Return the grabbed state of the object, if the player holds it return true
    /// </summary>
    public bool IsGrabbed()
    {
        return grabbed;
    }

    public void Drop()
    {
        dropped = true;     //The object has been dropped
        grabbable = false;  //The user can't grab this object anymore
        player.GetComponent<PlayerStats>().ModifyHealth(healthModifier);
    }

    public bool IsDropped()
    {
        return dropped;
    }


    #endregion


}
