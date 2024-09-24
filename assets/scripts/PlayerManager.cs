using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    //属性值
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;
    //引用
    public GameObject born;
    public Text PlayerScoreText;
    public Text PlayerLifeValueText;
    public GameObject isDefeatUI;
    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnTheMainMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        PlayerScoreText.text = playerScore.ToString();
        PlayerLifeValueText.text = lifeValue.ToString();
    }
    private void Recover()
    {
        lifeValue--;
        if (lifeValue <=0)
        {
            //游戏失败，返回主界面
            isDefeat = true;
            Invoke("ReturnTheMainMenu", 3);
        }
        else
        {
            
            GameObject go = Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
            go.GetComponent<born>().createPlayer =true;
            isDead = false;
        }
    }
    private void ReturnTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
