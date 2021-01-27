using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginGamePanel : UIBase
{
    GameObject beginGame;
    GameObject select;
    Button button;
    Button button2;
    Text text;
    void Start()
    {
        Toggle toggle=null;
        beginGame = transform.Find("BeginGame").gameObject;
        select = transform.Find("Select").gameObject;
        text = select.transform.Find("Text").GetComponent<Text>();
        button2 = select.transform.Find("Button").GetComponent<Button>();
        button = beginGame.transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(()=> {
            beginGame.SetActive(false);
            select.SetActive(true);
            
        }); button2.onClick.AddListener(()=> {
            SceneManager.LoadScene("GameScene");
            UIManager.GetT.CloseUI(PanelName.BeginGamePanel);


        });
        Transform obj = select.transform.Find("GameObject");
        
        for (int i = 0; i < obj.childCount-1; i++)
        {
            Toggle togg = obj.GetChild(i).GetComponent<Toggle>();
            int index = i;
            togg.onValueChanged.AddListener((b)=> {
                if (b)
                {
                    
                    Playermsg.GetT.modelName = "player" + (index + 1).ToString();
                    Playermsg.GetT.playerModelMsg = Playermsg.GetT.allModelMsg[index];
                    PlayerModelMsg playerModel = Playermsg.GetT.allModelMsg[index];
                    text.text = playerModel.name + "\n血量" + playerModel.hp + "\n移动速度系数" + playerModel.speed + "\n站点速度系数" + playerModel.occupy;
                }
            });

            if (i==0)
            {
                toggle = togg;
            }
        }
        if (toggle!=null)
        {
            toggle.isOn = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
