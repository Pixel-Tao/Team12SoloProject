using UnityEngine;

public class ItemDropChance
{
    /// <summary>
    /// min ~ max 사이의 랜덤값을 반환합니다.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="decimalPlaces">소수점 자리수</param>
    /// <returns></returns>
    public static float GetRandom(float min, float max, int decimalPlaces = 4)
    {
        float randomValue = Random.Range(min, max);
        //소수점 자리수에 따라 반올림
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        float value = Mathf.Round(randomValue * multiplier) / multiplier;
        return value;
    }
    
    /// <summary>
    /// chance 확률로 아이템을 드랍할지를 반환합니다.
    /// </summary>
    /// <param name="chance">min 0 ~ max 100</param>
    /// <param name="decimalPlaces">소수점 자리수</param>
    /// <returns></returns>
    public static bool IsDrop(float chance, int decimalPlaces = 4)
    {
        chance = Mathf.Clamp(chance, 0, 100);
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        chance = Mathf.Round(chance * multiplier) / multiplier;
        float random = GetRandom(0, 100f, decimalPlaces);
        return random <= chance;
    }
}