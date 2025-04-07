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
        if (CheckNull.Player()) return;      
        _playerBoth = PlayerCore.Instance.transform.GetChild(Constants.Player.BOTH).transform;
    }
    Vector2 GetRelativeDirection(Vector2 v1, Vector2 v2)
    {
        if (v1 == Vector2.zero || v2 == Vector2.zero)
            return Vector2.zero;

        v1.Normalize();
        v2.Normalize();

        Vector2 yAxis = new Vector2(-v1.y, v1.x);
        float x = Vector2.Dot(v2, v1);
        float y = Vector2.Dot(v2, yAxis);

        return new Vector2(x, y);
    }
    #endregion
    #region Update
    public void PerformInitialUpdate() { }
    public void PerformUpdate() { }
    public void PerformFinalUpdate() { }
    public void PerformLateUpdate() { }
    public void PerformPreUpdate()
    {
        LookDirection = (this.transform.GetChild(0).position - this.transform.position).normalized;

        LookDirection = GetRelativeDirection(LookDirection, InputManager.WASDInput);
        _playerBoth.localRotation = this.transform.rotation;
    }
    #endregion
    #region Registration
    private void RegisterUpdate()
    {
        Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
    }

    private void UnregisterUpdate()
    {
        Updater.Instance?.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
    }
    private void Register()
    {
        Debug.Log("1");
        if(Updater.Instance != null && PlayerCore.Instance != null)
        {
            Debug.Log("2");
            GetPlayer();
            RegisterUpdate();            
        }
        if (Updater.Instance == null)
        {
            Debug.Log("3");
            InitEvents.OnUpdateReady?.AddListener(Register);
        }
        if (PlayerCore.Instance == null) 
        {
            Debug.Log("4");
            InitEvents.OnFirstCharacterReady?.AddListener(Register);
        }
        if (Updater.Instance != null) 
        {
            Debug.Log("5");
            InitEvents.OnUpdateReady?.RemoveListener(Register);
        }
        if (PlayerCore.Instance != null) 
        {
            Debug.Log("6");
            InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
        }
    }
    #endregion
    #region MONO METHODS
    public void Awake()
    {
        Direction = this.transform;
    }

    private void OnEnable()
    {
        Register();
        GameEvents.OnCharacterChange?.AddListener(GetPlayer);
    }

    private void OnDisable()
    {
        UnregisterUpdate();
        InitEvents.OnUpdateReady?.RemoveListener(Register);
        GameEvents.OnCharacterChange?.RemoveListener(GetPlayer);
        InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
    }
    #endregion
}


/*
    #region Registration Methods
    private void RegisterUpdate()
    {
        if (Updater.Instance != null)
        {
            Updater.Instance.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
            _isInitialized = true;
        }
    }

    private void UnregisterUpdate()
    {
        if (Updater.Instance != null)
        {
            Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
        }
        _isInitialized = false;
    }

    private void Register()
    {
        if (_playerBoth != null)
        {
            RegisterUpdate();
            return;
        }

        if (PlayerCore.Instance != null && GetPlayer())
        {
            RegisterUpdate();
        }
        else
        {
            InitEvents.OnFirstCharacterReady?.AddListener(Register);
        }
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        Direction = transform;
        Initialize();
    }

    private void Initialize()
    {
        if (PlayerCore.Instance != null)
        {
            if (GetPlayer())
            {
                Register();
            }
        }
        else
        {
            InitEvents.OnFirstCharacterReady?.AddListener(Initialize);
        }
    }

    private void OnEnable()
    {
        if (Updater.Instance != null)
        {
            Register();
        }
        else
        {
            InitEvents.OnUpdateReady?.AddListener(() =>
            {
                if (this != null) Register();
            });
        }

        GameEvents.OnCharacterChange?.AddListener(() =>
        {
            if (this != null) GetPlayer();
        });
    }

    private void OnDisable()
    {
        UnregisterUpdate();
        InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
        InitEvents.OnUpdateReady?.RemoveListener(Register);
        GameEvents.OnCharacterChange?.RemoveListener(() => GetPlayer());
    }

    private void OnDestroy()
    {
        UnregisterUpdate();
        InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
        InitEvents.OnUpdateReady?.RemoveListener(Register);
        GameEvents.OnCharacterChange?.RemoveListener(() => GetPlayer());
    }
    #endregion
 
 
 */
/*
 
     #region VARIABLES
    public static Vector2 LookDirection { get; private set; }
    public static Transform Direction { get; private set; }
    private Transform _playerBoth;
    private bool _isInitialized;
    #endregion

    #region PRIVATE METHODS
    private bool GetPlayer()
    {
        if (PlayerCore.Instance == null)
        {
            Debug.LogWarning("PlayerCore.Instance is null!");
            return false;
        }

        if (PlayerCore.Instance.transform.childCount <= Constants.Player.BOTH)
        {
            Debug.LogError($"PlayerCore has no child at index {Constants.Player.BOTH}");
            return false;
        }

        _playerBoth = PlayerCore.Instance.transform.GetChild(Constants.Player.BOTH).transform;
        return _playerBoth != null;
    }

    Vector2 GetRelativeDirection(Vector2 v1, Vector2 v2)
    {
        if (v1 == Vector2.zero || v2 == Vector2.zero)
            return Vector2.zero;

        v1.Normalize();
        v2.Normalize();

        Vector2 yAxis = new Vector2(-v1.y, v1.x);
        float x = Vector2.Dot(v2, v1);
        float y = Vector2.Dot(v2, yAxis);

        return new Vector2(x, y);
    }
    #endregion

    #region Update Methods
    public void PerformPreUpdate()
    {
        if (!_isInitialized || _playerBoth == null)
            return;

        if (transform.childCount == 0)
        {
            Debug.LogError("No child object found for direction calculation!");
            return;
        }

        try
        {
            LookDirection = (transform.GetChild(0).position - transform.position).normalized;
            LookDirection = GetRelativeDirection(LookDirection, InputManager.WASDInput);
            _playerBoth.localRotation = transform.rotation;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in PerformPreUpdate: {e.Message}");
        }
    }
    public void PerformInitialUpdate() {}
    public void PerformUpdate() {}
    public void PerformFinalUpdate() {}
    public void PerformLateUpdate() {}
    #endregion

    #region Registration Methods
    private void RegisterUpdate()
    {
        if (Updater.Instance != null)
        {
            Updater.Instance.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
            _isInitialized = true;
        }
    }

    private void UnregisterUpdate()
    {
        if (Updater.Instance != null)
        {
            Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
        }
        _isInitialized = false;
    }

    private void Register()
    {
        if (_playerBoth != null)
        {
            RegisterUpdate();
            return;
        }

        if (PlayerCore.Instance != null && GetPlayer())
        {
            RegisterUpdate();
        }
        else
        {
            InitEvents.OnFirstCharacterReady?.AddListener(Register);
        }
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        Direction = transform;
        Initialize();
    }

    private void Initialize()
    {
        if (PlayerCore.Instance != null)
        {
            if (GetPlayer())
            {
                Register();
            }
        }
        else
        {
            InitEvents.OnFirstCharacterReady?.AddListener(Initialize);
        }
    }

    private void OnEnable()
    {
        if (Updater.Instance != null)
        {
            Register();
        }
        else
        {
            InitEvents.OnUpdateReady?.AddListener(() =>
            {
                if (this != null) Register();
            });
        }

        GameEvents.OnCharacterChange?.AddListener(() =>
        {
            if (this != null) GetPlayer();
        });
    }

    private void OnDisable()
    {
        UnregisterUpdate();
        InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
        InitEvents.OnUpdateReady?.RemoveListener(Register);
        GameEvents.OnCharacterChange?.RemoveListener(() => GetPlayer());
    }

    private void OnDestroy()
    {
        UnregisterUpdate();
        InitEvents.OnFirstCharacterReady?.RemoveListener(Register);
        InitEvents.OnUpdateReady?.RemoveListener(Register);
        GameEvents.OnCharacterChange?.RemoveListener(() => GetPlayer());
    }
    #endregion
 
 
 */