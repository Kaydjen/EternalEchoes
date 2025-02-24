using UnityEngine;
using UnityEngine.Events;

[ComponentInfo("Player input manager",
    "Handles player input across multiple control schemes, " +
    "processes movement and camera actions, and invokes relevant events.")]
public class InputHandler : MonoBehaviour
{
    // Reference to the main input actions asset that contains all input mappings
    private InputActions _asset;
    // Instance of class
    public static InputHandler Instance;

    #region Maps 
    private InputActions.FPVActions _FPV;
    private InputActions.TopDownActions _TopDown;
    private InputActions.IsometricActions _Isometric;
    
    #endregion Maps
    #region Static variables
    public static Vector2 WASDInput { get; private set; } // Movement
    public static Vector2 MouseInput { get; private set; } // Mouse movement
    public static UnityEvent OnInteraction { get; private set; } = new UnityEvent(); // E - Interaction
    public static UnityEvent OnCPressed { get; private set; } = new UnityEvent(); // C - Switch сamera
    public static Vector2 WheelRotate { get; private set; } // Mouse wheel rotate value
    public static UnityEvent OnWheelRotate { get; private set; } = new UnityEvent(); // Mouse wheel rotate event
    public static UnityEvent OnWheelPerformed { get; private set; } = new UnityEvent();
    public static UnityEvent OnWheelCanceled { get; private set; } = new UnityEvent();
    #region numbers
    public static UnityEvent OnOne { get; private set; } = new UnityEvent();
    public static UnityEvent OnTwo { get; private set; } = new UnityEvent();
    public static UnityEvent OnThree { get; private set; } = new UnityEvent();
    public static UnityEvent OnFour { get; private set; } = new UnityEvent();
    public static UnityEvent OnFive { get; private set; } = new UnityEvent();
    public static UnityEvent OnSix { get; private set; } = new UnityEvent();
    public static UnityEvent OnSeven { get; private set; } = new UnityEvent();
    public static UnityEvent OnEight { get; private set; } = new UnityEvent();
    public static UnityEvent OnNine { get; private set; } = new UnityEvent();
    public static UnityEvent OnZero { get; private set; } = new UnityEvent();
    #endregion
    public static UnityEvent OnSwitchCharacter { get; private set; } = new UnityEvent();
    /*    public static UnityEvent OnWheelClickPerformed { get; private set; } = new UnityEvent();
        public static UnityEvent OnWheelClickCanceled { get; private set; } = new UnityEvent();*/
    #endregion Static variables
    #region Init methods
    public void Init()
    {
        ExclusivityСheck(); // singlton
        _asset = new InputActions();

        // 0
        // Initialize input action maps for different camera perspectives
        _FPV = _asset.FPV;           // Input action map for First-Person View
        _TopDown = _asset.TopDown;   // Input action map for Top-Down View
        _Isometric = _asset.Isometric; // Input action map for Isometric View
                                       // add here new maps...
        SubscribeToInputActions();
    }
    private void ExclusivityСheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance == this) return;
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void SubscribeToInputActions()    
    {
        // Subscribes to various actions
        _FPV.Movement.performed += _ => WASDInput = _.ReadValue<Vector2>();
        _TopDown.KeyboardCameraMovement.performed += _ => WASDInput = _.ReadValue<Vector2>();
        _Isometric.Movement.performed += _ => WASDInput = _.ReadValue<Vector2>();

        _FPV.Movement.canceled += _ => WASDInput = Vector2.zero;
        _TopDown.KeyboardCameraMovement.canceled += _ => WASDInput = Vector2.zero;
        _Isometric.Movement.canceled += _ => WASDInput = Vector2.zero;

        _FPV.RotateCamera.performed += _ => MouseInput = _.ReadValue<Vector2>();
        _TopDown.ScreenEdgePanning.performed += _ => MouseInput = _.ReadValue<Vector2>();
        _Isometric.Mouse.performed += _ => MouseInput = _.ReadValue<Vector2>();

        _FPV.RotateCamera.canceled += _ => MouseInput = Vector2.zero;
        _TopDown.ScreenEdgePanning.canceled += _ => MouseInput = Vector2.zero;
        _Isometric.Mouse.canceled += _ => MouseInput = Vector2.zero;

        _FPV.Interaction.performed += _ => OnInteraction.Invoke();
        _TopDown.Interaction.performed += _ => OnInteraction.Invoke();
        _Isometric.Interaction.performed += _ => OnInteraction.Invoke();

        _FPV.SwitchCamera.performed += _ => OnCPressed.Invoke();
        _TopDown.SwitchCamera.performed += _ => OnCPressed.Invoke();
        _Isometric.SwitchCamera.performed += _ => OnCPressed.Invoke();

        _TopDown.ZoomCamera.performed += _ => WheelRotate = _.ReadValue<Vector2>();
        _TopDown.ZoomCamera.canceled += _ => WheelRotate = Vector2.zero;

        _TopDown.ZoomCamera.performed += _ => OnWheelRotate.Invoke();

        _TopDown.ClickAndDrag.performed += _ => OnWheelPerformed.Invoke();
        _TopDown.ClickAndDrag.canceled += _ => OnWheelCanceled.Invoke();

        #region numbers
        _FPV.One.performed += _ => OnOne.Invoke();
        _FPV.Two.performed += _ => OnTwo.Invoke();
        _FPV.Three.performed += _ => OnThree.Invoke();
        _FPV.Four.performed += _ => OnFour.Invoke();
        _FPV.Five.performed += _ => OnFive.Invoke();
        _FPV.Six.performed += _ => OnSix.Invoke();
        _FPV.Seven.performed += _ => OnSeven.Invoke();
        _FPV.Nine.performed += _ => OnNine.Invoke();
        _FPV.Zero.performed += _ => OnZero.Invoke();

        _Isometric.One.performed += _ => OnOne.Invoke();
        _Isometric.Two.performed += _ => OnTwo.Invoke();
        _Isometric.Three.performed += _ => OnThree.Invoke();
        _Isometric.Four.performed += _ => OnFour.Invoke();
        _Isometric.Five.performed += _ => OnFive.Invoke();
        _Isometric.Six.performed += _ => OnSix.Invoke();
        _Isometric.Seven.performed += _ => OnSeven.Invoke();
        _Isometric.Nine.performed += _ => OnNine.Invoke();
        _Isometric.Zero.performed += _ => OnZero.Invoke();

        _TopDown.One.performed += _ => OnOne.Invoke();
        _TopDown.Two.performed += _ => OnTwo.Invoke();
        _TopDown.Three.performed += _ => OnThree.Invoke();
        _TopDown.Four.performed += _ => OnFour.Invoke();
        _TopDown.Five.performed += _ => OnFive.Invoke();
        _TopDown.Six.performed += _ => OnSix.Invoke();
        _TopDown.Seven.performed += _ => OnSeven.Invoke();
        _TopDown.Nine.performed += _ => OnNine.Invoke();
        _TopDown.Zero.performed += _ => OnZero.Invoke();
        #endregion

        _FPV.SwitchCharacter.performed += _ => OnSwitchCharacter.Invoke();
        _Isometric.SwitchCharacter.performed += _ => OnSwitchCharacter.Invoke();
        _TopDown.SwitchCharacter.performed += _ => OnSwitchCharacter.Invoke();

        /*        _TopDown.WheelClick.performed += _ => OnWheelClickPerformed.Invoke();
                _TopDown.WheelClick.canceled += _ => OnWheelClickCanceled.Invoke();*/
        // Add here a new one
    }
    #endregion Init methods
    #region Public methods
    // 0
    // Public methods to enable or disable input action maps        
    /// <summary>
    ///  Enable or disable the First-Person View input map
    /// </summary>
    /// <param name="state"> true - enable, false - disable</param>
    public void SetFPVState(bool state)
    {
        DisableAllMaps();
        if (state) _FPV.Enable();
        else _FPV.Disable();
    }
    // Public methods to enable or disable input action maps        
    /// <summary>
    ///  Enable or disable the Top-Down View input map
    /// </summary>
    /// <param name="state"> true - enable, false - disable</param>
    public void SetTopDownState(bool state)
    {
        DisableAllMaps();
        if (state) _TopDown.Enable();
        else _TopDown.Disable();
    }
    // Public methods to enable or disable input action maps        
    /// <summary>
    ///  Enable or disable the Isometric View input map
    /// </summary>
    /// <param name="state"> true - enable, false - disable</param>
    public void SetIsometricState(bool state)
    {
        DisableAllMaps();
        if (state) _Isometric.Enable();
        else _Isometric.Disable();
    }
    // add here new map's State...
    #endregion
    private void DisableAllMaps()
    {
        _FPV.Disable();
        _TopDown.Disable();
        _Isometric.Disable();
    }
}

/* Instruction
 
*** New Map ***
*Go to r.Maps and add a new one there*
*Go to f.Init.0 and init there new map as in example*
*Go to r.Public methods.0 and add here a State for new map* 
*Go to f.DisableAllMaps and write there as in example*
*Go to c.CameraSwitcher to f.Switcher and increase there value, also add new case in Switch-Case and new function like there already exest*
***         ***

*** New Input Event ***
*Go to r.StaticVariables and add there new variable
*Go to f.SubscribeToInputActions and add subscribe new value's change on input event*
***                 ***
 
 */

/*
 
     public void SetFPVState()
    {
        SetState(ref _FPV);
    }
    public void SetState(ref InputActionMap map)
    {
        if(_currentAction == null)
        {
            _currentAction = map;
        }
        else
        {
            if (_currentAction == map) return;
            _currentAction.Disable();
            _currentAction = map;
            _currentAction.Enable();
        }
    }
 
 */