using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Text pizzaCountText;

    private int m_playerPizzaCount = 0;
    public int PlayerPizzaCount
    {
        get
        {
            return m_playerPizzaCount;
        }
        set
        {
            m_playerPizzaCount += value;
        }
    }

    public List<GameObject> vehiclesList = new List<GameObject>();

    private int m_index = 0;

    [Header("Objects")]
    public GameObject purchaseButton;
    public GameObject startButton;
    public GameObject priceObjects;

    public Text priceText;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Selected Index"))
        {
            PlayerPrefs.SetInt("Selected Index", m_index);
            PlayerPrefs.Save();
        }
        m_index = PlayerPrefs.GetInt("Selected Index");

        if (!PlayerPrefs.HasKey("Player Pizza Count"))
        {
            PlayerPrefs.SetInt("Player Pizza Count", 0);
            PlayerPrefs.Save();
        }
        m_playerPizzaCount = PlayerPrefs.GetInt("Player Pizza Count");
    }
    private void Start()
    {
        ListVehicles();
        pizzaCountText.text = $"x{m_playerPizzaCount}";
    }

    public void SavePizzaCount()
    {
        PlayerPrefs.SetInt("Player Pizza Count", m_playerPizzaCount);
    }
    public void NextButton()
    {
        if (m_index >= vehiclesList.Count - 1) {
            m_index = 0;
        }
        else if (m_index < vehiclesList.Count) {
            m_index++;
        }
        CheckPurchase();
        ListVehicles();
    }
    public void PreviousButton()
    {
        if (m_index <= 0) {
            m_index = vehiclesList.Count - 1;
        }
        else if (m_index > 0) {
            m_index--;
        }
        CheckPurchase();
        ListVehicles();
    }
    public void PurchaseButton()
    {
        int price = vehiclesList[m_index].GetComponent<PlaneSpeciliaties>().Price;
        if (m_playerPizzaCount - price >= 0)
        {
            m_playerPizzaCount -= price;
            PlayerPrefs.SetInt("Player Pizza Count", m_playerPizzaCount);
            PlayerPrefs.SetString(vehiclesList[m_index].gameObject.name, vehiclesList[m_index].gameObject.name);
            PlayerPrefs.Save();

            purchaseButton.SetActive(false);
            startButton.SetActive(true);

            vehiclesList[m_index].GetComponent<PlaneSpeciliaties>().CheckPurchase();

            CheckPurchase();
            pizzaCountText.text = $"x{m_playerPizzaCount}";
        }
    }
    public void UseButton()
    {
        PlayerPrefs.SetInt("Selected Index", m_index);
        PlayerPrefs.Save();
    }
    private void ListVehicles()
    {
        for (int i = 0; i < vehiclesList.Count; i++)
        {
            vehiclesList[i].SetActive(false);
        }
        vehiclesList[m_index].SetActive(true);
    }
    public void CheckPurchase()
    {
        PlaneSpeciliaties planeScript = vehiclesList[m_index].GetComponent<PlaneSpeciliaties>();
        planeScript.CheckPurchase();

        if (planeScript.isPurchased) {
            startButton.SetActive(true);
            purchaseButton.SetActive(false);
            priceObjects.SetActive(false);
        }
        else if (!planeScript.isPurchased) {
            startButton.SetActive(false);
            purchaseButton.SetActive(true);
            priceObjects.SetActive(true);
            priceText.text = planeScript.Price.ToString();
        }
    }
}
