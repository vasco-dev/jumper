using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("DEBUG")]

    [SerializeField]
    public bool SpawnPlatNow = false;

    [SerializeField]
    public bool DestroyAllPlatsNow = false;

    [Header("Spawn Coordinates")]

    [SerializeField]
    private float _maxCoordY = 5f;
    [SerializeField]
    private float _leftCoordX = -2f;
    [SerializeField]
    private float _rightCoordX = 2f;
    [SerializeField]
    private float _maxCoordOffset = 0.5f;
    [SerializeField]
    private float _deathWallOffset = 4f;


    [Header("References")]



    [SerializeField]
    private List<GameObject> _platformPrefabs;

    private List<Platform> _platforms = new List<Platform>();

    [SerializeField]
    private int _maxBurstPlatformTotal = 100;

    private int _platformTotal = 0;

    private int _currentPlatform = 0;


    // last platform the player landed on
    public Platform CurrentCheckpoint { get; private set; } = null;

    public static PlatformManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            Debug.LogError("not instance");
        }
    }

    private void Update()
    {

        // debug
        if (SpawnPlatNow) {
            SpawnPlatNow = false;
            SpawnPlatform();
        }

        if (DestroyAllPlatsNow) {
            DestroyAllPlatsNow = false;
            DestroyAllPlatforms();
        }


    }

    /// <summary>
    /// spawns a random platform obtained from GetRandomPlatform then adds it to the list
    /// and sets it's position and index
    /// </summary>
    private void SpawnPlatform()
    {
        // instantiate a random platform and store into spawnedPlat
        Platform spawnedPlat = Instantiate(GetRandomPlatform());

        // set the platform's index
        spawnedPlat.SetIndex(_platformTotal);

        // set if the platform should be left or right
        // if it is even number it is on the right
        if (_platformTotal % 2 == 0) {
            spawnedPlat.SetIsRight(true);
        }

        // insert the spawned platform into its correct index
        _platforms.Insert(spawnedPlat.Index, spawnedPlat);


        // platform Y position and add offset
        float posY = (1 + _currentPlatform) * _maxCoordY;
        posY += UnityEngine.Random.Range(-_maxCoordOffset, _maxCoordOffset);

        // set the X position based on which side the platform is on and add offset
        float posX = spawnedPlat.IsRight ? _rightCoordX : _leftCoordX;
        posX += UnityEngine.Random.Range(-_maxCoordOffset, _maxCoordOffset);

        //set the transform position of the spawned platform
        spawnedPlat.transform.position = new(posX, posY, 0);

        // iterate the total number of platforms and the index of the current
        ++_platformTotal;
        ++_currentPlatform;

    }
    /// <summary>
    /// destroys every platform and resets the list and other variables
    /// </summary>

    private void DestroyAllPlatforms() {
        foreach (Platform platform in _platforms) {
            Destroy(platform.gameObject);
        }
        _platformTotal = 0;
        _currentPlatform = 0;
        _platforms.Clear();
    }
    private void BurstSpawnPlatforms()
    {
        do{
            SpawnPlatform();
        } while (_platformTotal < _maxBurstPlatformTotal);
    }


    /// <summary>
    /// randomizes a platform from the list of prefabs and returns it
    /// </summary>
    /// <returns> a random platform from the list of prefabs</returns>
    private Platform GetRandomPlatform()
    {
        int random = UnityEngine.Random.Range(0, _platformPrefabs.Count);

        _platformPrefabs[random].TryGetComponent<Platform>(out Platform plat);

        if (plat == null) {
            Debug.LogError("PREFAB IS NOT PLATFORM");
            return null;
        }

        return plat;
    }

    public void SetCheckpoint(Platform newCheckpoint)
    {
        CurrentCheckpoint = newCheckpoint;
        //DeathWall.Instance.transform.position = _deathWallOffset * Vector3.down + newCheckpoint.transform.position;
    }

    public void StartNew()
    {
        DestroyAllPlatforms();
        BurstSpawnPlatforms();
        CurrentCheckpoint = null;
    }
}
