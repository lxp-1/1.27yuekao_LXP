using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMgr1 : MonoBehaviour
{

    public GameObject player;
    public GameObject boss;
    void Start()
    {
        player = ResManager.GetT.Load<GameObject>(ResPath.none, Playermsg.GetT.modelName);
        player = Instantiate(player);
        player.AddComponent<Player>().Init(this);


            boss = ResManager.GetT.Load<GameObject>(ResPath.none, "Boss");
        boss = Instantiate(boss);
        boss.AddComponent<Boss>().Init(this);


        Camera.main.transform.SetParent(player.transform.Find("CameraPos"), false);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
    }

    float supplyTime = 10;
    void Update()
    {
        supplyTime += Time.deltaTime;
        if (supplyTime >= 10)
        {
            GameObject onj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            onj.transform.position = new Vector3(Random.Range(-15f, 15), 0, Random.Range(-15f, 15));
            onj.name = "补给";
            supplyTime = 0;
        }
    }

}
