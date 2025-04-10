using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemies;
    [SerializeField]
    private float spawnTimeDelay, startSpawnDelay;

    public bool isCompleted;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemieInstance = Instantiate(enemies[i], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimeDelay);

            if (i == enemies.Length - 1)
            {
                isCompleted = true;
                StopCoroutine(Spawn());
            }
        }
    }
}
