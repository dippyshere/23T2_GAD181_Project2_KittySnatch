using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ItemSpawner itemSpawner;
    public ArmController armController;
    public UpgradeManager upgradeManager;
    public GameObject upgradeNotification;
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

    private void Start()
    {
        armController.armSpeed = 1f + (playerSpeedLevel - 1) * 2f;
        armController.slamSpeedMultiplier = playerSlamLevel;
        StartSpawning();
        UpdatePricing();
        upgradeManager.gameManager = this;
        StartCoroutine(checkForUpgrades());
    }

    private void StartSpawning()
    {
        currentSpawnDelay = maxSpawnDelay - (spawnLevel - 1) * spawnDelayDecreaseRate + 0.1f;

        Invoke("SpawnItem", currentSpawnDelay);
    }

    private void SpawnItem()
    {
        itemSpawner.SpawnItem();
        if (!isPaused)
        {
            currentSpawnDelay = maxSpawnDelay - (spawnLevel - 1) * spawnDelayDecreaseRate + 0.1f;
            Invoke("SpawnItem", currentSpawnDelay);
        }
    }

    public void IncreaseSpawnLevel()
    {
        spawnLevel++;
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
