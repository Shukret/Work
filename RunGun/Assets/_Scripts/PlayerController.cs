using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    bool attack;
    [SerializeField] private Transform gfx;
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Slider sl;
    [SerializeField] private Animator anim;

    [Header("Shooting")]
    [SerializeField] private float shootPower;
    [SerializeField] private float shootTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Level")]
    [SerializeField] private int level;
    public int pickUps;
    [SerializeField] private Image[] pichUpImage;
    [SerializeField] private GameObject fightText;
    [SerializeField] private GameObject[] levelItems; 
    [SerializeField] private GameObject pistol;

    [Header("Boss")]
    [SerializeField] private LevelTimer timerScript;
    [SerializeField] private GameObject attackBtn;
    int click;
    [SerializeField] private Text attckText;
    [SerializeField] private Transform axe;

    void Start()
    {
        //StartCoroutine(Shoot());
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootTime);
            GameObject bul = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(DestroyBullet(bul));
            bul.GetComponent<Rigidbody>().velocity = shootPower * Vector3.forward;
        }
    }

    IEnumerator DestroyBullet(GameObject bul)
    {
        yield return new WaitForSeconds(9);
        Destroy(bul);
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerScript.boss)
            transform.Translate(0,0,-speed*Time.deltaTime);
        transform.position = new Vector3(-sl.value, transform.position.y, transform.position.z);

        if (pickUps == 3)
        {
            level += 1;
            pickUps = 0;
            ShowPickUps();
            ShowLevelItems();
        }
    }


    public void RotateMyObject()
    {
        float sliderValue = sl.value;
        transform.rotation = Quaternion.Euler(0, sliderValue * 75, 0);
    }

    public void ShowPickUps()
    {
        for (int i = 0; i < pichUpImage.Length; i++)
        {
            if (pickUps-1>=i)
            {
                pichUpImage[i].color = Color.yellow;
            }
            else
            {
                pichUpImage[i].color = Color.black;
            }
        }
    }

    void ShowLevelItems()
    {
        for (int i = 0; i < pichUpImage.Length; i++)
        {
            if (level-1>i)
            {
                levelItems[i].SetActive(true);
                if (i==1)
                    pistol.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            attackBtn.SetActive(true);
            timerScript.boss = true;
            //anim.applyRootMotion = false;
            anim.SetTrigger("finish");
        }
        if (other.CompareTag("enemy"))
        {
            if (!attack)
                StartCoroutine(Attack());
        }
    }

    public void attBtn()
    {
        fightText.SetActive(true);
        attckText.gameObject.SetActive(true);
        axe.localScale = new Vector3(axe.localScale.x+0.1f,axe.localScale.y+0.1f, axe.localScale.z+0.1f);
        click += 1;
        attckText.text = "+" + click.ToString();
        if (click>=24)
        {
            StartCoroutine(Attack());
            StartCoroutine(StopGame());
        }
    }
    IEnumerator Attack()
    {
        attack = true;
        speed /= 3;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.7f);
        attack = false;
        gfx.rotation=Quaternion.Euler(0, 180, 0);
        speed *=3;
    }   

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }
}