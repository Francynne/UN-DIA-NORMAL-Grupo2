using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    Character2DController character;

    [SerializeField]
    TextMeshProUGUI starCount;

     void Awake()
    {
        character.OnStartCountChanged.AddListener(OnStartCountChanged);
    }

    void OnStartCountChanged(int value)
    {
        starCount.text = value.ToString();
    }
}
