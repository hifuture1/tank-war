using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float v=-1
        ;
    private float h;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    //计时器
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
      
        //攻击的时间间隔-
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }

    }
    private void FixedUpdate()  //固定一帧的时间
    {
        Move();
        
    }
    //坦克的攻击方法
    private void Attack()
    {
      
      //子弹产生的角度：当前坦克的角度+子弹旋转的角度
       Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
       timeVal = 0;
        
    }
    //坦克的移动方法
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
            return;//停止
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
    //坦克的死亡方法
    private void Die()
    {
        //敌人死亡，玩家得分加一
        PlayerManager.Instance.playerScore++;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
    //敌人瞬时旋转角度
    private void OnCollisionEner2D(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValchangeDirection = 4;
        }
    }

}
