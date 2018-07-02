using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //Contain true 
    public bool[] instantiatedBoxes;
    public Rigidbody[] dropBoxesPrefab;
    public Rigidbody[] droppableObjectsPrefab;
    public Transform spawner;

    float timer = 0.0f;
    public float droppableObjectSpawnTimer = 3.0f;
    public float levelUpTimer = 15.0f;

    private float remainingTimeForDroppableObjectSpanw;
    private float remainingTimeForlevelUp;

    public int objectTypesNumber = 8;
    private int instantiatedTypes = 0;


    





	// Use this for initialization
	void Start () {
        if(dropBoxesPrefab.Length!= droppableObjectsPrefab.Length)
        {
            Debug.LogError("Error! DropBox types aren't equal to Droppable Object types !");
        }        

        remainingTimeForlevelUp = levelUpTimer;
        remainingTimeForDroppableObjectSpanw = droppableObjectSpawnTimer;
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        remainingTimeForlevelUp -= Time.deltaTime;
        if (remainingTimeForlevelUp <= 0)
        {
            remainingTimeForlevelUp = levelUpTimer;
            if (instantiatedTypes < objectTypesNumber -1)
            {
                instantiatedTypes++;
                //spawn new DropBox
                SpawnDropBox(instantiatedTypes);
            }
        }

        remainingTimeForDroppableObjectSpanw -= Time.deltaTime;
        if(remainingTimeForDroppableObjectSpanw <= 0)
        {
            remainingTimeForDroppableObjectSpanw = droppableObjectSpawnTimer;
            int spawType = Mathf.RoundToInt(Random.Range(0.0f, instantiatedTypes));
            SpawnDroppableObject(spawType);
        }


	}

    void SpawnDroppableObject(int type)
    {
        Rigidbody droppableObjInstance;
        Vector3 randPos = new Vector3(Random.Range(-10.0f, 10.0f), 5.0f, Random.Range(-10.0f, 10.0f));
        droppableObjInstance= Instantiate(droppableObjectsPrefab[type], spawner.position + randPos, spawner.rotation) as Rigidbody;
    }

    void SpawnDropBox(int type)
    {
        if (!instantiatedBoxes[type])
        {
            Rigidbody dropBoxInstance;
            Vector3 randPos = new Vector3(Random.Range(-10.0f, 10.0f), 5.0f, Random.Range(-10.0f, 10.0f));
            dropBoxInstance = Instantiate(dropBoxesPrefab[type], spawner.position+randPos, spawner.rotation ) as Rigidbody;
            instantiatedBoxes[type] = true;
        }
            
    }
}
