using UnityEngine;

public class CamerasParametersUpdater : MonoBehaviour
{
    public void Awake()
    {
        GameEvents.OnCharacterChange.AddListener(ParametersUpdate);
    }
    private void OnDestroy()
    {
        GameEvents.OnCharacterChange.RemoveListener(ParametersUpdate);
    }

    /// <summary>
    /// Этот метод будет срабатывать при каждом переключении перса, дабы обновлять параметры камеры в зависимости от параметров указанных в плеере
    /// Создал интерфейс в CameraCore который будут наследовать все камеры и уже там будет происходить вся логика изменения (пока что)
    /// </summary>
    private void ParametersUpdate()
    {
        ICameraUpdate[] views = GetComponents<ICameraUpdate>();
        foreach (ICameraUpdate view in views)
        {
            view.UpdateNeededComponents();
        }
    }
}
