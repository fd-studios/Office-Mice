using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public partial class WaveSpawner : MonoBehaviour
{
    int _nextWave = 0;
    int _waveCount = 1;
    SpawnState _state = SpawnState.Counting;
    float _searchCountdown = 1f;
    int _statMultiplier = 1;
    int _countDown;
    GameObject[] _spawnPoints;

    public Wave[] waves;
    public float timeBetweenWaves = 3f;
    public float waveCountDown;
    public Text Label;
    public AudioSource[] CountDown = new AudioSource[11];
    public AudioSource RushAnnouncement;

    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        _countDown = Mathf.RoundToInt(waveCountDown);
        _spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        Debug.Log($"Spawn Points:{_spawnPoints.Length}");
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
                Label.text = $"Wave {_waveCount}: {_countDown + 1}";
            }
            _countDown = countDown;
        }
        yield break;
    }

    IEnumerator SpawnWaves(Wave wave)
    {
        Debug.Log($"Spawning Wave: {wave.Name}");
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
        Debug.Log($"Spawning enemy: ");
        var enemy = ObjectPooler.SharedInstance.GetPooledObject<Enemy>(wave.ObjectTag);
        var spawnPoints = _spawnPoints[Random.Range(1, _spawnPoints.Length) - 1];
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
        _state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;
        _nextWave++;
        _waveCount++;
        if (_nextWave >= waves.Length)
        {
            _nextWave = 0;
            _statMultiplier += 1;
        }
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
