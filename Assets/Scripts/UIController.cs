using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject WinnerScreen;

    [SerializeField] private TextMeshProUGUI Points;
    [SerializeField] private Image Followed;

    [SerializeField] private Sprite isFollowed;
    [SerializeField] private Sprite isNotFollowed;



    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else Destroy(gameObject);

        SetActive_GameOverScreen(false);
        SetActive_WinnerScreen(false);
    }

    void Start()
    {
        UpdatePoints(0, GameController.instance.NpcAmount);
        UpdateFollowed(false);
    }



    public void UpdatePoints(ushort points, ushort outOf) {
        Points.text = $"{(points < 10 ? $"0{points}" : points)}/{(outOf < 10 ? $"0{outOf}" : outOf)}";
    }
    public void UpdateFollowed(bool beingFollowed) {
        Followed.sprite = beingFollowed ? isFollowed : isNotFollowed;
    }

    public void SetActive_GameOverScreen(bool value) {
        GameOverScreen.SetActive(value);
    }
    public void SetActive_WinnerScreen(bool value) {
        WinnerScreen.SetActive(value);
    }
}
