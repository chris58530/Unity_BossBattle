using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingLens : MonoBehaviour
{
    [SerializeField] GameObject blackHole;
    [SerializeField] GameObject boom;
    SpriteRenderer sprite;

    float speed = 10;
    float t;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(Boomer());
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;


        if (speed <= 0)
        {
            speed = 0;
        }
        else
        {
            speed -= 5 * Time.deltaTime;
        }
      
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.Rotate(Vector3.up * speed, Space.World);
    }
    IEnumerator Boomer()
    {     
       
        yield return new WaitForSeconds(2);
        GameObject h = Instantiate(blackHole, transform.position, transform.rotation);
        Destroy(h, 1);
        yield return new WaitForSeconds(0.5f);

        sprite.enabled = false;
        yield return new WaitForSeconds(0.5f);

        GameObject b = Instantiate(boom, transform.position, transform.rotation);
        Destroy(b, 3);
        Destroy(gameObject);

    }
   
}
