using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Player : MonoBehaviour
{
    //����ֵ
    public float moveSpeed=3;
    private Vector3 bulletEulerAngles;
    private float timeVal;//��ʱ��
    private float defendTimeVal=3f;
    private bool isDefended=true;//��ұ�����״̬
    private float vlock;
    private float hlock;
    //����
    private SpriteRenderer sr;
    public Sprite[]tankSprite;//��������
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;//���õ���Ч�����
    public AudioClip[] tankAudio;//���õ���Ч���ز�
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
        //�Ƿ����޵�״̬
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
        //����CD
        if (timeVal >= 0.4f)
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
        if (PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move();
        
    }
    //̹�˵Ĺ�������
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ӵ������ĽǶȣ���ǰ̹�˵ĽǶ�+�ӵ���ת�ĽǶ�
            Instantiate(bulletPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");//��ֱ��������
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
        if (Mathf.Abs(v) > 0.05f)//v�ľ���ֵ����0.05��
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
 
        if (v != 0)
        {
            return;//ֹͣ
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
        if (Mathf.Abs(h) > 0.05f)//v�ľ���ֵ����0.05��
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
        if (h == 0 && v == 0)//AKF�����
        {
            vlock = 0;
            hlock = 0;
        }

        if (h != 0 && v == 0)//���򵥼�
        {
            hlock = 2;
            vlock = 1;
        }
        else if (h == 0 && v != 0)//���򵥼�
        {
            hlock = 1;
            vlock = 2;
        }
        else if (h != 0 && v != 0)//˫��
        {
            if (vlock * hlock == 0)//������Է�ֹAFK�����˫���밴����б��
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
    //̹�˵���������
    private void Die()
    {
       //�޵�״̬��������
        if (isDefended)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }

}
