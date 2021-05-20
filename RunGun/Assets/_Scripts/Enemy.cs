using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("lives")]
    [SerializeField] private float maxLives;
    public float lives;
    [SerializeField] private Slider sl;

    [Header("Texts")]
    [SerializeField] private Text currentText;
    [SerializeField] private Text maxText;

    [Header("PickUp")]
    public GameObject pickUp;

    // Start is called before the first frame update
    void Start()
    {
        sl.maxValue = maxLives;
        maxText.text = maxLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        sl.value = lives;    
        currentText.text = lives.ToString();
        if (lives <= 0)
        {
            pickUp.SetActive(true);
            Destroy(gameObject);
        }
    }
}
