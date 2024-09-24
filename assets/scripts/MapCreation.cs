using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //����װ�γ�ʼ��ͼ�������������
    //0.�ϼ� 1.ǽ 2.�ϰ� 3.����Ч�� 4.���� 5.�� 6.����ǽ
    public GameObject[] item;

    //�Ѿ��ж�����λ���б�
    public List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {
        InitMap();
    }
    //��װһ��ʵ������ͼ�ķ���
    private void InitMap()
    {
        //ʵ�����ϼ�
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);//����ת
        //��ǽ���ϼ�Χ����
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //ʵ������Χǽ(����ǽ)
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        //��ʼ�����
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<born>().createPlayer = true;
        //��������

        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        //ÿ��һ��ʱ�����²���һ������,��һ��4s���ڶ���5s
        InvokeRepeating("CreateEnemy", 4, 5);
        //ʵ������ͼ.����20������
        for (int i = 0; i < 40; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    //��װ��һ������ʹ��hierarchy�ĵ�ͼ�Ķ�����ɢ��
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);

    }
    //�������λ�õķ���
    private Vector3 CreateRandomPosition()
    {
        //������x=-10��10�����У�y=-8��8���е�λ��
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10),Random.Range(-7,8),0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
           
    }
    //�����ж�λ���б����Ƿ������λ��
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }
    //�������˵ķ���
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);//012
        Vector3 EnemyPos = new Vector3();
        if (num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else if (num == 2)
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);

    }
}
