using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private ShotCounterText shotCounterText;
    public Text ballsCountText;
    public GameObject[] block; 
    public List<GameObject> levels;
    private GameObject level1,level2;
    private Vector2 level1Pos,level2Pos;
    
    public int shootCount;
    public int ballsCount;
    public GameObject ballsContainer; 
    public GameObject gameover,pause;
    public Text goldText;
    public int score=0;
    private bool firstShot=true; 
    public Sprite[] cannons;
    public SpriteRenderer currentCannon;
    public bool isMenu,gameOver;
    public int currentBall
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentBall",1);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentBall",value);
        }
    }
    public int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("Gold",0);
        }
        set
        {
            PlayerPrefs.SetInt("Gold",value);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(!isMenu)
        {
            shotCounterText=GameObject.Find("ShotUI").GetComponent<ShotCounterText>();
            currentCannon.sprite=cannons[currentBall-1];
        }
    }
    void Start()
    {
        gameOver=false;
        if(!isMenu)
        {
            ballsCount=PlayerPrefs.GetInt("Balls",5);
            Physics2D.gravity=new Vector2(0,-17);
            ballsCountText.text=ballsCount.ToString();
            SpawnLevel();
            GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMenu)
        {
            if(ballsContainer.transform.childCount==0 && shootCount==4 && !gameOver)
            {
                
                goldText.text=score.ToString();
                gameover.SetActive(true);
                pause.SetActive(false);
                GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn",false);
                Gold+=score;
                gameOver=true;
            }
            if(shootCount>2)
            {
                firstShot=false;
            }
            else
            {
                firstShot=true;
            }
            CheckBlocks();
        }

    }
    void SpawnNewLevel(int levelNumber1,int levelNumber2,int min,int max)
    {
        if(shootCount>1)
        {
                Camera.main.GetComponent<CameraTransitions>().RotateCameraToFront();
        }
        shootCount=1;
        Camera.main.GetComponent<CameraTransitions>().RotateCameraToFront();
        level1Pos=new Vector2(3.5f,1);
        level2Pos=new Vector2(3.5f,-3.4f);

        level1=levels[levelNumber1];
        level2=levels[levelNumber2];

        Instantiate(level1,level1Pos,Quaternion.identity);
        Instantiate(level2,level2Pos,Quaternion.identity);
        SetBlocksCount(min,max);
    }
    void SpawnLevel()
    {
        if(PlayerPrefs.GetInt("Level",0)==0)
        {
            SpawnNewLevel(0,17,3,5);
        }
        else if(PlayerPrefs.GetInt("Level",0)==1)
        {
            SpawnNewLevel(1,18,3,5);
        }
        else if(PlayerPrefs.GetInt("Level",0)==2)
        {
            SpawnNewLevel(2,19,3,6);
        }
        else if(PlayerPrefs.GetInt("Level",0)==3)
        {
            SpawnNewLevel(5,20,4,7);
        }
        else if(PlayerPrefs.GetInt("Level",0)==4)
        {
            SpawnNewLevel(12,17,5,8);
        }
        else if(PlayerPrefs.GetInt("Level",0)==5)
        {
            SpawnNewLevel(14,29,7,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==6)
        {
            SpawnNewLevel(12,27,6,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==7)
        {
            SpawnNewLevel(8,14,9,15);
        }
        else if(PlayerPrefs.GetInt("Level",0)==8)
        {
            SpawnNewLevel(5,24,5,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==9)
        {
            SpawnNewLevel(3,37,6,11);
        }
        else if(PlayerPrefs.GetInt("Level",0)==10)
        {
            SpawnNewLevel(8,30,4,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==11)
        {
            SpawnNewLevel(17,30,5,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==12)
        {
            SpawnNewLevel(9,39,8,11);
        }
        else if(PlayerPrefs.GetInt("Level",0)==13)
        {
            SpawnNewLevel(22,36,8,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==14)
        {
            SpawnNewLevel(11,28,9,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==15)
        {
            SpawnNewLevel(18,23,4,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==16)
        {
            SpawnNewLevel(30,38,3,6);
        }
        else if(PlayerPrefs.GetInt("Level",0)==17)
        {
            SpawnNewLevel(14,27,8,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==18)
        {
            SpawnNewLevel(19,31,10,15);
        }
        else if(PlayerPrefs.GetInt("Level",0)==19)
        {
            SpawnNewLevel(1,6,8,15);
        }
        else if(PlayerPrefs.GetInt("Level",0)==20)
        {
            SpawnNewLevel(3,8,6,15);
        }
        else if(PlayerPrefs.GetInt("Level",0)==21)
        {
            SpawnNewLevel(17,25,8,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==22)
        {
            SpawnNewLevel(14,15,7,10);
        }
        else if(PlayerPrefs.GetInt("Level",0)==23)
        {
            SpawnNewLevel(16,38,6,11);
        }
        else if(PlayerPrefs.GetInt("Level",0)==24)
        {
            SpawnNewLevel(24,27,8,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==25)
        {
            SpawnNewLevel(11,12,7,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==26)
        {
            SpawnNewLevel(9,39,8,11);
        }
        else if(PlayerPrefs.GetInt("Level",0)==27)
        {
            SpawnNewLevel(22,36,8,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==28)
        {
            SpawnNewLevel(11,28,9,12);
        }
        else if(PlayerPrefs.GetInt("Level",0)==29)
        {
            SpawnNewLevel(12,17,12,15);
        }
    }
    void SetBlocksCount(int min,int max)
    {
        block=GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < block.Length; i++)
        {
            int count=Random.Range(min,max);
            block[i].GetComponent<Block>().SetStartingCount(count);
        }
    }
    public void CheckBlocks()
    {
        block=GameObject.FindGameObjectsWithTag("Block");
        if(block.Length<1)
        {
            RemoveBalls();
            PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
            SpawnLevel();
            if(ballsCount>=PlayerPrefs.GetInt("Balls",5))
            {
                PlayerPrefs.SetInt("Balls",ballsCount);
            }
            if(firstShot)
            {
                score+=5;
            }
            else{
                score+=3;
            }
        }
    }
    void RemoveBalls()
    {
        GameObject[] balls=GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
    }
    public void CheckShotCount()
    {
        if(shootCount==1)
        {
            shotCounterText.SetTopText("SHOT");
            shotCounterText.SetBottomText("1/3");
            shotCounterText.Flash();
        }
        else if(shootCount==2)
        {
            shotCounterText.SetTopText("SHOT");
            shotCounterText.SetBottomText("2/3");
            shotCounterText.Flash();
        }
        else
        {
            shotCounterText.SetTopText("FINAL");
            shotCounterText.SetBottomText("SHOT");
            shotCounterText.Flash();
        }
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
