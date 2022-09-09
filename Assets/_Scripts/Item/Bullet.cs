using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,5);
        rb.bodyType = RigidbodyType2D.Static;
    }
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BossHealth"||other.gameObject.tag == "CantShoot")
        {
            Destroy(gameObject);
        }
    }
}
