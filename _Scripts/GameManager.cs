using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text m_scoreText;
    public GameObject loseTextObj;
    public Text endScoreText;

    [Header("Player Components")]
    public GameObject playerObject;

    private Transform playerTransform;
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private Player playerScript;

    private int m_score;

    [Header("UI Game Objects")]
    public GameObject homeSceneUI;
    public GameObject gameSceneUI;
    public GameObject gameOverSceneUI;

    [Header("Other Components")]
    [SerializeField] private SpawnManager spawnManagerScript;
    [SerializeField] private ShopManager shopManagerScript;

    public static bool isGameStarted = false;
    public static bool isPlaneCrashed = false;


    private void Start()
    {
        playerTransform = playerObject.GetComponent<Transform>();
        playerScript = playerObject.GetComponent<Player>();
        playerRb = playerObject.GetComponent<Rigidbody>();
        playerAnimator = playerObject.GetComponent<Animator>();

        m_score = 0;

        isGameStarted = false;
        isPlaneCrashed = false;

        playerRb.constraints = RigidbodyConstraints.FreezePositionY;

        playerScript.enabled = false;
        playerAnimator.enabled = false;
        spawnManagerScript.enabled = false;
        shopManagerScript.enabled = true;

        homeSceneUI.SetActive(true);
        gameSceneUI.SetActive(false);
        gameOverSceneUI.SetActive(false);
    }
    

    public void AddScore(int score)
    {
        m_score += score;
        m_scoreText.text = m_score.ToString();
    }
    public void GameOver()
    {
        endScoreText.text = m_score.ToString();

        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        playerScript.enabled = false;
        playerAnimator.enabled = false;

        gameOverSceneUI.SetActive(true);
        gameSceneUI.SetActive(false);

        shopManagerScript.PlayerPizzaCount = m_score;
        shopManagerScript.SavePizzaCount();

        Instantiate(loseTextObj, playerTransform.position + new Vector3(1.4f, 4.5f, 0), loseTextObj.transform.rotation);
    }
    public void CloudCollision()
    {
        gameSceneUI.SetActive(false);
        playerScript.enabled = false;
        isPlaneCrashed = true;
    }


    //Buttons
    public void StartButton()
    {
        isGameStarted = true;
        shopManagerScript.UseButton();

        //Enable components
        playerAnimator.enabled = true;
        playerScript.enabled = true;
        spawnManagerScript.enabled = true;
        shopManagerScript.enabled = false;

        //Player rigidbody settings
        playerRb.constraints = RigidbodyConstraints.None;
        playerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX;
        playerRb.constraints = RigidbodyConstraints.FreezeRotationY;

        //Active - deactive objects
        gameSceneUI.SetActive(true);
        homeSceneUI.SetActive(false);

        //Functions to call
        playerScript.JumpButton();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}
