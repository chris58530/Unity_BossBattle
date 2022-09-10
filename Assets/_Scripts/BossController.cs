using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    [SerializeField] Transform ShootPoint;
    [SerializeField] Material healthMaterial;
    [SerializeField] Material bossMaterial;
    [SerializeField] GameObject healthBall;
    [SerializeField] Transform PosUp;
    [SerializeField] Transform PosDown;
    [SerializeField] LineRenderer line;
    bool bossDir;
    float hurtTime = 0;

    [SerializeField] UnityEngine.UI.Image up;
    [SerializeField] UnityEngine.UI.Image down;
    [SerializeField] GameObject healthBarUp;
    [SerializeField] GameObject healthBarDown;

    [SerializeField] GameObject aimingLens;
    [SerializeField] GameObject SmallBullet;
    [SerializeField] GameObject BigBullet;


    public bool followPlayer = false;
    bool backToRotate = false;
    bool turnAround = false;
    public bool canShoot;
    public bool canShoot2;
    bool canBoom;
    bool canShake = true;
    public bool canDash;
    bool canMove;
    bool canDir;
    bool canLazer;
    bool canRotate;
    bool mad = false;
    int mode;
    int currentMode;
    Animator ani;


    private void Awake()
    {
        instance = this;
        ani = GetComponent<Animator>();
        BossHealth.health = 10;

    }
    void Start()
    {
        canShoot = false;
        canShoot2 = false;
        canBoom = true;
        canDash = true;
        canMove = false;
        canRotate = false;
        canLazer = true;
        mode = 1;

        healthBarDown.SetActive(true);
        healthBarUp.SetActive(false);

        bossMaterial.color = Color.gray;


        bossDir = true;

    }

    // Update is called once per frame
    void Update()
    {
        up.fillAmount = (float)BossHealth.health / 10;
        down.fillAmount = (float)BossHealth.health / 10;
        TurnAround();
        MoveToPlayer();
        BossMode();
        FollowPlayer();
        BackToRotate();
        GetHurt();
        BossDir();
        //lazer();
        ani.SetFloat("Health", BossHealth.health);
        //if(!GameObject.FindGameObjectWithTag("Player"))
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //}
    }

    void BossMode()
    {
        switch (mode)
        {
            case 0:
              
                canShoot = true;
                canBoom = true;
                canShoot2 = true;
                canDash = true;
                backToRotate = true;
                if (BossHealth.health <= 6 && !mad)
                {
                    mad = true;
                    StartCoroutine(Mad());
                    bossMaterial.color = Color.Lerp(Color.grey,Color.magenta, Time.deltaTime);
                }
                mode = Random.Range(1, 5);
                currentMode = mode;
                break;
            case 1:
                if (canShoot)
                {
                    if (mad)
                        StartCoroutine(Shoot(3, 3, 6));
                    else if (!mad)
                        StartCoroutine(Shoot(3, 3, 3));
                }
                break;
            case 2:
                if (canBoom)
                    StartCoroutine(Boom());

                break;
            case 3:
                if (canShoot2)
                {
                    if (mad)
                    {
                        StartCoroutine(Shoot2(4, 6, 6));
                    }else if (!mad)
                    {
                        StartCoroutine(Shoot2(4, 4, 4));

                    }
                }
                break;
            case 4:
                if (canDash)
                {
                    StartCoroutine(DashToPlayer());
                }
                break;
                 
            default:
                 canShoot = true;
                canBoom = true;
                canShoot2 = true;
                mode = Random.Range(1, 5);
                currentMode = mode;
                break;

        }
    }
   IEnumerator Mad()
    {
        Vector3 pos =  transform.position;
        for (int i = 0; i < 8; i++)
        {
            bossMaterial.color += new Color(17, 0, 0);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position = pos;
        }
    }
    IEnumerator Shoot(int wave,int times,int count)
    {
        canShoot = false;
        followPlayer = true;
        backToRotate = false;

        yield return new WaitForSeconds(1.5f);


        for (int q = 0; q < wave; q++)
        {
            for (int i = 0; i < times; i++)
            {

                float rot = count * 3 / 2;

                for (int y = 0; y < count; y++)
                {
                    Instantiate(SmallBullet, ShootPoint.position, ShootPoint.rotation * Quaternion.Euler(0, 0, rot));
                    rot -= 6;
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(1.5f);

        }
        followPlayer = false;
        backToRotate = true;
           
        yield return new WaitForSeconds(1.5f);
    
        mode = 0;


    }
    IEnumerator Boom()
    {
        canBoom = false;
        followPlayer = false;
        backToRotate = true;
        Vector3 origin = transform.position;

        for (int i = 0; i < 5; i++)
        {
            transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
            yield return new WaitForSeconds(0.01f);
            Instantiate(aimingLens, new Vector3(5, 0, 5), ShootPoint.rotation);
            transform.position = origin;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.5f);

      
        mode = 0; 
    }
    //IEnumerator LazerAttack()
    //{
    //    canLazer = true;
    //    transform.rotation = Quaternion.Euler(0,-90,0);
    //    yield return new WaitForSeconds(2);
    //    canRotate = true;
    //}
    //void lazer()
    //{
    //    if (canLazer)
    //    {
    //        line.SetPosition(0, ShootPoint.transform.position);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ShootPoint.transform.position, ShootPoint.transform.forward, out hit))
    //        {
    //            if (hit.collider)
    //            {
    //                line.SetPosition(1, hit.point);
    //                Destroy(hit.transform, 5);
    //                if (hit.collider.tag == "Player")
    //                {
    //                    PlayerController plr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //                    plr.PlayerDie();
    //                }

    //            }
    //        }
    //        else line.SetPosition(1, ShootPoint.transform.forward * 5000);
    //    }
    //    else return;
       
    //}
    IEnumerator Shoot2(int wave, int times, int count)
    {
        canShoot2 = false;
        backToRotate = true;
        followPlayer = false;

        Debug.Log("開始協程");

        bool midBullet = true;
        for (int o = 0; o < wave; o++)
        {
            if (o > 0)
            {
                followPlayer = true;
                backToRotate = false;

            }
            for (int i = 0; i < times; i++)
            {
                midBullet = true;
                float rot = count * 15;
                for (int y = 0; y < count / 2; y++)
                {
                    Instantiate(SmallBullet, ShootPoint.position, ShootPoint.rotation * Quaternion.Euler(0, 0, rot));
                    rot -= 30;
                }              
                rot -= 30;
                for (int z = 0; z < count / 2; z++)
                {
                    Instantiate(SmallBullet, ShootPoint.position, ShootPoint.rotation * Quaternion.Euler(0, 0, rot));
                    rot -= 30;
                }
                if (o >= 1 && i  == 3 && midBullet)
                {

                    GameObject big = Instantiate(BigBullet, ShootPoint.position, ShootPoint.rotation * Quaternion.Euler(0, 0, 0));
                    big.transform.localScale = new Vector3(0.04f, 0.2f + (o * 0.1f), 0.04f);
                    midBullet = false;
                }
                yield return new WaitForSeconds(0.5f);

            }
            yield return new WaitForSeconds(1.5f);

        }
      
      
        if (BossHealth.canHurt == false)
        {
            turnAround = false;
            backToRotate = true;
            followPlayer = false;
            mode = 0;

        }
        else
        {
            followPlayer = false;
            turnAround = true;
        }

        yield return new WaitForSeconds(3f);
        turnAround = false;
        mode = 0;



    }
    IEnumerator DashToPlayer()
    {
        canDash = false;
        Vector3 origin = transform.position;
        followPlayer =true;
        turnAround = false;
        backToRotate = false;
        SoundManager.instance.Dash();
        for (int i = 0; i < 15; i++)
        {
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = origin;
        transform.tag = "Kill";
        yield return new WaitForSeconds(1);
        followPlayer = false;
        canMove = true;
        yield return new WaitForSeconds(2);
        canMove = false;

     


        if (bossDir)
        {
            canDir = true;
            transform.position = PosDown.position - new Vector3(0,0,2);
            bossDir = false;
            backToRotate = true;
        }
        else if (!bossDir)
        {
            canDir = true;
            transform.position = PosUp.position + new Vector3(0, 0, 2);
            bossDir = true;
            backToRotate = true;
        }
        yield return new WaitForSeconds(3);
        canDir = false;
        transform.tag = "CantShoot";

        mode = 0;
        yield break;



     

    }
    void MoveToPlayer()
    {
        if (canMove)
        {
            transform.Translate(new Vector3(0, 0, 5* Time.deltaTime));

        }
        else
            return;
    }
    
    void BossDir()
    {
        if (canDir)
        {
            if (bossDir)
            {
                healthBarUp.SetActive(false);
                healthBarDown.SetActive(true);
                backToRotate = true;
                transform.position = Vector3.MoveTowards(transform.position, PosUp.position, 3 * Time.deltaTime);
            }
            else if (!bossDir)
            {
                backToRotate = true;
                healthBarUp.SetActive(true);
                healthBarDown.SetActive(false);
                transform.position = Vector3.MoveTowards(transform.position, PosDown.position , 3 * Time.deltaTime);
            }
        }
      
    }
    void FollowPlayer()
    {
        if (followPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.Log("FollowPlayer");
        }else if (!followPlayer)
        {
            return;
        }
    }
   void BackToRotate()
    {
        if (backToRotate)
        {
            if (bossDir)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -180, 0), 0.05f);
            else if(!bossDir)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
            Debug.Log("BackToRotate");

        }
        else if (!backToRotate)
        {
            return;
        }
    }
    void TurnAround()
    {
        if (turnAround)
        {
            if(bossDir)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
            else if(!bossDir)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,180, 0), 0.05f);

            Debug.Log("TurnAround");

        }
        else if (!turnAround)
        {
            return;
        }
    }
    IEnumerator BallShake()
    {
        canShake = false;
        Vector3 origin = healthBall.transform.position;
        healthBall.transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
        yield return new WaitForSeconds(0.01f);
        healthBall.transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
        yield return new WaitForSeconds(0.01f);
        healthBall.transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
        yield return new WaitForSeconds(0.01f);
        healthBall.transform.position += new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
        yield return new WaitForSeconds(0.01f);
        healthBall.transform.position = origin;
        yield break;    
    }
    void RotateSelf()
    {
        if (canRotate)
        {
            transform.Rotate(Vector3.up * 10*Time.deltaTime, Space.World);

        }
        else return;
    }
    void  GetHurt()
    {
        if (BossHealth.canHurt == false)
        {

         
            hurtTime += Time.deltaTime;
            healthMaterial.color = Color.red;
            if (canShake)
                StartCoroutine(BallShake());
            if (hurtTime > 3)
                BossHealth.canHurt = true;
        }
        else
        {
            healthMaterial.color = Color.blue;
            hurtTime = 0;
            canShake = true;
        }
    }
}
