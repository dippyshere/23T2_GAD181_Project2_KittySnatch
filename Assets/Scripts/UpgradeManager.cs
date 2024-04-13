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
    public VolumeProfile defaultPostProcess;
    public VolumeProfile upgradePostProcess;

    public bool upgradesAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        cursorController.UnlockCursor();
        UpdateUI();
        Time.timeScale = 0f;
        postProcessVolume.profile = upgradePostProcess;
        GameManager.instance.isPaused = true;
        GameManager.instance.armController.canSlam = false;
    }

    private void OnEnable()
    {
        cursorController.UnlockCursor();
        UpdateUI();
        Time.timeScale = 0f;
        postProcessVolume.profile = upgradePostProcess;
        GameManager.instance.isPaused = true;
        GameManager.instance.armController.canSlam = false;
    }

    public void UpdateUI()
    {
        balanceText.text = "<sprite=0> " + GameManager.instance.armController.scoreManager.energy.ToString("N0", CultureInfo.InvariantCulture);
        spawnLevelText.text = "Level: " + GameManager.instance.spawnLevel.ToString();
        scoreLevelText.text = "Level: " + GameManager.instance.scoreLevel.ToString();
        playerSpeedLevelText.text = "Level: " + GameManager.instance.playerSpeedLevel.ToString();
        playerSlamLevelText.text = "Level: " + GameManager.instance.playerSlamLevel.ToString();

        upgradesAvailable = false;

        if (GameManager.instance.spawnLevel >= 6 || GameManager.instance.armController.scoreManager.energy <= GameManager.instance.spawnLevelPrice)
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

        if (GameManager.instance.armController.scoreManager.energy <= GameManager.instance.scoreLevelPrice)
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

        if (GameManager.instance.playerSpeedLevel >= 20 || GameManager.instance.armController.scoreManager.energy <= GameManager.instance.playerSpeedLevelPrice)
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

        if (GameManager.instance.playerSlamLevel >= 20 || GameManager.instance.armController.scoreManager.energy <= GameManager.instance.playerSlamLevelPrice)
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

        spawnLevelPrice.text = "<sprite=0>" + GameManager.instance.spawnLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        scoreLevelPrice.text = "<sprite=0>" + GameManager.instance.scoreLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        playerSpeedLevelPrice.text = "<sprite=0>" + GameManager.instance.playerSpeedLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
        playerSlamLevelPrice.text = "<sprite=0>" + GameManager.instance.playerSlamLevelPrice.ToString("N0", CultureInfo.InvariantCulture);
    }

    public void UpgradeSpawnLevel()
    {
        if (GameManager.instance.armController.scoreManager.energy >= GameManager.instance.spawnLevelPrice)
        {
            GameManager.instance.armController.scoreManager.DecreaseEnergy(GameManager.instance.spawnLevelPrice);
            GameManager.instance.IncreaseSpawnLevel();
            UpdateUI();
        }
    }

    public void UpgradeScoreLevel()
    {
        if (GameManager.instance.armController.scoreManager.energy >= GameManager.instance.scoreLevelPrice)
        {
            GameManager.instance.armController.scoreManager.DecreaseEnergy(GameManager.instance.scoreLevelPrice);
            GameManager.instance.IncreaseScoreLevel();
            UpdateUI();
        }
    }

    public void UpgradePlayerSpeedLevel()
    {
        if (GameManager.instance.armController.scoreManager.energy >= GameManager.instance.playerSpeedLevelPrice)
        {
            GameManager.instance.armController.scoreManager.DecreaseEnergy(GameManager.instance.playerSpeedLevelPrice);
            GameManager.instance.IncreasePlayerSpeedLevel();
            UpdateUI();
        }
    }

    public void UpgradePlayerSlamLevel()
    {
        if (GameManager.instance.armController.scoreManager.energy >= GameManager.instance.playerSlamLevelPrice)
        {
            GameManager.instance.armController.scoreManager.DecreaseEnergy(GameManager.instance.playerSlamLevelPrice);
            GameManager.instance.IncreasePlayerSlamLevel();
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
        postProcessVolume.profile = defaultPostProcess;
        GameManager.instance.isPaused = false;
        GameManager.instance.armController.canSlam = true;
    }
}
