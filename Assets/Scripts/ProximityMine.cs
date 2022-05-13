using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    Collider collider;
    float actionRadius = 5f;
    public GameObject explosion;
    bool activated;
    float lifeTime = 120f;
    public float Damage { get; set; } = 20f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponentInParent<Enemy>() != null && !activated) {
            activated = true;
            Collider[] colls = Physics.OverlapSphere(transform.position, actionRadius);
            foreach (Collider c in colls) {
                if (c.gameObject.GetComponentInParent<Enemy>() != null) {
                    c.gameObject.GetComponentInParent<Enemy>().TakeDamage(Damage);
                    Debug.Log("Damage done");
                }
            }
            Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
      
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, actionRadius);
    }
}
