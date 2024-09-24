# tank-war
这是一个复刻的坦克大站的游戏项目

复刻坦克大战问题合集

# The referenced script on this Behaviour is missing 如何把这个警告解决-很简单
预制体 脚本丢失
顺藤摸瓜 找到有问题的预制体 ， 然后重新添加正确的脚本/移除没用的脚本！
就是这么简单！！！
1、删除错误的预制体
2、重新制作预制体
3、重新挂脚本

### 碰撞检测条件
1、双方都要有碰撞器
2、运动的一方要有刚体

### 渲染层级
层级越高，越先渲染，视觉上先看到

###  玩家子弹发射不出来问题
原因：bullet脚本下，发射子弹，立马就销毁了
解决：把销毁代码放在if条件代码下，但是出现敌人子弹连续发射
```c#
   switch (collision.tag)
  {
      case "Tank":
          if (!isPlayerBullet)
          {
              collision.SendMessage("Die");
              Destroy(gameObject);
          }
          
          break;
      case "Heart":
          collision.SendMessage("Die");
          Destroy(gameObject);
          break;
      case "Enemy":
          if (isPlayerBullet)
          {
              collision.SendMessage("Die");
              Destroy(gameObject);
          }
          
          break;
      case "Wall":
          Destroy(collision.gameObject);//销毁墙
          Destroy(gameObject);//销毁子弹
          break;
      case "Barrier":
          Destroy(gameObject);
          break;
      default:
          break;
```
怎么解决子弹连续发射问题？
解决：在enemy脚本下，把fixedupdate的attack方法删除
```c#
private void FixedUpdate()  //固定一帧的时间
{
    Move();
    
}
```
### 敌人一出现就是出现特效，没有具体的敌人出现
如图：
![[Pasted image 20240811165407.png]]
解决方案：
发现born的预制体内的脚本的敌人列表没有添加敌人模型
加入之后就可解决该问题
如图：
![[Pasted image 20240811165741.png]]

### unity ui的text拖不进脚本里
发现创建的组件不是text组件，是TextMeshPro组件
![[Pasted image 20240813171834.png]]
而text组件在UI⇨Legacy⇨Text
![[Pasted image 20240813172006.png]]

### 这里有个BUG  玩家有4条命 
bug代码
```c#
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
    
     if (lifeValue <=0)
     {
         //游戏失败，返回主界面
         isDefeat = true;
         Invoke("ReturnTheMainMenu", 3);
     }
     else
     {
         lifeValue--;
         GameObject go = Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
         go.GetComponent<born>().createPlayer =true;
         isDead = false;
     }
 }
```
关键点在于recover中的减生命在判断失败的后面做的
比如：第一条命  lifevalue=3  运行else代码
       第二条命  lifevalue=2  运行else代码
       第三条命  lifevalue=1  运行else代码
       第四条命  lifevalue=0时，运行if代码
解决方案：
把lifeValue--;提到if (lifeValue <=0)前
```c#
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

怎么做开场向上滑的动画？
1、打开Animation窗口
	Window > Animation打开Animation窗口
2、选中要制作动画的物体 
3、创建新的动画Clip 
把mainUI.anim保存在Animations文件夹中
![[Pasted image 20240813221212.png]]
4、选择add property(增加属性)，再选择position
![[Pasted image 20240813221520.png]]5、加入两个关键帧
界面的画面下的关键帧
界面的画面居中的关键帧
6、不循环播放
取消mianUI动画的loop time下的勾
