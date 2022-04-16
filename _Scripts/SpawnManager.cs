using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> trackPrefabList = new List<GameObject>();

    [SerializeField] private Transform player;

    [Header("Track Spawn Stuffs")]
    public float spawnDistance;
    public float trackDistance;
    private Vector3 trackSpawnPosition;

    private void Start()
    {
        trackSpawnPosition = new Vector3(player.position.x + spawnDistance, 0, 0);
    }

    private void Update()
    {
        float distance = trackSpawnPosition.x - player.position.x;
        if (distance < spawnDistance)
        {
            //Spawn new track
            SpawnTrack();

            //Add distance
            trackSpawnPosition = new Vector3(trackSpawnPosition.x + trackDistance, trackSpawnPosition.y, trackSpawnPosition.z);
        }
    }

    private void SpawnTrack()
    {
        int index = Random.Range(0, trackPrefabList.Count);
        Instantiate(trackPrefabList[index], trackSpawnPosition, trackPrefabList[index].transform.rotation);
    }
}
