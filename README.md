
### CharacterController
- Uses the `CharacterController` `Move` method to implement character movement.
- Movement is achieved through root motion driven by the `OnAnimationMove` method at animation update frequency.

### OnAnimationMove
- Enables smooth movement by using root motion based on the animation update frequency provided by `OnAnimationMove`.

### Animator and State Machine Design
- Animation transitions and playback logic are based on Unity's built-in `Animator` component and finite state machine (FSM).
- All animation states inherit the `IState` interface, with each state machine restricted to refreshing one state at a time.
- Divided into `MovementStateMachine` and `ComboStateMachine`, each built on the `StateMachine` base class, which handles state transitions.
- Subclasses manage caching and initializing various states, handling input events registration/unregistration, animation playback, data initialization/updates, and exit/transition logic.

### ComboState (Combo System)
- Manages input events for attack keys, including pre-input animation events, required time-based animation events, combo interruptions, and transition animations.

### CharacterHealth
- Registers and handles damage events, including tracking the source of damage, playing hurt and block animations, and generating hit sound effects and visual effects.

### Third-Person and Skill Camera Control
- Uses Cinemachine’s State-Driven Camera, Dolly track camera, and Timeline to dynamically update Dolly data.
- Horizontal centering of movement states is achieved through Cinemachine calculations.

### ScriptableObject and ReusableData
- `ScriptableObject` stores all static data for characters.
- `ReusableData` stores mutable data with change events bound through BindableProperty.

### TimerManager (Timer Utility)
- Provides countdown delegate functions based on both `ScaleTime` and `UnScaleTime`.
- Includes an API to destroy currently active countdown delegates.

### GameEventManager (Event Utility)
- Offers APIs for event registration, triggering, and destruction, using `string` types to map to delegate events.

### SFX_PoolManager (Sound Effect Pool Manager)
- Provides an API to activate sound prefabs based on `SoundStyle` enum mappings.
- Uses object pool design to automate sound prefab generation and configuration.

### VFXManager (Visual Effect Manager)
- Controls the playback speed of all visual effects and offers an API for controlling scene-wide VFX playback speed.

### CameraHitFeel (Camera Feedback)
- Manages slow motion, freeze frames, and screen shake effects, providing APIs to adjust `ScaleTime`, animation playback speed, and VFX playback speed across the scene.

### SwitchCharacter (Character Switching)
- Implements character switching, switch animations, and changes camera target points.
- Provides APIs for character switching and combo actions.

### GameBlackboard (Character Data Sharing)
- Implements data sharing between characters and synchronization with enemies.
- Provides `GetGameData(string)` and `GetEnemies()` APIs for accessing character and enemy-related data.
