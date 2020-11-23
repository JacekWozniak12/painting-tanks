// GENERATED AUTOMATICALLY FROM 'Assets/PaintingTanks/Settings/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""bfaaf809-3501-4169-ad26-c80c2b8bf431"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""dacb4caa-8b8e-482c-bf1f-8f4a5ba501a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""02d10fdf-08c6-4299-b0de-c8e76227e898"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""6855cf2f-9cc3-496c-9f8a-86fe74ec2656"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""76860534-1a84-4d3e-a96d-24fd61265a98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FindTarget"",
                    ""type"": ""Value"",
                    ""id"": ""7f8eb9bf-4131-4853-90bd-17764d01e7f7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7e468415-7ec9-45a5-a361-b61232e1b1fe"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WSAD"",
                    ""id"": ""8b3ee935-6181-4943-9515-88ab49e9daad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""30dc7385-097d-44ac-968f-a6bbb793b606"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""04c165c4-0c14-49fd-9e55-a4aa6b6fc318"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9e0c072a-0d07-4a07-860a-f1430ce7a540"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6a744b88-155c-4424-aa33-f02d2bfb906f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""4970d2b9-e31e-4177-bdde-a4d7764a8d3d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b958b495-633b-4543-8f64-130e8ce8412c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""32c8c63e-b77b-4f1f-81fd-df7be8abe5c5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""794abee5-d7c1-4b77-bc40-5a4cbd2aa101"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""187a2e66-2631-45d3-bf05-f48028e051a0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MouseScroll"",
                    ""id"": ""e4c418eb-0306-42cc-a595-e21a40cfed78"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ed156b7f-d9d9-4718-85fe-400bcb9c507a"",
                    ""path"": ""<Mouse>/scroll/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e9acb047-20ed-41e9-ba53-f48c574dcedb"",
                    ""path"": ""<Mouse>/scroll/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5d507178-fb09-4e08-b061-d8fba5a83554"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7062e38e-40d8-47e8-89bf-9100d81806e9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FindTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7228f106-bc8e-44d3-a104-380e61d97af8"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""904deb95-12e3-4f76-bd26-15987964f9e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""1411761b-b4fc-4bb4-831b-ff2a9660101c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mute"",
                    ""type"": ""Button"",
                    ""id"": ""6cb632a9-735a-47d6-b253-7b42ee51d18a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""48c5b900-94cf-4e70-aee3-9c96d40cebee"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e216a94d-def2-4090-b30c-450e0d9d3783"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5921472f-a3fa-4c01-87c8-c4f0cee523d0"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player_Keyboard"",
                    ""action"": ""Mute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player_Keyboard"",
            ""bindingGroup"": ""Player_Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Switch = m_Player.FindAction("Switch", throwIfNotFound: true);
        m_Player_Special = m_Player.FindAction("Special", throwIfNotFound: true);
        m_Player_FindTarget = m_Player.FindAction("FindTarget", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Quit = m_UI.FindAction("Quit", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
        m_UI_Mute = m_UI.FindAction("Mute", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Switch;
    private readonly InputAction m_Player_Special;
    private readonly InputAction m_Player_FindTarget;
    public struct PlayerActions
    {
        private @GameControls m_Wrapper;
        public PlayerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Switch => m_Wrapper.m_Player_Switch;
        public InputAction @Special => m_Wrapper.m_Player_Special;
        public InputAction @FindTarget => m_Wrapper.m_Player_FindTarget;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Switch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Special.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @FindTarget.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFindTarget;
                @FindTarget.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFindTarget;
                @FindTarget.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFindTarget;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @FindTarget.started += instance.OnFindTarget;
                @FindTarget.performed += instance.OnFindTarget;
                @FindTarget.canceled += instance.OnFindTarget;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Quit;
    private readonly InputAction m_UI_Pause;
    private readonly InputAction m_UI_Mute;
    public struct UIActions
    {
        private @GameControls m_Wrapper;
        public UIActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Quit => m_Wrapper.m_UI_Quit;
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputAction @Mute => m_Wrapper.m_UI_Mute;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Quit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Mute.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMute;
                @Mute.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMute;
                @Mute.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMute;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Mute.started += instance.OnMute;
                @Mute.performed += instance.OnMute;
                @Mute.canceled += instance.OnMute;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_Player_KeyboardSchemeIndex = -1;
    public InputControlScheme Player_KeyboardScheme
    {
        get
        {
            if (m_Player_KeyboardSchemeIndex == -1) m_Player_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Player_Keyboard");
            return asset.controlSchemes[m_Player_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnFindTarget(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnQuit(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMute(InputAction.CallbackContext context);
    }
}
