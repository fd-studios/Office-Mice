using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class WaveSpawner : MonoBehaviour
{
    int _nextWave = 0;
    int _waveCount = 1;
    SpawnState _state;
    float _searchCountdown = 1f;
    int _statMultiplier = 1;
    GameObject[] _spawnPoints;

    public Wave[] waves;
    public float timeBetweenWaves = 3f;
    public float waveCountDown;
    public Text Label;

    public AudioSource Ready;
    public AudioSource Begin;
    public AudioSource RushAnnouncement;

    // Start is called before the first frame update
    void Start()
    {
        StartCountDown();

        _spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
    }

    void StartCountDown()
    {
        Label.gameObject.SetActive(true);
        _state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == SpawnState.Waiting)
        {
            if (!EnemiesAlive())
                WaveCompleted();
        }
        else if(_state == SpawnState.Spawning)
        {

        }
        else if(_state == SpawnState.Counting)
        {

            var i = (int)waveCountDown;
            waveCountDown -= Time.deltaTime;
            var j = (int)waveCountDown;

            if (j < i)
            {
                Label.text = $"Wave {_waveCount}: {j + 1}";

                if (i == 1)
                    Ready.Play();
                else if (i == 0)
                {
                    Begin.Play();
                    Label.gameObject.SetActive(false);
                    StartCoroutine(SpawnWaves(waves[_nextWave]));
                }
            }
        }
    }


    IEnumerator SpawnWaves(Wave wave)
    {
        _state = SpawnState.Spawning;

        StartCoroutine(StartRush(wave));
        for (int i = 0; i < wave.Count; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(1 / wave.Rate);
        }

        _state = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Wave wave)
    {
        var enemy = ObjectPooler.SharedInstance.GetPooledObject<Enemy>(wave.ObjectTag);
        var spawnPoints = _spawnPoints[UnityEngine.Random.Range(1, _spawnPoints.Length) - 1];
        if (enemy != null)
        {
            enemy.transform.position = spawnPoints.transform.position;
            enemy.transform.rotation = spawnPoints.transform.rotation;
            enemy.StatMultiplier = _statMultiplier;
            enemy.gameObject.SetActive(true);
        }
    }

    bool EnemiesAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0)
        {
            _searchCountdown = 1f;
            return FindObjectsOfType<Mouse>().Length > 0;
        }
        return true;
    }

    void WaveCompleted()
    {
        _nextWave++;
        _waveCount++;
        if (_nextWave >= waves.Length)
        {
            _nextWave = 0;
            _statMultiplier += 1;
        }

        StartCountDown();
    }

    IEnumerator StartRush(Wave wave)
    {
        if(_state == SpawnState.Counting) yield break;
        yield return new WaitForSeconds(wave.RushTimer);
        if(RushAnnouncement != null) RushAnnouncement.Play();
        var enemeies = GameObject.FindObjectsOfType<Enemy>();
        foreach (var enemy in enemeies)
        {
            enemy.Kill();
        }
        yield break;
    }
}

[Serializable]
public class Wave
{
    public string Name;
    public Enemy Enemy;
    public int Count;
    public float Rate;
    public int RushTimer;
    public string ObjectTag;
}

public enum SpawnState { Spawning, Waiting, Counting }