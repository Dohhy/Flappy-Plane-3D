using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpeciliaties : MonoBehaviour
{
    [SerializeField] private int m_price;
    public int Price
    {
        get
        {
            return m_price;
        }
    }
    public bool isPurchased = false;

    private void Start()
    {
        CheckPurchase();
    }

    public void CheckPurchase()
    {
        if (PlayerPrefs.HasKey(gameObject.name))
        {
            isPurchased = true;
        }
    }
}
