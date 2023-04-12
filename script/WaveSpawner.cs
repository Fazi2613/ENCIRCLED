using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WaveSpawner : MonoBehaviour{

    [System.Serializable]
    public class waveContent{
        [SerializeField] GameObject[] enemySpawner;
        [SerializeField] Transform[] spawnLocations;
        
        public GameObject[] getMonsterSpawnList(){
            return enemySpawner;
        }

        public Transform[] getSpawnLocations(){
            return spawnLocations;
        }
    }

    [SerializeField][NonReorderable] waveContent[] waves;
    int currentWave = 0;
    float spawnRange = 0;
    public int enemiesKilled;
    public List<GameObject> currentEnemy;
    public TMP_Text waveText;

   private bool allWavesSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnWavesWithDelay(5f, 22f));
    }

    IEnumerator SpawnWavesWithDelay(float delayBetweenWaves, float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        allWavesSpawned = false;
        currentWave = 0;
        while (!allWavesSpawned)
        {
            spawnWave();
            yield return new WaitUntil(() => currentEnemy.Count == 0);
            currentWave++;
            if (currentWave >= waves.Length)
            {
                allWavesSpawned = true;
                yield break;
            }
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }


    void spawnWave()
    {
        waveContent currentWaveContent = waves[currentWave];
        Transform[] spawnLocations = currentWaveContent.getSpawnLocations();
        for(int i = 0; i < currentWaveContent.getMonsterSpawnList().Length; i++){
            int randomIndex = Random.Range(0, spawnLocations.Length);
            Transform spawnTransform = spawnLocations[randomIndex]; 

            GameObject newSpawn = Instantiate(currentWaveContent.getMonsterSpawnList()[i],FindSpawnLoc(spawnTransform),Quaternion.identity);
            currentEnemy.Add(newSpawn);

            AiController zombie = newSpawn.GetComponent<AiController>();
            zombie.setSpawner(this);
            waveText.text = (currentWave + 1).ToString() +"."+" hullám";
        }
    }

    Vector3 GetSpawnLocation(waveContent waveContent)
    {
        Transform[] spawnLocations = waveContent.getSpawnLocations();
        int randomIndex = Random.Range(0, spawnLocations.Length);
        Vector3 spawnPos = spawnLocations[randomIndex].position;

        if (Physics.Raycast(spawnPos, Vector3.down, 5)) {
            return spawnPos;
        } else {
            return GetSpawnLocation(waveContent);
        }
    }

    Vector3 FindSpawnLoc(Transform spawnTransform)
     {
        Vector3 spawnPos;
        float xLoc = Random.Range(-spawnRange, spawnRange) + spawnTransform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + spawnTransform.position.z;
        float yLoc = spawnTransform.position.y;
        spawnPos = new Vector3(xLoc,yLoc,zLoc);

        if(Physics.Raycast(spawnPos, Vector3.down, 5)){
            return spawnPos;
        } else {
            return FindSpawnLoc(spawnTransform);
        }
    }
}
