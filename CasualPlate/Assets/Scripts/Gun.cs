using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject emptyCollider;
    Vector3 speed;
    public GameObject[] BulletPrefabs;
    public float Power = 100;

    public TrajectoryRenderer Trajectory;

    private Camera mainCamera;

    bool stopC;
    
    [SerializeField] private DynamicJoystick fixedJoystick;
    [SerializeField] private Slider powerSlider;
    [SerializeField] private Level levelScript;
    public float hor;
    public float ver;

    private void Start()
    {
        mainCamera = Camera.main;
        powerSlider.value = 0;
        //StartCoroutine(PulEmptyCol());
    }

    private void Update()
    {
        float enter;
        ver = fixedJoystick.Vertical;
        if (fixedJoystick.Horizontal < 0)
        {
            hor += 0.25f;
        }
        else if(fixedJoystick.Horizontal > 0)
        {
            hor -= 0.25f;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        speed = new Vector3(hor, mouseInWorld.y - transform.position.y, mouseInWorld.z - transform.position.z - 90) * Power;
        transform.rotation = Quaternion.LookRotation(speed);
        Trajectory.ShowTrajectory(transform.position, speed);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
    }

        //IEnumerator PulEmptyCol()
        //{
        //    while (true)
        //    {
        //        yield return new WaitForSeconds(0.05f);
        //        Rigidbody em = Instantiate(emptyCollider, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //        em.AddForce(speed, ForceMode.VelocityChange);
        //    }
        //}

        IEnumerator PowerUp()
        {
            while (Power < 1)
            {   
                if (stopC == false)
                {
                    Power += 0.01f;
                    powerSlider.value = Power;
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    break;
                }
            }
        }

        public void Down()
        {
            if (levelScript.platesOst>0)
            {
                stopC = false;
                StartCoroutine(PowerUp());
            }
        }
        public void Up()
        {
            if (levelScript.platesOst>0)
            {
                int i = Random.Range(0, BulletPrefabs.Length);
                Rigidbody bullet = Instantiate(BulletPrefabs[i], transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                bullet.AddForce(speed, ForceMode.VelocityChange);
                bullet.AddForce(Vector3.up*4, ForceMode.VelocityChange);
                bullet.GetComponent<plate>().levelScript = levelScript;
                stopC = true;
                hor = 0;
                Power = 0;
                powerSlider.value = Power;
            }
        }
}