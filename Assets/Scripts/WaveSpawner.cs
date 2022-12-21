using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Photon.Pun;

public class WaveSpawner : MonoBehaviour
{
    private static WaveSpawner instance;
    public static WaveSpawner Instance    
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WaveSpawner>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //[SerializeField]
    //private UIManager uiManager;

    public List<GameObject> spawnedEnemy = new List<GameObject>();
    public Transform[] spawningPoints = new Transform[0];

    //[SerializeField]
    //private Transform ennemyPrefab;

    //[SerializeField]
    //private Transform spawnPoint;

    public GameObject particule;

    [SerializeField]
    private float TimeBetweenWaves = 5f;

    private float countdown = 5f;

    public int waveIndex = 0;

    public int EnemiesAlive = 0;

    //public PhotonView view;

private void Start()
    {
       ////if(PhotonNetwork.IsMasterClient == true)
       //// {
       //     gameObject.SetActive(true);
       //// }
       // else
       // {
       //     gameObject.SetActive(false);
       //     gameObject.tag = "Untagged";
       // }
        //uiManager = GameObject.FindObjectOfType<UIManager>();
        GameObject[] _tempSpawn = GameObject.FindGameObjectsWithTag("Spawner");
        for(int i = 0; i < _tempSpawn.Length; i++)
        {
            spawningPoints[i] = _tempSpawn[i].transform;
        }
        //view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive > 0)
        {
//            Debug.Log(EnemiesAlive);
            return;
        }
        else 
        {
            EnemyController.isMove = false;
            //view.RPC("showProgressBarWave", RpcTarget.All);
            //uiManager.ShowProgressBarWave(true);
        }

        if (countdown <= 0f)
        {
            //view.RPC("startCoroutine", RpcTarget.All);
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWaves;
            //return;
        }
        countdown -= Time.deltaTime;
        Debug.Log("timer = " + countdown/TimeBetweenWaves);

        //uiManager.UpdateProgressBarWave(countdown/TimeBetweenWaves);
    }

    //[PunRPC]
    public void showProgressBarWave(){
        //if (PhotonNetwork.IsMasterClient == true){
            //uiManager.ShowProgressBarWave(true);
        //}
    }

    //[PunRPC]
    //public void startCoroutine(){
    //    StartCoroutine("SpawnWave");
    //}

    IEnumerator SpawnWave()
    {
        waveIndex++;
        //uiManager.UpdateWaveText();
        //uiManager.ShowProgressBarWave(false);

        for ( int i = 0; i < waveIndex; i++)
        {
            //Instantiate(particule, spawnPoint.position, spawnPoint.rotation);
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
            EnemyController.isMove = true;
            
        }

    }

    void SpawnEnemy()
    {
        int random = Random.Range(0, spawnedEnemy.Count);
        int random1 = Random.Range(0, spawningPoints.Length);
        Instantiate(spawnedEnemy[random], spawningPoints[random1].transform.position, spawningPoints[random1].rotation);

        EnemiesAlive++;
    }
}
