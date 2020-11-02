using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] List<float> delays;

    public GameObject getEnemyPrefab() {return enemyPrefab; }
    public List<Transform> getWaypoints() {

        List<Transform> wayPoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform)
        {
            wayPoints.Add(waypoint);
        }
        return wayPoints;
    }
    public List<float> getDelays()
    {
        return this.delays;
    }
    public float getTimeBetweenSpawn() { return timeBetweenSpawn; }
    public float getSpawnRandomFactor() { return spawnRandomFactor; }
    public int getNumberOfEnemies() { return numberOfEnemies; }
    public float getMoveSpeed() { return moveSpeed; }
    public float getRotateSpeed() { return rotateSpeed; }
}
