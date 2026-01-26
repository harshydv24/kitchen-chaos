using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance{get; set;}

    private int CurrentMoney;
    private int RecentMoney;
    private int TargetMoney;
    private int TotalMoneyEarned;
    private float UpdateTime = 1f;

    private void Awake()
    {
        Instance = this;
        CurrentMoney = PlayerPrefs.GetInt("Total Money", 0);
    }

    private void Start()
    {
        TotalMoneyEarned = 0;
        DeliveryManager.Instance.UpdateMoneyAmount += DeliveryManager_UpdateMoneyAmount;
    }

    private void DeliveryManager_UpdateMoneyAmount(object sender, DeliveryManager.UpdateMoneyAmountEventArgs e)
    {
        UpdateMoneyStats(e.MoneyAmount);
        StartCoroutine(MoneyUpdateAnimation());
        
        if(CurrentMoney <= 0)
        {
            CurrentMoney = 0;
        }
    }

    public int GetCurrentMoneyAmount()
    {
        return CurrentMoney;
    }

    public int GetTotalMoneyEarned()
    {
        return TotalMoneyEarned;
    }

    // deducting money for purchasing counters.
    public void DeductMoney(int deductAmount)
    {
        UpdateMoneyStats(-deductAmount);
        StartCoroutine(MoneyUpdateAnimation());
    }

    private void SaveCurrentMoney(int moneyAmount)
    {
        PlayerPrefs.SetInt("Total Money", moneyAmount);
        PlayerPrefs.Save();
    }

    private IEnumerator MoneyUpdateAnimation()
    {
        float timeElapsed = 0f;
        while(timeElapsed < UpdateTime)
        {
            float t = timeElapsed / UpdateTime;
            CurrentMoney = (int)Mathf.Lerp(RecentMoney, TargetMoney, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        CurrentMoney = TargetMoney;
        SaveCurrentMoney(CurrentMoney);
    }

    private void UpdateMoneyStats(int updationMoney)
    {
        RecentMoney = CurrentMoney;
        TargetMoney = RecentMoney + updationMoney;
        TotalMoneyEarned += updationMoney;
        if(TotalMoneyEarned < 0)
        {
            TotalMoneyEarned = 0;
        }
    }
}
