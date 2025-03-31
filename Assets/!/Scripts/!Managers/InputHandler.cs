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
    private InputActions.DefNumbersActions _DefNumbers;
    private InputActions.OptionsNumbersActions _OptionsNumbers;
    private InputActions.AllActions _All;
    private InputActions.UIActions _UI;

    #endregion Maps
    #region Static variables
    public static Vector2 WASDInput { get; private set; } // Movement
    public static Vector2 MouseInput { get; private set; } // Mouse movement
    public static UnityEvent OnInteractionPress { get; private set; } = new UnityEvent(); // E - Interaction
    public static UnityEvent OnInteractionHoldPerformed { get; private set; } = new UnityEvent(); 
    public static UnityEvent OnInteractionHoldReleased { get; private set; } = new UnityEvent(); 
    public static UnityEvent OnCPressed { get; private set; } = new UnityEvent(); // C - Switch сamera
    public static Vector2 WheelRotate { get; private set; } // Mouse wheel rotate value
    public static UnityEvent OnWheelRotate { get; private set; } = new UnityEvent(); // Mouse wheel rotate event
    public static UnityEvent OnWheelPerformed { get; private set; } = new UnityEvent();
    public static UnityEvent OnWheelCanceled { get; private set; } = new UnityEvent();
    #region numbers
    public static UnityEvent OnDefOne { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefTwo { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefThree { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefFour { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefFive { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefSix { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefSeven { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefEight { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefNine { get; private set; } = new UnityEvent();
    public static UnityEvent OnDefZero { get; private set; } = new UnityEvent();

    public static UnityEvent OnOptionsOne { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsTwo { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsThree { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsFour { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsFive { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsSix { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsSeven { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsEight { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsNine { get; private set; } = new UnityEvent();
    public static UnityEvent OnOptionsZero { get; private set; } = new UnityEvent();
    #endregion
    public static UnityEvent OnGetCharacterInfo { get; private set; } = new UnityEvent();
    /*    public static UnityEvent OnWheelClickPerformed { get; private set; } = new UnityEvent();
        public static UnityEvent OnWheelClickCanceled { get; private set; } = new UnityEvent();*/
    public static UnityEvent OnAltF { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackLMB { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackLMBPerformed { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackLMBReleased { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackRMB { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackRMBPerformed { get; private set; } = new UnityEvent();
    public static UnityEvent OnAttackRMBReleased { get; private set; } = new UnityEvent();
    public static UnityEvent OnEnterAimingMode { get; private set; } = new UnityEvent();
    public static UnityEvent OnExitAimingMode { get; private set; } = new UnityEvent();
    public static UnityEvent OnReload { get; private set; } = new UnityEvent();

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
        _DefNumbers = _asset.DefNumbers;
        _OptionsNumbers = _asset.OptionsNumbers;
        _All = _asset.All;
        _UI = _asset.UI;
                                    // add here new maps...

        SubscribeToInputActions();
        ActivateDefNumbersMap();
        SetAllState(true);
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

        _FPV.SwitchCamera.performed += _ => OnCPressed.Invoke();
        _TopDown.SwitchCamera.performed += _ => OnCPressed.Invoke();
        _Isometric.SwitchCamera.performed += _ => OnCPressed.Invoke();

        _TopDown.ZoomCamera.performed += _ => WheelRotate = _.ReadValue<Vector2>();
        _TopDown.ZoomCamera.canceled += _ => WheelRotate = Vector2.zero;

        _TopDown.ZoomCamera.performed += _ => OnWheelRotate.Invoke();

        _TopDown.ClickAndDrag.performed += _ => OnWheelPerformed.Invoke();
        _TopDown.ClickAndDrag.canceled += _ => OnWheelCanceled.Invoke();

        #region numbers
        _DefNumbers.One.performed += _ => OnDefOne.Invoke();
        _DefNumbers.Two.performed += _ => OnDefTwo.Invoke();
        _DefNumbers.Three.performed += _ => OnDefThree.Invoke();
        _DefNumbers.Four.performed += _ => OnDefFour.Invoke();
        _DefNumbers.Five.performed += _ => OnDefFive.Invoke();
        _DefNumbers.Six.performed += _ => OnDefSix.Invoke();
        _DefNumbers.Seven.performed += _ => OnDefSeven.Invoke();
        _DefNumbers.Eight.performed += _ => OnDefEight.Invoke();
        _DefNumbers.Nine.performed += _ => OnDefNine.Invoke();
        _DefNumbers.Zero.performed += _ => OnDefZero.Invoke();

        _OptionsNumbers.One.performed += _ => OnOptionsOne.Invoke();
        _OptionsNumbers.Two.performed += _ => OnOptionsTwo.Invoke();
        _OptionsNumbers.Three.performed += _ => OnOptionsThree.Invoke();
        _OptionsNumbers.Four.performed += _ => OnOptionsFour.Invoke();
        _OptionsNumbers.Five.performed += _ => OnOptionsFive.Invoke();
        _OptionsNumbers.Six.performed += _ => OnOptionsSix.Invoke();
        _OptionsNumbers.Seven.performed += _ => OnOptionsSeven.Invoke();
        _OptionsNumbers.Eight.performed += _ => OnOptionsEight.Invoke();
        _OptionsNumbers.Nine.performed += _ => OnOptionsNine.Invoke();
        _OptionsNumbers.Zero.performed += _ => OnOptionsZero.Invoke();
        #endregion

        _FPV.SwitchCharacter.performed += _ => OnGetCharacterInfo.Invoke();
        _Isometric.SwitchCharacter.performed += _ => OnGetCharacterInfo.Invoke();
        _TopDown.SwitchCharacter.performed += _ => OnGetCharacterInfo.Invoke();

        /*        _TopDown.WheelClick.performed += _pool => OnWheelClickPerformed.Invoke();
                _TopDown.WheelClick.canceled += _pool => OnWheelClickCanceled.Invoke();*/

        _All.AltF.performed += _ => OnAltF.Invoke();

        _FPV.AttackLMB.started += _ => OnAttackLMB.Invoke();
        _Isometric.AttackLMB.started += _ => OnAttackLMB.Invoke();
        _FPV.AttackLMB.performed += _ => OnAttackLMBPerformed.Invoke();
        _Isometric.AttackLMB.performed += _ => OnAttackLMBPerformed.Invoke();
        _FPV.AttackLMB.canceled += _ => OnAttackLMBReleased.Invoke();
        _Isometric.AttackLMB.canceled += _ => OnAttackLMBReleased.Invoke();

        _FPV.AttackRMB.started += _ => OnAttackRMB.Invoke();
        _Isometric.AttackRMB.started += _ => OnAttackRMB.Invoke();
        _FPV.AttackRMB.performed += _ => OnAttackRMBPerformed.Invoke();
        _Isometric.AttackRMB.performed += _ => OnAttackRMBPerformed.Invoke();
        _FPV.AttackRMB.canceled += _ => OnAttackRMBReleased.Invoke();
        _Isometric.AttackRMB.canceled += _ => OnAttackRMBReleased.Invoke();

        _FPV.AimRMB.performed += _ => OnEnterAimingMode.Invoke();
        _Isometric.AimRMB.performed += _ => OnEnterAimingMode.Invoke();
        _FPV.AimRMB.canceled += _ => OnExitAimingMode.Invoke();
        _Isometric.AimRMB.canceled += _ => OnExitAimingMode.Invoke();

        #region Interact
        _FPV.Interaction.performed += _ => OnInteractionPress.Invoke();
        _TopDown.Interaction.started += _ => OnInteractionPress.Invoke();
        _Isometric.Interaction.performed += _ => OnInteractionPress.Invoke();

        _TopDown.Interaction.performed += _ => OnInteractionHoldPerformed.Invoke();
        _TopDown.Interaction.canceled += _ => OnInteractionHoldReleased.Invoke();
        #endregion

        _FPV.Reload.performed += _ => OnReload.Invoke();
        _Isometric.Reload.performed += _ => OnReload.Invoke();
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
    /// <summary>
    ///  Enable the DefNumbers input map
    /// </summary>
    public void ActivateDefNumbersMap()
    {
        _DefNumbers.Enable();
        _OptionsNumbers.Disable();
    }
    /// <summary>
    ///  Enable the OptionsNumbers input map
    /// </summary>
    public void ActivateOptionsNumbersMap()
    {
        _OptionsNumbers.Enable();
        _DefNumbers.Disable();
    }
    /// <summary>
    ///   Enable or disable the movements of all input maps 
    /// </summary>
    /// <param name="state">true - enable, false - disable</param>
    public void SetAllMovement(bool state)
    {
        if (state)
        {
            _FPV.Movement.Enable();
            _TopDown.KeyboardCameraMovement.Enable();
            _Isometric.Movement.Enable();
        }
        else
        {
            _FPV.Movement.Disable();
            _TopDown.KeyboardCameraMovement.Disable();
            _Isometric.Movement.Disable();
        }
    }
    /// <summary>
    ///   Enable or disable the attack's ability of all input maps 
    /// </summary>
    /// <param name="state">true - enable, false - disable</param>
    public void SetAllAttack(bool state)
    {
        if (state)
        {
            _FPV.AttackLMB.Enable();
            _Isometric.AttackLMB.Enable();
        }
        else
        {
            _FPV.AttackLMB.Disable();
            _Isometric.AttackLMB.Disable();
        }
    }
    /// <summary>
    ///   Enable or disable the aim's ability of all input maps 
    /// </summary>
    /// <param name="state">true - enable, false - disable</param>
    public void SetAllAim(bool state)
    {
        if (state)
        {
            _FPV.AimRMB.Enable();
            _Isometric.AimRMB.Enable();
        }
        else
        {
            _FPV.AimRMB.Disable();
            _Isometric.AimRMB.Disable();
        }
    }
    /// <summary>
    ///   Enable or disable the aim's ability of all input maps 
    /// </summary>
    /// <param name="state">true - enable, false - disable</param>
    public void SetAllInteract(bool state)
    {
        if (state)
        {
            if(CameraSwitcher.Instance.GetViewType() is FPV)
                _FPV.Interaction.Enable();
            if (CameraSwitcher.Instance.GetViewType() is IsometricV)
                _Isometric.Interaction.Enable();
            if (CameraSwitcher.Instance.GetViewType() is TopDownV)
                _TopDown.Interaction.Enable();
        }
        else
        {
            _FPV.Interaction.Disable();
            _Isometric.Interaction.Disable();
            _TopDown.Interaction.Disable();
        }
    }
    /// <summary>
    ///  Enable or disable the All View input map
    /// </summary>
    /// <param name="state"> true - enable, false - disable</param>
    public void SetAllState(bool state)
    {
        if (state) _All.Enable();
        else _All.Disable();
    }
    /// <summary>
    ///  Enable or disable the UI input map
    /// </summary>
    /// <param name="state"> true - enable, false - disable</param>
    public void SetUIState(bool state)
    {
        if (state) _UI.Enable();
        else _UI.Disable();
    }
    // add here new map's State...
    #endregion
    private void DisableAllMaps()
    {
        _FPV.Disable();
        _TopDown.Disable();
        _Isometric.Disable();
        /*        _DefNumbers.Disable(); those don't touch
                _Isometric.Disable();*/
        //_All.Disable();
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
 * Это заметки, что бы понятно было что и зачем я сделал
 26.02.25 
Сегодня добавил две новых мапы чисто для цифр (DefNumbers & OptionsNumbers). 
В Init активируется DefNumbers map 
Добавлены два метода в   r.Public methods.0 которе чисто врубают одно и вырубают другое, то есть всегда будет включена одна из двух мап
P.S. сделано это на будущее, что бы было удобнее работать с цифрами во время игры и во время выбора действий с персонажем
 
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