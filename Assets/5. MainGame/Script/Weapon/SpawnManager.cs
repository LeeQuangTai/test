using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnList = new List<GameObject>();
    public GameObject boss;
    public GameObject _bloodHeeler;
    public bool _isEndAllWave;
    private float spawnTime = 2.0f;
    private Vector2 spawnPosition;
    private float leftLimit = -29f;
    private float rightLimit = 29f;
    private float topLimit = 19f;
    private float bottomLimit = -19f;
    
    private bool canSpawn;
    private int wavesNum = 4;
    public int currentWave { get; private set;} 
    public int _enemyNumAtBegin;
    private int _currentEnemyNumToSpawn;
    private int _currentEnemyNumToKill;
    private List<GameObject> _enemiesOnWave = new List<GameObject>();
    private void Awake()
    {
        currentWave = 0;
        _currentEnemyNumToSpawn = _enemyNumAtBegin;
        canSpawn = true;
    }
    
    void Start()
    {
        StartCoroutine(WavesSpawn());
        StartCoroutine(SpawnBlood(2));
    }

    void Update()
    {
        _enemiesOnWave = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        _currentEnemyNumToKill = _enemiesOnWave.Count;
        if (_currentEnemyNumToKill <= 0)
        {
            canSpawn = true;
        }
        if (wavesNum == -1)
        {
            _isEndAllWave = true;
            Debug.Log("End Waves");
        }
        if (_isEndAllWave)
        {
                   
        }
    }

    IEnumerator WavesSpawn()
    {
        Debug.Log(wavesNum);
        while (wavesNum > 0 && canSpawn)
        {
            currentWave++;
            for (int i = 0; i < _currentEnemyNumToSpawn; i++)
            {
                spawnPosition = new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(topLimit, bottomLimit));
                SpawnEnemy(spawnPosition);
            }
            canSpawn = false;
            wavesNum --;
            _currentEnemyNumToSpawn += 2;
            yield return new WaitUntil(() => canSpawn);
            yield return new WaitForSeconds(2);
        }
        if (wavesNum == 0)
        {
            spawnPosition = new Vector2(0,0);
            SpawnBoss(spawnPosition);            
            wavesNum --;
        }
    }

    private void SpawnBoss(Vector2 vector2)
    {
        Instantiate(boss, this.spawnPosition, Quaternion.identity);
    }

    IEnumerator SpawnBlood(float timeWaitToSpawnBlood)
    {
        yield return new WaitForSeconds(timeWaitToSpawnBlood);
        _spawnBlood(new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(topLimit, bottomLimit)));
    }
    void _spawnBlood(Vector2 spawnPosition)
    {
        Instantiate(_bloodHeeler, this.spawnPosition, Quaternion.identity);
    }
    void SpawnEnemy(Vector2 spawnPosition)
    {
        Instantiate(spawnList[Random.Range(0, spawnList.Count)], this.spawnPosition, Quaternion.identity);
    }
}
