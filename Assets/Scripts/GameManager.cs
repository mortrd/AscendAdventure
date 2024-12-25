using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int GoldCointAmount = 0;
    [SerializeField] TextMeshProUGUI goldUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        goldUI.text = GoldCointAmount.ToString();
    }

    private void Update()
    {

    }

    public void GetCoin(int amount)
    {
        GoldCointAmount += amount;
        goldUI.text = GoldCointAmount.ToString();
    }

}
