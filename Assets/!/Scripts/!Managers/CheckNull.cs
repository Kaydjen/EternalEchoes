public static class CheckNull
{
    /// <summary>
    ///  If null - return true
    /// </summary>
    /// <returns></returns>
    public static bool Camera()
    {
        if (CameraSwitcher.Instance == null)
        {
            UnityEngine.Debug.Log($"{nameof(CameraSwitcher.Instance)} is null");
            return true;
        }
        else return false;
    }
    /// <summary>
    ///  If null - return true
    /// </summary>
    /// <returns></returns>
    public static bool Player()
    {
        if (PlayerCore.Instance == null)
        {
            UnityEngine.Debug.Log($"{nameof(PlayerCore.Instance)} is null");
            return true;
        }
        else return false;
    }
}