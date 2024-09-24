using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefablist;
    public bool createPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);//延时调用
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void BornTank()
    {
        if (createPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);//随机0，1两个数字
            Instantiate(enemyPrefablist[num], transform.position, Quaternion.identity);
        }
        
    }
}
