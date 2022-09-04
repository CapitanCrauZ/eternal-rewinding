using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
 
    public GameObject Enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int enemyRotation;

    private void Start()
    {
        transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime);
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop(){
        while (enemyCount < 10){   
            xPos = Random.Range(1, 50);
            zPos = Random.Range(1, 31);
            Instantiate(Enemy, new Vector3(xPos, 0f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

    void Update(){
        
    }
    
}
