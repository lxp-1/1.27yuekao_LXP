using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    int shiye = 10;
    float speed = 3;
    int att = 50;
    public int hp = 1000;
    int exp = 0;
    int juli = 3;
    Animator anim;
    GameObjectMgr1 gameMgr;
    NavMeshAgent nav;
    public void Init(GameObjectMgr1 _gameMgr)
    {
        gameMgr = _gameMgr;
    }
    void Start()

    {
        anim = gameObject.GetComponent<Animator>();
        nav = gameObject.AddComponent<NavMeshAgent>();
        nav.speed = speed / 2;
    }

    public State state = State.Idle;
    float exptime = 0;
    float atttime = 0;
    float mohua = 0;
    bool mohua1 = false;
    void Update()
    {
        exptime += Time.deltaTime;
        if (exptime > 1)
        {
            exp += 1;
            exptime = 0;
        }
        switch (state)
        {
            case State.Idle:
                atttime += Time.deltaTime;
                if (atttime >= 2)
                {
                    atttime = 0;
                    state = State.Patrol;
                }
                break;
            case State.Patrol:
                if (Vector3.Distance(nav.destination, transform.position) < 0.5f)
                {
                    anim.SetBool("run", true);
                    nav.isStopped = false;
                    nav.SetDestination(new Vector3(Random.Range(-15f, 15), 0, Random.Range(-15f, 15)));
                }
                if (Vector3.Distance(gameMgr.player.transform.position, transform.position) < shiye / 2)
                {
                    state = State.Chase;
                }
                break;
            case State.Chase:
                nav.SetDestination(gameMgr.player.transform.position);
                if (Vector3.Distance(gameMgr.player.transform.position, transform.position) > shiye / 2)
                {
                    state = State.Patrol;
                }
                if (Vector3.Distance(gameMgr.player.transform.position, transform.position) < juli)
                {
                    state = State.AttackLevel1;
                }
                break;
            case State.AttackLevel1:
                nav.isStopped = true;
                nav.SetDestination(transform.position);
                anim.SetTrigger("att");
                if (Vector3.Distance(gameMgr.player.transform.position, transform.position) < 2)
                {
                    gameMgr.player.GetComponent<Player>().shoushang(att);
                    exp += 10;
                }

                anim.SetBool("run", false);
                state = State.Idle;
                break;
            case State.AttackLevel2:
                mohua -= Time.deltaTime;
                if (mohua<=0)
                {
                    transform.LookAt(gameMgr.player.transform);
                    anim.SetTrigger("att");
                    GameObject onj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    onj.transform.position = transform.position + new Vector3(0, 1.5f, 0);
                    onj.name = "bossbu";
                    Destroy(onj, 3);
                    Rigidbody ri = onj.AddComponent<Rigidbody>();
                    ri.useGravity = false;
                    ri.AddForce(transform.forward * 3000);
                    mohua = 1;
                }
                break;
            default:
                break;
        }
        if (exp>=200&& mohua1==false)
        {
            mohua1 = transform;
            nav.isStopped = true;
            nav.SetDestination(transform.position);
            anim.SetBool("run",false);
            state = State.AttackLevel2;
            shiye +=50;
            att += 50;
            juli += 5;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "playerbu")
        {
            Destroy(other.gameObject);
            hp -= 20;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
public enum State
{
    Idle, Patrol, Chase, AttackLevel1, AttackLevel2
}
