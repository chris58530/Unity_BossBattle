using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;


    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos; 
    [SerializeField] Transform bulletPos1;
    [SerializeField] Transform bulletPos2;
    float time = 0;


    bool reloadBullet;
    bool canShoot;
    Vector3 mousePos;
    Rigidbody rb;
    Camera camera;

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        canShoot = true;
        reloadBullet = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtMouse();
        Shoot();
    }
    private void FixedUpdate()
    {
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(h, 0, v) * speed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //}

    }
    void Shoot()
    {
        if (reloadBullet)
        {
            if(canShoot && Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                bulletPos.position = bulletPos1.position;
                time = 0;
                canShoot = false;
                reloadBullet = false;
            }
        }
        else if (!reloadBullet)
        {
            time += Time.deltaTime;

            if(time > 2)
            {
                canShoot = true;
                reloadBullet = true;
            }
            else
            {
                bulletPos.position = Vector3.MoveTowards(bulletPos.position, bulletPos2.position, 0.3f * Time.deltaTime);

            }
        }
    }
    public void PlayerDie()
    {
        ani.SetTrigger("Die");
    }
    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
 
    IEnumerator ReloadBullet(float t)
    {

        yield return new WaitForSeconds(t);
        reloadBullet = true;
        canShoot = true;
    }
    void LookAtMouse()
    {
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenth;
        if(groundPlane.Raycast(cameraRay, out rayLenth))
        {
            Vector3 pointLook = cameraRay.GetPoint(rayLenth);
            Debug.DrawLine(cameraRay.origin, pointLook,Color.blue);
            transform.LookAt(new Vector3(pointLook.x,transform.position.y,pointLook.z));
        }

    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kill")
        {
            Debug.Log("PlayerDie");
            //PlayerDie();
        }
    }
}
