using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI balanceText;

    public TextMeshProUGUI spawnLevelPrice;
    public TextMeshProUGUI scoreLevelPrice;
    public TextMeshProUGUI playerSpeedLevelPrice;
    public TextMeshProUGUI playerSlamLevelPrice;

    public TextMeshProUGUI spawnLevelText;
    public TextMeshProUGUI scoreLevelText;
    public TextMeshProUGUI playerSpeedLevelText;
    public TextMeshProUGUI playerSlamLevelText;

    public Button spawnLevelButton;
    public Button scoreLevelButton;
    public Button playerSpeedLevelButton;
    public Button playerSlamLevelButton;

    public CursorController cursorController;

    public Volume postProcessVolume;

    public bool upgradesAvailable = false;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cursorController.UnlockCursor();
        UpdateUI();
        Time.timeScale = 0f;
        postProcessVolume.profile = Resources.Load<VolumeProfile>("PostProcessing Profile 1");
    }

    private void OnEnable()
    {
        cursorController.UnlockCursor();
        UpdateUI();
        Time.timeScale = 0f;
        postProcessVolume.profile = Resources.Load<VolumeProfile>("PostProcessing Profile 1");
        gameManager.isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        balanceText.text = "<sprite=0> " + gameManager.armController.scoreManager.energy.ToString("N0", CultureInfo.InvariantCulture);
        spawnLevelText.text = "Level: " + gameManager.spawnLevel.ToString();
        scoreLevelText.text = "Level: " + gameManager.scoreLevel.ToString();
        playerSpeedLevelText.text = "Level: " + gameManager.playerSpeedLevel.ToString();
        playerSlamLevelText.text = "Level: " + gameManager.playerSlamLevel.ToString();

        upgradesAvailable = false;

        if (gameManager.spawnLevel >= 6 || gameManager.armController.scoreManager.energy <= gameManager.spawnLevelPrice)
        {
            spawnLevelButton.interactable = false;
            //spawnLevelButton.gameObject.GetComponent<Image>().color = new Color(255, 116, 95, 255);
        }
        else
        {
            spawnLevelButton.interactable = true;
            //spawnLevelButton.gameObject.GetComponent<Image>().color = new Color(160, 255, 95, 255);
            upgradesAvailable = true;
        }

        if (gameManager.armController.scoreManager.energy <= gameManager.scoreLevelPrice)
        {
            scoreLevelButton.interactable = false;
            //scoreLevelButton.gameObject.GetComponent<Image>().color = new Color(255, 116, 95, 255);
        }
        else
        {
            scoreLevelButton.interactable = true;
            //spawnLevelButton.gameObject.GetComponent<Image>().color = new Color(160, 255, 95, 255);
            upgradesAvailable = true;
        }

        if (gameManager.playerSpeedLevel >= 20 || gameManager.armController.scoreManager.energy <= gameManager.playerSpeedLevelPrice)
        {
            playerSpeedLevelButton.interactable = false;
            //playerSpeedLevelButton.gameObject.GetComponent<Image>().color = new Color(255, 116, 95, 255);
        }
        else
        {
            playerSpeedLevelButton.interactable = true;
            //spawnLevelButton.gameObject.GetComponent<Image>().color = new Color(160, 255, 95, 255);
            upgradesAvailable = true;
        }

        if (gameManager.playerSlamLevel >= 20 || gameManager.armController.scoreManager.energy <= gameManager.playerSlamLevelPrice)
        {
            playerSlamLevelButton.interactable = false;
            //playerSlamLevelButton.gameObject.GetComponent<Image>().color = new Color(255, 116, 95, 255);
        }
        else
        {
            playerSlamLevelButton.interactable = true;
            //spawnLevelButton.gameObject.GetComponent<Image>().color = new Color(160, 255, 95, 255);
            upgradesAvailable = true;
        }

        spawnLevelPrice.text = "<sprite=0>" + gameManager.spawnLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        scoreLevelPrice.text = "<sprite=0>" + gameManager.scoreLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        playerSpeedLevelPrice.text = "<sprite=0>" + gameManager.playerSpeedLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        playerSlamLevelPrice.text = "<sprite=0>" + gameManager.playerSlamLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
    }

    public void UpgradeSpawnLevel()
    {
        if (gameManager.armController.scoreManager.energy >= gameManager.spawnLevelPrice)
        {
            gameManager.armController.scoreManager.DecreaseEnergy(gameManager.spawnLevelPrice);
            gameManager.IncreaseSpawnLevel();
            UpdateUI();
        }
    }

    public void UpgradeScoreLevel()
    {
        if (gameManager.armController.scoreManager.energy >= gameManager.scoreLevelPrice)
        {
            gameManager.armController.scoreManager.DecreaseEnergy(gameManager.scoreLevelPrice);
            gameManager.IncreaseScoreLevel();
            UpdateUI();
        }
    }

    public void UpgradePlayerSpeedLevel()
    {
        if (gameManager.armController.scoreManager.energy >= gameManager.playerSpeedLevelPrice)
        {
            gameManager.armController.scoreManager.DecreaseEnergy(gameManager.playerSpeedLevelPrice);
            gameManager.IncreasePlayerSpeedLevel();
            UpdateUI();
        }
    }

    public void UpgradePlayerSlamLevel()
    {
        if (gameManager.armController.scoreManager.energy >= gameManager.playerSlamLevelPrice)
        {
            gameManager.armController.scoreManager.DecreaseEnergy(gameManager.playerSlamLevelPrice);
            gameManager.IncreasePlayerSlamLevel();
            UpdateUI();
        }
    }

    public void CloseUpgradeMenu()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        cursorController.LockCursor();
        Time.timeScale = 1f;
        postProcessVolume.profile = Resources.Load<VolumeProfile>("PostProcessing Profile");
        gameManager.isPaused = false;
    }
}
