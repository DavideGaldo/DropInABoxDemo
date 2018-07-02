using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int PlayerHealthPenality = 1;
    private float remainingTimePlayerDamaging = 1;

    private GameObject player;


    public Text healthText;
    public Text gameOverText;
    public Text scoreText;


    private bool playerIsDeath = false;



    





	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");
        gameOverText.enabled = false;
        scoreText.enabled = false;



        if (dropBoxesPrefab.Length!= droppableObjectsPrefab.Length)
        {
            Debug.LogError("Error! DropBox types aren't equal to Droppable Object types !");
        }        

        remainingTimeForlevelUp = levelUpTimer;
        remainingTimeForDroppableObjectSpanw = droppableObjectSpawnTimer;




	}
	
	// Update is called once per frame
	void Update () {

        if (!playerIsDeath)
        {
            //timer += Time.deltaTime;

            #region Leveling Logic
            remainingTimeForlevelUp -= Time.deltaTime;
            if (remainingTimeForlevelUp <= 0)
            {
                remainingTimeForlevelUp = levelUpTimer;
                PlayerHealthPenality += 2;
                if (instantiatedTypes < objectTypesNumber - 1)
                {
                    instantiatedTypes++;
                    //spawn new DropBox
                    SpawnDropBox(instantiatedTypes);
                }
            }
            #endregion

            #region Droppable Object Spawning
            remainingTimeForDroppableObjectSpanw -= Time.deltaTime;
            if (remainingTimeForDroppableObjectSpanw <= 0)
            {
                remainingTimeForDroppableObjectSpanw = droppableObjectSpawnTimer;
                int spawType = Mathf.RoundToInt(Random.Range(0.0f, instantiatedTypes));
                SpawnDroppableObject(spawType);
            }
            #endregion

            #region Player Health Penality

            remainingTimePlayerDamaging -= Time.deltaTime;
            if (remainingTimePlayerDamaging <= 0)
            {
                remainingTimePlayerDamaging = 1;
                player.GetComponent<PlayerStats>().TakeDamage(PlayerHealthPenality);
            }

            #endregion

            UpdateUI();


        }

        
        



    }

    void FixedUpdate()
    {
        if (!playerIsDeath)
        {
            timer += Time.deltaTime;
        }
    }

    void SpawnDroppableObject(int type)
    {
        Rigidbody droppableObjInstance;
        Vector3 randPos = new Vector3(Random.Range(-13.0f, 13.0f), 0.0f, Random.Range(-13.0f, 13.0f));
        droppableObjInstance= Instantiate(droppableObjectsPrefab[type], spawner.position + randPos, spawner.rotation) as Rigidbody;
    }

    void SpawnDropBox(int type)
    {
        if (!instantiatedBoxes[type])
        {
            Rigidbody dropBoxInstance;
            Vector3 randPos = new Vector3(Random.Range(-12.0f, 12.0f), 0.0f, Random.Range(-12.0f, 12.0f));
            dropBoxInstance = Instantiate(dropBoxesPrefab[type], spawner.position+randPos, spawner.rotation ) as Rigidbody;
            instantiatedBoxes[type] = true;
        }
            
    }

    void UpdateUI()
    {
        int currentPlayerHealth = player.GetComponent<PlayerStats>().GetHealth(); ;
        if (currentPlayerHealth > 0)
        {
            healthText.text = "Health: " + currentPlayerHealth;
        }
    }

    public void GameOver()
    {
        playerIsDeath = true;
        float score = timer;
        scoreText.text = "You survived for: " + score + "seconds";
        gameOverText.enabled = true;
        scoreText.enabled = true;
        
    }
}
