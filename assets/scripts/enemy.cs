using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //����ֵ
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float v=-1
        ;
    private float h;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//��������
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    //��ʱ��
    private float timeVal;
    private float timeValchangeDirection;

    private void Awake()
    {

        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
      
        //������ʱ����-
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }

    }
    private void FixedUpdate()  //�̶�һ֡��ʱ��
    {
        Move();
        
    }
    //̹�˵Ĺ�������
    private void Attack()
    {
      
      //�ӵ������ĽǶȣ���ǰ̹�˵ĽǶ�+�ӵ���ת�ĽǶ�
       Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
       timeVal = 0;
        
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        if (timeValchangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            timeValchangeDirection = 0;
        }
        else
        {
            timeValchangeDirection += Time.fixedDeltaTime;
        }

        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (v != 0)
        {
            return;//ֹͣ
        }

        
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
    }
    //̹�˵���������
    private void Die()
    {
        //������������ҵ÷ּ�һ
        PlayerManager.Instance.playerScore++;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
    //����˲ʱ��ת�Ƕ�
    private void OnCollisionEner2D(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValchangeDirection = 4;
        }
    }

}
