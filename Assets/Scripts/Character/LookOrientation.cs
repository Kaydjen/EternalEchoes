using UnityEngine;

[ComponentInfo("", "Для того, щоб гравець дивився та йшов куди треба. Поворот залежить вiд камери")]
public class LookOrientation : MonoBehaviour, IUpdate
{
    #region VARIABLES
    public static Vector3 Direction { get; private set; }
    private Vector3 _direction;
    private Transform _playerBoth;
    #endregion

    #region PRIVATE METHODS
    private void UpdateNeededComponents()
    {
        _playerBoth = PlayerCore.Instance.transform.GetChild(Constants.Player.BODY_ANIMATIONS).transform;
    }

    private Vector3 GetRelativePosition(Vector2 input, Vector2 forward)
    {
        // Нормализуем вектор направления
        forward.Normalize();

        // Вычисляем правый вектор (выбираем знак так, чтобы примеры совпадали)
        Vector2 right = new Vector2(-forward.y, forward.x);

        // Здесь предполагаем, что «вперёд» будет отрицательной компонентой
        float localForward = -Vector2.Dot(input, forward);
        float localRight = Vector2.Dot(input, right);

        return new Vector3(localForward, 0f, localRight);
    }

    #endregion

    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PerformPreUpdate()
    {
        // Вычисляем 3D-направление взгляда
        _direction = (this.transform.GetChild(0).position - this.transform.position).normalized;
        // Преобразуем его в 2D (используем оси X и Z)
        Vector2 forward2D = new Vector2(_direction.x, _direction.z);

        // Получаем относительную позицию
        Direction = GetRelativePosition(InputHandler.WASDInput, forward2D);

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
