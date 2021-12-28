using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCount : MonoBehaviour
{
    TextMeshProUGUI goldLable;
    Bank bank;

    private void Awake()
    {
        goldLable = GetComponent<TextMeshProUGUI>();
        bank = GameObject.FindObjectOfType<Bank>();
    }
    // Update is called once per frame
    void Update()
    {
        goldLable.text = $"Gold: {bank.CurrentBalance}";
    }
}
