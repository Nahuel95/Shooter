using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PushBackAmuletEffect : MonoBehaviour
{
    Rigidbody body;
    Transform player;
    float lifeTime = 1f;
    NavMeshAgent navigator;
    Vector3 direction;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = Transform.FindObjectOfType<PlayerController>().transform;
        navigator = GetComponent<NavMeshAgent>();
        dir = (body.transform.position - player.position);
        direction = new Vector3(dir.x, 0, dir.z).normalized * 10f;
        //Debug.Log(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (dir.magnitude <= 3f) {
            body.MovePosition(player.position + dir.normalized * 3.2f);
        }
        navigator.velocity = direction;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(this);
        }
    }
}
