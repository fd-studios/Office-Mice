using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public partial class WaveSpawner : MonoBehaviour
{
    int _nextWave = 0;
    SpawnState _state = SpawnState.Counting;
    float _searchCountdown = 1f;
    uint _statMultiplier = 1;
    int _countDown;

    public Wave[] waves;
    public float timeBetweenWaves = 3f;
    public float waveCountDown;
    public GameObject[] SpawnPoints;
    public Text Label;
    public AudioSource[] CountDown = new AudioSource[11];

    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        _countDown = Mathf.RoundToInt(waveCountDown);
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == SpawnState.Waiting)
        {
            if (EnemiesAlive())
            {
                return;
            }
            WaveCompleted();
        }
        if (waveCountDown <= 0)
        {
            if (_state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWaves(waves[_nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.smoothDeltaTime;
            StartCoroutine(UpdateCountDown(Mathf.FloorToInt(waveCountDown)));
        }
        Label.gameObject.SetActive(_state == SpawnState.Counting);
    }

    IEnumerator UpdateCountDown(int countDown)
    {
        if (countDown >= 0 && _countDown != countDown)
        {
            yield return new WaitForSeconds(1);

            if(_nextWave >= 0)
            {
                CountDown[_countDown].Play();
                Label.text = $"Wave {_nextWave + 1}: {_countDown}";
            }
            _countDown = countDown;
        }
        yield break;
    }

    IEnumerator SpawnWaves(Wave wave)
    {
        Debug.Log($"Spawning Wave: {wave.name}");
        _state = SpawnState.Spawning;

        for (int i = 0; i < wave.count * _statMultiplier; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }

        _state = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Enemy enemy)
    {
        Debug.Log($"Spawning enemy: ");
        var spawnPoints = SpawnPoints[Random.Range(1,4)];
        //This didn't work
        //enemy.StatMultiplier = _statMultiplier;
        Instantiate(enemy.transform, spawnPoints.transform.position, spawnPoints.transform.rotation);
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
        _state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;
        _nextWave++;
        if (_nextWave >= waves.Length)
        {
            _nextWave = 0;
            _statMultiplier += 1;
        }
    }
}
