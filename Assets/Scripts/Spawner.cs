using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public GameObject MeteorPrefab;
    public GameObject DiamondPrefab;
    GameObject ChosenBlock;
    public float secondsBetweenSpawns = 1;
    float nextSpawntime;
    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;
    public int DiamondRarity;
    [Tooltip("Higher number == more rare")]
    Vector2 screenHalfSizeWorldUnits;
    [Space]
    public bool FreezeActivated = false;
    public bool StartSpawning = false;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartSpawning && !FreezeActivated)
        {
            if (Time.time > nextSpawntime)
            {
                nextSpawntime = Time.time + secondsBetweenSpawns;
                float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
                float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
                Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), transform.position.y + spawnSize / 2f);

                if (Random.Range(0, DiamondRarity) == 0)
                {
                    ChosenBlock = DiamondPrefab;
                }
                else
                {
                    ChosenBlock = MeteorPrefab;
                }
                GameObject FallingPrefab = (GameObject)Instantiate(ChosenBlock, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
                FallingPrefab.transform.localScale = Vector2.one * spawnSize;
            }
        }

    }
}
