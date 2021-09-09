using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public Transform trashParent;
    [SerializeField] private GameObject[] trashes;

    public float minSpawnPosX;
    public float maxSpawnPosX;

    [Range(0, 10)] public float minInterval;
    [Range(0, 10)] public float maxInterval;

    void Start()
    {
        float timeInterval = GetTimeInterval();
        StartCoroutine(SpawnCountdown(timeInterval));
    }

    IEnumerator SpawnCountdown(float timer)
    {
        yield return new WaitForSeconds(timer);

        SpawnTrash(SelectRandomTrash());

        float timeInterval = GetTimeInterval();
        StartCoroutine(SpawnCountdown(timeInterval));
    }

    private float GetTimeInterval()
    {
        return Random.Range(minInterval, maxInterval);
    }

    private float GetSpawnPosXInterval()
    {
        return Random.Range(minSpawnPosX, maxSpawnPosX);
    }

    private GameObject SelectRandomTrash()
    {
        return trashes[Random.Range(0, trashes.Length)];
    }

    private void SpawnTrash(GameObject trash)
    {
        GameObject ne = Instantiate(trash, trashParent);
        ne.transform.position = new Vector3(GetSpawnPosXInterval(), trashParent.position.y);
    }
}
