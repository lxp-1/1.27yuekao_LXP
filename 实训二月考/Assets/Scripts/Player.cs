using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerModelMsg modelMsg;
    float speed = 1;
    Animator anim;
    public int hp;
    public int buCount =0;
    GameObjectMgr1 gameMgr;
    public void Init(GameObjectMgr1 _gameMgr)
    {
        gameMgr = _gameMgr;
    }
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        modelMsg = Playermsg.GetT.playerModelMsg;
        hp = modelMsg.hp;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            float x = h == 0 ? 0 : Time.deltaTime * modelMsg.speed * h / Mathf.Abs(h)* speed;
            float z = v == 0 ? 0 : Time.deltaTime * modelMsg.speed * v / Mathf.Abs(v)* speed;
            transform.Translate(new Vector3(x, 0, z));
            anim.SetBool("run",true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        float xx = Input.GetAxis("Mouse X");
        if (xx!=0)
        {
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y+xx*5,0);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&buCount>0)
        {
            anim.SetTrigger("att");
            GameObject onj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            onj.transform.position = transform.position+new Vector3(0,1.5f,0);
            onj.name = "playerbu";
            Destroy(onj,3);
            Rigidbody ri= onj.AddComponent<Rigidbody>();
            ri.useGravity = false;
            ri.AddForce(transform.forward*1000);
            buCount -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "补给")
        {
            hp += 20;
            buCount += 10;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "bossbu")
        {

            speed /= 2;
            Destroy(other.gameObject);
            gameMgr.boss.GetComponent<Boss>().state = State.Chase;
        }
    }
    public void shoushang(int att)
    {
        hp -= att;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
