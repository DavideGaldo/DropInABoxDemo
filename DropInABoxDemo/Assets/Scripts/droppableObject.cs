using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppableObject : MonoBehaviour {

    #region properties

    /// <summary>
    /// True if the object is near the player 
    /// </summary>
    [SerializeField]
    bool grabbable = false;

    /// <summary>
    /// True if the object is grabbed by the player
    /// </summary>
    ///     [SerializeField]
    bool grabbed = false;

    [SerializeField]
    float floatHeight = 1f;

    /// <summary>
    /// Il player che afferra l'oggetto
    /// </summary>
    private GameObject player;

    #endregion



    #region Unity Methods

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
            Vector3 toPlayer = player.transform.position + new Vector3(0f,2f,0f) - transform.position;
            float speed = 4f;
            transform.Translate(toPlayer * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {        
            grabbable = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            grabbable = false;
        }

        if (!grabbed)
            player = null;
    }

    #endregion


    #region Custom Methods
    /// <summary>
    /// Richiamato quando afferro un oggetto
    /// </summary>
    public void Grab()
    {

    }
    
    /// <summary>
    /// Metodo richiamato quando rilascio un oggetto
    /// </summary>
    public void Drop()
    {

    }
    #endregion

}
