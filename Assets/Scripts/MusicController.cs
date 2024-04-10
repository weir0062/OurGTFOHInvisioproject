using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

    public AudioSource musicSource;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Находим компонент AudioSource в объекте, который проигрывает музыку
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        // Находим компонент Slider, чтобы получить доступ к его событиям
        volumeSlider = GetComponent<Slider>();

        // Устанавливаем начальное значение ползунка в соответствии с текущей громкостью музыки
        volumeSlider.value = musicSource.volume;

        // Подписываемся на событие изменения значения ползунка
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        // Устанавливаем громкость музыки в соответствии с текущим значением ползунка
        musicSource.volume = volumeSlider.value;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
