using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coinText;
    public GameObject shopPanel;
    private int currentBall,currentGold;

    public GameObject[] buttons;
    public int[] prices;
      public GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Cannon1",1);
        currentGold=gameController.Gold;
        currentBall=gameController.currentBall;
        coinText.text=currentGold.ToString();
        buttons[currentBall-1].GetComponent<Button>().interactable=false;
        buttons[currentBall-1].GetComponentInChildren<Text>().text="USED";
        for(int i=0;i<buttons.Length;i++)
        {
            
            if(i!=currentBall-1)
            {
                string value="Cannon"+(i+1);
                if(PlayerPrefs.GetInt(value,0)==1)
                {
                    buttons[i].GetComponentInChildren<Text>().text="USE".ToString();
                    buttons[i].GetComponentInChildren<Text>().fontSize=14;
                }
                else
                {
                    buttons[i].GetComponentInChildren<Text>().text=prices[i].ToString();
                    buttons[i].GetComponentInChildren<Text>().fontSize=19;
                    if(prices[i]>currentGold)
                    {
                        buttons[i].GetComponent<Button>().interactable=false;
                    }
                }
                
            }
        }
    }

    // Update is called once per frame

    public void BuyCannons(int index)
    {
        bool kontrol=false;
        for(int i=0;i<buttons.Length;i++)
        {
            if(index-1==i)
            {
                string value="Cannon"+(i+1);
                if(PlayerPrefs.GetInt(value,0)==1)
                {
                    //kullandi
                    currentBall=index;
                    gameController.currentBall=index;
                    kontrol=true;
                }
                else if(prices[i]<=currentGold)
                {
                    //satin aldi
                    currentBall=index;
                    gameController.currentBall=index;
                    currentGold-=prices[i];
                    coinText.text=currentGold.ToString();
                    PlayerPrefs.SetInt(value,1);
                    kontrol=true;
                }
            }
        }
        if(kontrol)
        {
            buttons[currentBall-1].GetComponentInChildren<Text>().text="USED";
            buttons[currentBall-1].GetComponent<Button>().interactable=false;
            for(int i=0;i<buttons.Length;i++)
            {
            
            if(i!=currentBall-1)
            {
                string value="Cannon"+(i+1);
                if(PlayerPrefs.GetInt(value,0)==1)
                {
                    buttons[i].GetComponentInChildren<Text>().text="USE".ToString();
                    buttons[i].GetComponent<Button>().interactable=true;
                    buttons[i].GetComponentInChildren<Text>().fontSize=14;
                }
                else
                {
                    buttons[i].GetComponentInChildren<Text>().text=prices[i].ToString();
                    buttons[i].GetComponentInChildren<Text>().fontSize=19;
                    if(prices[i]>currentGold)
                    {
                        buttons[i].GetComponent<Button>().interactable=false;
                    }
                }
                
            }
            }
        }
    }

    public void DeActivateGameObject()
    {
        shopPanel.SetActive(false);
    }
     public void ActivateGameObject()
    {
        shopPanel.SetActive(true);
    }
}
