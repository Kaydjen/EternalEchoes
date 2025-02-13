using UnityEngine;

[ComponentInfo("", "Для того, щоб гравець дивився та йшов куди треба. Поворот залежить вiд камери")]
public class LookOrientation : MonoBehaviour, IUpdate
{
    #region VARIABLES
    public static Vector2 Direction { get; private set; }
    private Transform _playerBoth;
    #endregion

    #region PRIVATE METHODS
    private void UpdateNeededComponents()
    {
        _playerBoth = PlayerCore.Instance.transform.GetChild(Constants.Player.BODY_ANIMATIONS).transform;
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
        Direction = (this.transform.GetChild(0).position - this.transform.position).normalized;

        Direction = GetRelativeDirection(Direction, InputHandler.WASDInput);
        Debug.Log(Direction);
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
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
    }

    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
    }
    #endregion

    #region MONO METHODS
    public void Init()
    {
        UpdateNeededComponents();
    }

    private void OnEnable()
    {
        RegisterUpdate();
        GameEvents.OnCharacterChange.AddListener(UpdateNeededComponents);
    }

    private void OnDisable()
    {
        UnregisterUpdate();
        GameEvents.OnCharacterChange.RemoveListener(UpdateNeededComponents);
    }
    #endregion
}
