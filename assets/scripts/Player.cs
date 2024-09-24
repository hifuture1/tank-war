using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed=3;
    private Vector3 bulletEulerAngles;
    private float timeVal;//计时器
    private float defendTimeVal=3f;
    private bool isDefended=true;//玩家被保护状态
    private float vlock;
    private float hlock;
    //引用
    private SpriteRenderer sr;
    public Sprite[]tankSprite;//上右下左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;//先拿到音效的组件
    public AudioClip[] tankAudio;//再拿到音效的素材
    private void Awake() {
        
        sr=GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
        //攻击CD
        if (timeVal >= 0.4f)
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
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move();
        
    }
    //坦克的攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //子弹产生的角度：当前坦克的角度+子弹旋转的角度
            Instantiate(bulletPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
    }
    //坦克的移动方法
    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");//垂直方向优先
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
        if (Mathf.Abs(v) > 0.05f)//v的绝对值大于0.05秒
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
 
        if (v != 0)
        {
            return;//停止
        }

        float h = Input.GetAxisRaw("Horizontal");
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
        if (Mathf.Abs(h) > 0.05f)//v的绝对值大于0.05秒
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        if (h == 0 && v == 0)//AKF的情况
        {
            vlock = 0;
            hlock = 0;
        }

        if (h != 0 && v == 0)//竖向单键
        {
            hlock = 2;
            vlock = 1;
        }
        else if (h == 0 && v != 0)//横向单键
        {
            hlock = 1;
            vlock = 2;
        }
        else if (h != 0 && v != 0)//双键
        {
            if (vlock * hlock == 0)//这个可以防止AFK后快速双键齐按导致斜走
            {
                //Debug.Log("Surprise mother fucker");
                return;
            }
            else if (hlock > vlock)
                h = 0;
            else if (hlock < vlock)
                v = 0;
        }
    }
    //坦克的死亡方法
    private void Die()
    {
       //无敌状态跳过死亡
        if (isDefended)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }

}
