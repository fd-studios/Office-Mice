using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public partial class WaveSpawner : MonoBehaviour
{
    int nextWave = 0;
    SpawnState state = SpawnState.Counting;
    float searchCountdown = 1f;

    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public GameObject[] SpawnPoints;
    public Text Label;

    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (EnemiesAlive())
            {
                return;
            }
            WaveCompleted();
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWaves(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            Label.text = $"Wave {nextWave + 1}: {Mathf.FloorToInt(waveCountDown)}";
        }
        Label.gameObject.SetActive(state == SpawnState.Counting);
    }

    IEnumerator SpawnWaves(Wave wave)
    {
        Debug.Log($"Spawning Wave: {wave.name}");
        state = SpawnState.Spawning;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }

        state = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log($"Spawning enemy: ");
        var spawnPoints = SpawnPoints[Random.Range(1,4)];
        Instantiate(enemy, spawnPoints.transform.position, spawnPoints.transform.rotation);
    }

    bool EnemiesAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            return FindObjectsOfType<Mouse>().Length > 0;
        }
        return true;
    }

    void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;
        nextWave++;
        if (nextWave >= waves.Length) nextWave = 0;
    }
}
