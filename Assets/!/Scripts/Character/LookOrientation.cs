using UnityEngine;

[ComponentInfo("", "Для того, щоб гравець дивився та йшов куди треба. Поворот залежить вiд камери")]
public class LookOrientation : MonoBehaviour, IUpdate
{
    #region VARIABLES
    public static Vector2 LookDirection { get; private set; }
    public static Transform Direction { get; private set; }
    private Transform _playerBoth;
    #endregion

    #region PRIVATE METHODS
    private void GetPlayer()
    {
        _playerBoth = PlayerCore.Instance.transform.GetChild(Constants.Player.BOTH).transform;
    }

    Vector2 GetRelativeDirection(Vector2 v1, Vector2 v2)
    {
        v1.Normalize();
        v2.Normalize();

        Vector2 yAxis = new Vector2(-v1.y, v1.x);

        float x = Vector2.Dot(v2, v1);
        float y = Vector2.Dot(v2, yAxis);

        return new Vector2(x, y);
    }


    #endregion

    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PerformPreUpdate()
    {
        LookDirection = (this.transform.GetChild(0).position - this.transform.position).normalized;

        LookDirection = GetRelativeDirection(LookDirection, InputManager.WASDInput);
        //Debug.Log(LookDirection);
        _playerBoth.localRotation = this.transform.rotation;
    }

    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }

    private void RegisterUpdate()
    {
        Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
    }

    private void UnregisterUpdate()
    {
        Updater.Instance?.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
    }
    #endregion

    #region MONO METHODS
    public void Start()
    {
        if (PlayerCore.Instance != null) GetPlayer();
        else InitEvents.OnFirstCharacterInit?.AddListener(GetPlayer);
        Direction = this.transform;
    }

    private void OnEnable()
    {
        if(Updater.Instance != null) RegisterUpdate();
        else InitEvents.OnUpdateInit?.AddListener(RegisterUpdate);

        GameEvents.OnCharacterChange?.AddListener(GetPlayer);
    }

    private void OnDisable()
    {
        UnregisterUpdate();
        GameEvents.OnCharacterChange?.RemoveListener(GetPlayer);
    }
    #endregion
}
