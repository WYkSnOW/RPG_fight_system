//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Input/CharacterInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CharacterInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""62ee6aa7-62fd-4cc8-b248-a477380be08b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""bbda2620-a122-48ce-8aa6-10894a61d6fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraLook"",
                    ""type"": ""Value"",
                    ""id"": ""ae5defdb-4b00-4d3e-b677-f6873eab31ad"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run "",
                    ""type"": ""Button"",
                    ""id"": ""b6afb287-b6aa-4afb-be99-2723d12744ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""b2033b6c-443b-4382-a743-cdf06aefa7fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4a67b277-e142-4f9b-9010-6384ceacb511"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Execute"",
                    ""type"": ""Button"",
                    ""id"": ""34d9f735-14c7-4417-b25f-42ed8a79038e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""809c530d-7d91-4391-b4e3-9db74b53254e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""64c89c86-af92-42db-a9ed-e17c1fd3a58f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-0.1,max=0.1),Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""L_AtK"",
                    ""type"": ""Button"",
                    ""id"": ""7f74e8e4-7ca4-4b3c-9b38-89d63c625518"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""R_Atk"",
                    ""type"": ""Button"",
                    ""id"": ""9521b179-e5c3-4d99-a6b9-ad5ceec43c82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""c054dd1f-25b4-4192-9445-85bb6ca25a29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Continue_Atk"",
                    ""type"": ""Button"",
                    ""id"": ""916ed978-71e2-4ac7-9aad-9bd38b0b8565"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill"",
                    ""type"": ""Button"",
                    ""id"": ""bac84eb2-c7bc-4796-b20f-12b0c01d44a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FinishSkill"",
                    ""type"": ""Button"",
                    ""id"": ""0ffaedb5-b616-49b1-9b8d-6fc54d26a182"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""ea2be348-f94a-48b2-a464-967ca4f0bc95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""871e9967-3ea9-4446-a1fe-f2a8d0b4e9c7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""900b0939-d67f-436d-ad54-aeeea114cfdb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1b425890-c04d-46a8-84e4-248f10de52cf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""04f3d9a4-50f2-4165-8c82-9ef885679df2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e4d7591b-2698-4afc-912e-81d4c6ffd7f5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0ef1fd49-869b-400f-905e-703e9df47d21"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2"",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03a0ebc3-4ed8-45b7-a7ed-96c5b256b526"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run "",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccad51a5-745e-4e66-a77c-6de94c316555"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98582167-a12e-49c8-b917-bf7ed65c88a0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4a8e339-8389-4bfa-9779-884a44cd4ec6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Execute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3715a130-9b80-439a-a574-eee36dba4aac"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fa4563e-5d53-485b-a1a7-5e419edbc420"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e07fb3fe-bf85-4bab-adac-71b068398b33"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L_AtK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b83734e-7867-481e-9c60-97bdc9ab7f67"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R_Atk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34d59d8b-a117-48aa-902c-92c6bad262d7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c66d6ad-b28f-448d-a160-fb5140c9fa0a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue_Atk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5df8f07-0532-430b-8abc-c377f241cf3e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5061eaff-73f8-40d1-89f0-02afb0790844"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FinishSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9088e8b9-e717-40a6-a6fa-dad2da892f00"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SwitchSkill"",
            ""id"": ""3fe15299-c219-45a4-85ba-b4173cf27a89"",
            ""actions"": [
                {
                    ""name"": ""L"",
                    ""type"": ""Button"",
                    ""id"": ""54c61ebb-41ce-4b32-874a-fab67ac10a10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""R"",
                    ""type"": ""Button"",
                    ""id"": ""a6057f67-c5b1-4d4b-bbef-d1778f2dbec6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91ed39d8-792f-4946-97be-0d2aa74a0b4f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b48c9ad8-4771-4f52-84db-4f143e56bda2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_CameraLook = m_Player.FindAction("CameraLook", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run ", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Execute = m_Player.FindAction("Execute", throwIfNotFound: true);
        m_Player_SwitchCharacter = m_Player.FindAction("SwitchCharacter", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_L_AtK = m_Player.FindAction("L_AtK", throwIfNotFound: true);
        m_Player_R_Atk = m_Player.FindAction("R_Atk", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_Continue_Atk = m_Player.FindAction("Continue_Atk", throwIfNotFound: true);
        m_Player_Skill = m_Player.FindAction("Skill", throwIfNotFound: true);
        m_Player_FinishSkill = m_Player.FindAction("FinishSkill", throwIfNotFound: true);
        m_Player_Walk = m_Player.FindAction("Walk", throwIfNotFound: true);
        // SwitchSkill
        m_SwitchSkill = asset.FindActionMap("SwitchSkill", throwIfNotFound: true);
        m_SwitchSkill_L = m_SwitchSkill.FindAction("L", throwIfNotFound: true);
        m_SwitchSkill_R = m_SwitchSkill.FindAction("R", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_CameraLook;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Execute;
    private readonly InputAction m_Player_SwitchCharacter;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_L_AtK;
    private readonly InputAction m_Player_R_Atk;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_Continue_Atk;
    private readonly InputAction m_Player_Skill;
    private readonly InputAction m_Player_FinishSkill;
    private readonly InputAction m_Player_Walk;
    public struct PlayerActions
    {
        private @CharacterInput m_Wrapper;
        public PlayerActions(@CharacterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @CameraLook => m_Wrapper.m_Player_CameraLook;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Execute => m_Wrapper.m_Player_Execute;
        public InputAction @SwitchCharacter => m_Wrapper.m_Player_SwitchCharacter;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @L_AtK => m_Wrapper.m_Player_L_AtK;
        public InputAction @R_Atk => m_Wrapper.m_Player_R_Atk;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @Continue_Atk => m_Wrapper.m_Player_Continue_Atk;
        public InputAction @Skill => m_Wrapper.m_Player_Skill;
        public InputAction @FinishSkill => m_Wrapper.m_Player_FinishSkill;
        public InputAction @Walk => m_Wrapper.m_Player_Walk;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @CameraLook.started += instance.OnCameraLook;
            @CameraLook.performed += instance.OnCameraLook;
            @CameraLook.canceled += instance.OnCameraLook;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Execute.started += instance.OnExecute;
            @Execute.performed += instance.OnExecute;
            @Execute.canceled += instance.OnExecute;
            @SwitchCharacter.started += instance.OnSwitchCharacter;
            @SwitchCharacter.performed += instance.OnSwitchCharacter;
            @SwitchCharacter.canceled += instance.OnSwitchCharacter;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @L_AtK.started += instance.OnL_AtK;
            @L_AtK.performed += instance.OnL_AtK;
            @L_AtK.canceled += instance.OnL_AtK;
            @R_Atk.started += instance.OnR_Atk;
            @R_Atk.performed += instance.OnR_Atk;
            @R_Atk.canceled += instance.OnR_Atk;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Continue_Atk.started += instance.OnContinue_Atk;
            @Continue_Atk.performed += instance.OnContinue_Atk;
            @Continue_Atk.canceled += instance.OnContinue_Atk;
            @Skill.started += instance.OnSkill;
            @Skill.performed += instance.OnSkill;
            @Skill.canceled += instance.OnSkill;
            @FinishSkill.started += instance.OnFinishSkill;
            @FinishSkill.performed += instance.OnFinishSkill;
            @FinishSkill.canceled += instance.OnFinishSkill;
            @Walk.started += instance.OnWalk;
            @Walk.performed += instance.OnWalk;
            @Walk.canceled += instance.OnWalk;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @CameraLook.started -= instance.OnCameraLook;
            @CameraLook.performed -= instance.OnCameraLook;
            @CameraLook.canceled -= instance.OnCameraLook;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Execute.started -= instance.OnExecute;
            @Execute.performed -= instance.OnExecute;
            @Execute.canceled -= instance.OnExecute;
            @SwitchCharacter.started -= instance.OnSwitchCharacter;
            @SwitchCharacter.performed -= instance.OnSwitchCharacter;
            @SwitchCharacter.canceled -= instance.OnSwitchCharacter;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @L_AtK.started -= instance.OnL_AtK;
            @L_AtK.performed -= instance.OnL_AtK;
            @L_AtK.canceled -= instance.OnL_AtK;
            @R_Atk.started -= instance.OnR_Atk;
            @R_Atk.performed -= instance.OnR_Atk;
            @R_Atk.canceled -= instance.OnR_Atk;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Continue_Atk.started -= instance.OnContinue_Atk;
            @Continue_Atk.performed -= instance.OnContinue_Atk;
            @Continue_Atk.canceled -= instance.OnContinue_Atk;
            @Skill.started -= instance.OnSkill;
            @Skill.performed -= instance.OnSkill;
            @Skill.canceled -= instance.OnSkill;
            @FinishSkill.started -= instance.OnFinishSkill;
            @FinishSkill.performed -= instance.OnFinishSkill;
            @FinishSkill.canceled -= instance.OnFinishSkill;
            @Walk.started -= instance.OnWalk;
            @Walk.performed -= instance.OnWalk;
            @Walk.canceled -= instance.OnWalk;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // SwitchSkill
    private readonly InputActionMap m_SwitchSkill;
    private List<ISwitchSkillActions> m_SwitchSkillActionsCallbackInterfaces = new List<ISwitchSkillActions>();
    private readonly InputAction m_SwitchSkill_L;
    private readonly InputAction m_SwitchSkill_R;
    public struct SwitchSkillActions
    {
        private @CharacterInput m_Wrapper;
        public SwitchSkillActions(@CharacterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @L => m_Wrapper.m_SwitchSkill_L;
        public InputAction @R => m_Wrapper.m_SwitchSkill_R;
        public InputActionMap Get() { return m_Wrapper.m_SwitchSkill; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SwitchSkillActions set) { return set.Get(); }
        public void AddCallbacks(ISwitchSkillActions instance)
        {
            if (instance == null || m_Wrapper.m_SwitchSkillActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SwitchSkillActionsCallbackInterfaces.Add(instance);
            @L.started += instance.OnL;
            @L.performed += instance.OnL;
            @L.canceled += instance.OnL;
            @R.started += instance.OnR;
            @R.performed += instance.OnR;
            @R.canceled += instance.OnR;
        }

        private void UnregisterCallbacks(ISwitchSkillActions instance)
        {
            @L.started -= instance.OnL;
            @L.performed -= instance.OnL;
            @L.canceled -= instance.OnL;
            @R.started -= instance.OnR;
            @R.performed -= instance.OnR;
            @R.canceled -= instance.OnR;
        }

        public void RemoveCallbacks(ISwitchSkillActions instance)
        {
            if (m_Wrapper.m_SwitchSkillActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISwitchSkillActions instance)
        {
            foreach (var item in m_Wrapper.m_SwitchSkillActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SwitchSkillActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SwitchSkillActions @SwitchSkill => new SwitchSkillActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraLook(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnExecute(InputAction.CallbackContext context);
        void OnSwitchCharacter(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnL_AtK(InputAction.CallbackContext context);
        void OnR_Atk(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnContinue_Atk(InputAction.CallbackContext context);
        void OnSkill(InputAction.CallbackContext context);
        void OnFinishSkill(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
    }
    public interface ISwitchSkillActions
    {
        void OnL(InputAction.CallbackContext context);
        void OnR(InputAction.CallbackContext context);
    }
}
