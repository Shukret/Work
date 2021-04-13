using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float waitTime;
    [SerializeField] private GameObject spawnStickmans;
    [SerializeField] private GameObject fire;

    [SerializeField] private Transform[] spawnPointsStickmans;
    [SerializeField] private Transform[] spawnPointsFire;

    public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float zMinRange = 8.0f;
	public float zMaxRange = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TimelineWait()); 
        StartCoroutine(SpawnStickmans());   
        StartCoroutine(SpawnCamps()); 
    }

    IEnumerator SpawnStickmans()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(spawnStickmans, spawnPointsStickmans[Random.Range(0,spawnPointsStickmans.Length)].position, Quaternion.identity);
        }
    }

    IEnumerator SpawnCamps()
    {
        yield return new WaitForSeconds(waitTime);

        Vector3 spawnPosition;
        for (int i=0; i<10; i++)
        {
            spawnPosition = new Vector3(Random.Range (xMinRange, xMaxRange), 15, Random.Range (zMinRange, zMaxRange));
            Instantiate(fire, spawnPosition, Quaternion.identity);
        }
    }

    IEnumerator TimelineWait()
    {
        yield return new WaitForSeconds(6.5f);
        StartCoroutine(SpawnStickmans());   
        StartCoroutine(SpawnCamps()); 
    }
}
