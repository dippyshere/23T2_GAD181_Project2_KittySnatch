using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemSpawner itemSpawner;
    public ArmController armController;
    public UpgradeManager upgradeManager;
    public GameObject upgradeNotification;
    public TextMeshProUGUI upgradeNotificationText;
    public float maxSpawnDelay = 5f;
    public float spawnDelayDecreaseRate = 0.1f; // delay decreases by this amount per spawn level

    public int spawnLevel = 1; // range 1-5
    public int scoreLevel = 1; // range 1-10
    public int playerSpeedLevel = 1; // range 1-10
    public int playerSlamLevel = 1; // range 1-10

    public int spawnLevelPrice = 1000;
    public int scoreLevelPrice = 1000;
    public int playerSpeedLevelPrice = 1000;
    public int playerSlamLevelPrice = 1000;

    public bool isPaused = false;

    private float currentSpawnDelay;
    private bool checkUpgrades = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ChatGPT - Formula
        armController.armSpeed = 1f + (playerSpeedLevel - 1) * 2f;
        armController.slamSpeedMultiplier = playerSlamLevel;
        StartSpawning();
        UpdatePricing();
        StartCoroutine(checkForUpgrades());
        if (Input.touchSupported)
        {
            upgradeNotificationText.text = "Tap / H to open menu";
        }
    }

    private void StartSpawning()
    {
        // ChatGPT - Formula
        currentSpawnDelay = maxSpawnDelay - (spawnLevel - 1) * spawnDelayDecreaseRate + 0.1f;

        Invoke(nameof(SpawnItem), currentSpawnDelay);
    }

    private void SpawnItem()
    {
        if (!isPaused)
        {
            itemSpawner.SpawnItem();
        }
        // ChatGPT - Formula
        currentSpawnDelay = maxSpawnDelay - (spawnLevel - 1) * spawnDelayDecreaseRate + 0.1f;
        Invoke(nameof(SpawnItem), currentSpawnDelay);
    }

    public void IncreaseSpawnLevel()
    {
        spawnLevel++;
        // ChatGPT - Formula
        currentSpawnDelay = maxSpawnDelay - (spawnLevel - 1) * spawnDelayDecreaseRate + 0.05f;
        UpdatePricing();
    }

    public void IncreaseScoreLevel()
    {
        scoreLevel++;
        UpdatePricing();
    }

    public void IncreasePlayerSpeedLevel()
    {
        playerSpeedLevel++;
        // ChatGPT - Formula
        armController.armSpeed = 1f + (playerSpeedLevel - 1) * 2f;
        UpdatePricing();
    }

    public void IncreasePlayerSlamLevel()
    {
        playerSlamLevel++;
        armController.slamSpeedMultiplier = playerSlamLevel;
        UpdatePricing();
    }

    public void UpdatePricing()
    {
        if (spawnLevel >= 5)
        {
            // Purely random 6 digit number
            spawnLevelPrice = 2653967 * spawnLevel;
        }
        else
        {
            spawnLevelPrice = 250 * spawnLevel;
        }
        scoreLevelPrice = 1000 * scoreLevel;
        playerSpeedLevelPrice = 500 * playerSpeedLevel;
        playerSlamLevelPrice = 750 * playerSlamLevel;
    }

    IEnumerator checkForUpgrades()
    {
        while (checkUpgrades)
        {
            upgradeManager.UpdateUI();
            if (upgradeManager.upgradesAvailable)
            {
                upgradeNotification.SetActive(true);
            }
            else
            {
                upgradeNotification.SetActive(false);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void ResetGame()
    {
        spawnLevel = 1;
        scoreLevel = 1;
        playerSpeedLevel = 1;
        playerSlamLevel = 1;
    }

    private void OnDestroy()
    {
        checkUpgrades = false;
    }
}
