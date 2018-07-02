using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    public bool grabbable = false;
    private bool grabbed = false;
    public Material[] material;
    private Renderer rend;

    /// <summary>
    /// The player
    /// </summary>
    GameObject player;



    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        
        //Highlight the object
        if (grabbable)
        {
            rend.sharedMaterial = material[1];
        }else
        {
            rend.sharedMaterial = material[0];
        }

        if (grabbable && Input.GetKeyDown(KeyCode.G))
        {
            Grab();
        }               
	}


    void FixedUpdate()
    {
        if (grabbed)
        {
            Vector3 toPlayer = player.transform.position + new Vector3(0f, 2.2f, 0f);
            transform.position = (toPlayer);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
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

    public void Grab()
    {
        grabbed = true;
    }
}
