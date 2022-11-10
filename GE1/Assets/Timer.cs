using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    int countDownStartValue = 300;
    public Text timerUI;
    // Start is called before the first frame update

    void Start()
    {
        countDownTimer();
    }
    void countDownTimer()
    {
        if (countDownStartValue > 0)
        {
            TimeSpan spanTime = TimeSpan.FromSeconds(countDownStartValue);
            timerUI.text = +spanTime.Minutes + ":" + spanTime.Seconds;
            Debug.Log("Timer: " + countDownStartValue);
            countDownStartValue--;
            Invoke("countDownTimer", 1.0f);
        }
        else
        {
            SceneManager.LoadScene(3);
        }

    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
