using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermsg : Singleton<Playermsg>
{
    public string modelName;
    public PlayerModelMsg playerModelMsg;
    public PlayerModelMsg[] allModelMsg;
    public Playermsg()
    {
        modelName = "player1";
        allModelMsg = new PlayerModelMsg[3];
        string str = Resources.Load<TextAsset>("player").text;
        string[] strs = str.Split('\n');
        for (int i = 0; i < 3; i++)
        {
            allModelMsg[i] = new PlayerModelMsg(strs[i]);
        }
        playerModelMsg = allModelMsg[0];
    }

}
public class PlayerModelMsg
{
    public string name;
    public int hp;
    public int speed;
    public int occupy;
    public PlayerModelMsg(string str)
    {
        string[] strs = str.Split('_');

        name = strs[0];
        hp = int.Parse(strs[1]);
        speed = int.Parse(strs[2]);
        occupy = int.Parse(strs[3]);


    }
}
