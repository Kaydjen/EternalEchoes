public interface ICamera
{
    /// <summary>
    ///  It used when character was switched and we want to be sure currentValue player controls were switched correctly
    ///  | FPV -> Manual Controls (WASD) Character is controlled by Player
    ///  | IsometricV -> Manual Controls (WASD) Character is controlled by Player
    ///  | TopDown -> AI Controls (NavMesh) Character is controlled by AI logic
    /// </summary>
    public void ForCharacterSwitch();
    /// <summary>
    /// Unlock character ability to move or do something (Unlock Manual or AI controls)
    /// </summary>
    public void ForControlsManage();
    /// <summary>
    /// Enable currentValue camera View
    /// </summary>
    public void Enable();
    /// <summary>
    ///  Disable currentValue camera View
    /// </summary>
    public void Disable();
    /// <summary>
    /// Is currentValue camera View enabled
    /// </summary>
    /// <returns></returns>
    public bool IsEnabled();
}

