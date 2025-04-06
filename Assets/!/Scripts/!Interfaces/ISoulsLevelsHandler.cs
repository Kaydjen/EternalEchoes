public interface ISoulsLevelsHandler
{
    byte GetCurrentSoulNumber();
    ESoulType GetCurrentSoulType();
    void GetParameters();
    void IncreaseSoulLevel();
}