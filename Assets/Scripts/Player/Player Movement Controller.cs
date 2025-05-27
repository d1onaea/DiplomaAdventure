using UMA;
using UMA.CharacterSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementContoller : MonoBehaviour, IDataPersistence
{
    [Header("Configuration")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 4f;

    private CharacterController controller;
    private Transform cameraMainTransform;
    private Animator animator;

    private bool movementDisabled = false;
    private Vector2 currentMoveInput = Vector2.zero;

    [SerializeField] private DynamicCharacterAvatar avatar;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onMovePressed += OnMovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onMovePressed -= OnMovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;

        if (avatar != null)
        {
            avatar.CharacterCreated.RemoveListener(OnCharacterCreated);
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;

        if (avatar != null)
        {
            avatar.CharacterCreated.AddListener(OnCharacterCreated);
        }
        else
        {
            Debug.LogError("UMA Avatar не назначен!");
        }
    }

    private void OnCharacterCreated(UMAData data)
    {
        animator = avatar.GetComponent<Animator>();
    }

    private void OnMovePressed(Vector2 moveDir)
    {
        currentMoveInput = moveDir;
    }

    private void DisablePlayerMovement()
    {
        movementDisabled = true;
        animator.SetBool("walking", false);
    }

    private void EnablePlayerMovement()
    {
        movementDisabled = false;
    }

    private void Update()
    {
        if (movementDisabled || animator == null)
        {
            return;
        }

        Vector3 move = new(currentMoveInput.x, 0, currentMoveInput.y);
        move = (cameraMainTransform.forward * move.z) + (cameraMainTransform.right * move.x);
        move.y = 0f;

        _ = controller.Move(move * Time.deltaTime * playerSpeed);

        UpdateAnimation(currentMoveInput);
        UpdateRotation(currentMoveInput);
        ClampToGround();
    }
    private void ClampToGround()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
    private void UpdateAnimation(Vector3 currentMoveInput)
    {
        bool walking = currentMoveInput.magnitude > 0.01f;
        animator.SetBool("walking", walking);
    }

    private void UpdateRotation(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            float targetAngle = (Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg) + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
        transform.rotation = data.playerRotation;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
        data.playerRotation = transform.rotation;
    }
}



