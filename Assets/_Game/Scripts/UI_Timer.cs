using CodeStage.AdvancedFPSCounter.CountersData;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityExtensions.Tween;

public class UI_Timer : MonoBehaviour, IToggleable
{
    [SerializeField] private int pomodoroTime = 1500;
    [SerializeField] private int breakTime = 300;
    [SerializeField] private float countTime = 0;

    [SerializeField] GameObject popup;
    [SerializeField] TweenPlayer tp;

    [SerializeField] TMP_Text[] text;
    public bool pausing = false;
    public bool running = false;
    public bool breaking = false;

    [SerializeField] GameObject playHover;
    [SerializeField] GameObject pauseHover;
    public void PauseTimer()
    {
        if (pausing)
        {
            pausing = false;
            Debug.Log("Unpause");
            playHover.SetActive(false);
            pauseHover.SetActive(true);
        }
        else
        {
            pausing = true;
            Debug.Log("Pausing");
            playHover.SetActive(true);
            pauseHover.SetActive(false);
        }
    }
    public void StartTimer() {
        if (running || breaking)
        {
            PauseTimer();
        }
        else
        {
            playHover.SetActive(false);
            pauseHover.SetActive(true);
            running = true;
            countTime = pomodoroTime;
            Debug.Log("Start");
        }
    }

    public void ResetTimer()
    {
        if (countTime > 0)
        {
            countTime = 5;
            pausing = false;
        }
    }
    public void SetTime()
    {
        int minutes = Mathf.FloorToInt(countTime / 60);

        // Returns the remainder
        int seconds = Mathf.FloorToInt(countTime % 60);
        text[0].text = (minutes / 10).ToString();
        text[1].text = (minutes % 10).ToString();
        text[2].text = (seconds / 10).ToString();
        text[3].text = (seconds % 10).ToString();
    }

    private void Update()
    {
        if((running || breaking) && !pausing)
        {
            if(countTime > 0)
            {
                countTime -= Time.deltaTime;
                if(countTime <= 0)
                {
                    if (breaking)
                    {
                        breaking = false;
                        Debug.Log("Stop");
                        playHover.SetActive(true);
                        pauseHover.SetActive(false);
                    }
                    if (running)
                    {
                        running = false;
                        breaking = true;
                        countTime = breakTime;
                        Debug.Log("breaking");
                    }
                    
                }
                else
                {
                    SetTime();
                }
            }
        }
    }

    bool isPop = false;
    public void Pop()
    {
        if (isPop)
        {
            ToggleOff();
            isPop = false;
        }
        else
        {
            ToggleOn();
            isPop = true;
        }
    }

    public void ToggleOn()
    {
        tp.ForcePlayRuntime();
    }

    public void ToggleOff()
    {
        tp.ForcePlayBackRuntime();
    }
    [SerializeField] Slider pomodoroTimeInput;
    [SerializeField] TMP_Text ptiText;
    [SerializeField] Slider breakTimeInput;
    [SerializeField] TMP_Text btiText;
    public void ValueChange()
    {
        pomodoroTime = Mathf.FloorToInt(pomodoroTimeInput.value) * 60;
        breakTime = Mathf.FloorToInt(breakTimeInput.value) * 60;
        ptiText.text = pomodoroTimeInput.value.ToString();
        btiText.text = breakTimeInput.value.ToString();
    }
}
