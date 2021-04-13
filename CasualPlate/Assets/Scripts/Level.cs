using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private int level;

    [Header("TaskVariables")]
    //количество тарелок на полке для выигрыша
    [SerializeField] private int platesTask;
    //количество тарелок на полке
    public int platesOnShelf;
    //количество данных на уровень тарелок
    [SerializeField] private int platesDano;
    //количество оставшихся тарелок
    public int platesOst;

    [Header("UI")]
    [SerializeField] private Text taskText;
    //слайдер попаданий
    [SerializeField] private Slider slider;
    //слайдер потраченных тарелок
    [SerializeField] private Slider sliderLives;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject loseBtn;
    void Start()
    {
        StartLevel();
    }
    public void StartLevel()
    {
        loseBtn.SetActive(false);
        levelText.text = level.ToString();
        taskText.text = "Put " + platesTask.ToString() + " props to shelf or table";
        slider.maxValue = platesTask;
        platesOnShelf = 0;
        slider.value = 0;
        platesOst = platesDano;
        sliderLives.maxValue = platesDano;
        sliderLives.value = platesDano;
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = platesOnShelf;
        sliderLives.value = platesOst;
        if (platesOst <= 0)
            loseBtn.SetActive(true);
    }
}
