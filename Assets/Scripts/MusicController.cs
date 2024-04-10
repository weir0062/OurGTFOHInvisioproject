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
        // ������� ��������� AudioSource � �������, ������� ����������� ������
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        // ������� ��������� Slider, ����� �������� ������ � ��� ��������
        volumeSlider = GetComponent<Slider>();

        // ������������� ��������� �������� �������� � ������������ � ������� ���������� ������
        volumeSlider.value = musicSource.volume;

        // ������������� �� ������� ��������� �������� ��������
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        // ������������� ��������� ������ � ������������ � ������� ��������� ��������
        musicSource.volume = volumeSlider.value;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
