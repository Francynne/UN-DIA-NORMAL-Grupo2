using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    float maxiumTime = 180.0F;

    float currentTime;

    bool timerEnabled = true;

    void Awake()
    {
        currentTime = maxiumTime;
        slider.maxValue = maxiumTime;
        slider.value = currentTime;
    }

     void Update()
    {
        OnTimerChanged();
    }

    void OnTimerChanged()
    {
        currentTime -= Time.deltaTime;

        if (!timerEnabled)
        {
            return;
        }

        if(currentTime > 0.0F && currentTime < maxiumTime)
        {
            slider.value = currentTime;
        }
        else
        {
            timerEnabled = false;
        }
    }

    //bool timerEnabled;

     //void Start()
    //{
       // ActivateTimer();
    //}

     //void Update()
    //{
    //    ChangeTimer();
    //}

    //void ActivateTimer()
    //{
    //    currentTime = maxiumTime;
    //    slider.maxValue = currentTime;
    //    EnableTimer(true);
    //}

    //void EnableTimer (bool enabled)
    //{
    //    timerEnabled = enabled;
    //}

    //void ChangeTimer()
    //{
    //    if (!timerEnabled)
    //    {
    //        return;
    //    }
    //    currentTime -= Time.deltaTime;
    //    if (currentTime > 0.0F)
    //    {
    //        slider.value = currentTime;
    //    }
    //    else
    //    {
    //        EnableTimer(false);
    //    }
    //}
}
