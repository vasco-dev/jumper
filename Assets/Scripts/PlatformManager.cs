using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{    

    [SerializeField]
    private List<GameObject> _prefabs;

    private List<Platform> _platforms = new List<Platform>();

    private int _platformTotal = 0;

    private int _currentPlatform;


    public static PlatformManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
            Debug.LogError("not instance");
        }
    }

    private void SpawnPlatform()
    {
        Platform localPlat = GetRandomPlatform();
    }

    private Platform GetRandomPlatform()
    {
        int random = UnityEngine.Random.Range(0, 1 + _prefabs.Count);

        _prefabs[random].TryGetComponent<Platform>(out Platform plat);

        if(plat == null) {
            Debug.LogError("PREFAB IS NOT PLATFORM");
            return null;
        }

        return plat;
    }

}
