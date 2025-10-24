using Coherence;
using Coherence.Toolkit;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [Sync]
    public string characterName = "Character";
    [Sync] 
    public bool IsDead = false;
    [Sync]
    public string PlayerName = "Player";
    [Sync]
    public int CharIndex = 0;
    [Sync]
    public int Hp = 100;

    [Sync]
    public int Atk = 100;
    [Sync]
    public Mesh mesh;
    [SerializeField] public CharacterData characterData;

    [Header("References")]
    [SerializeField] private CharacterCamera characterCamera;
    [SerializeField] private Animator characterAnimator;

    [SerializeField] public Transform characterModel;
    [SerializeField] private SkinnedMeshRenderer characterMesh;

    [SerializeField] private TMP_Text playerNameText;

    public CharacterData CharacterData => characterData;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private float targetSpeed;
    private string currentAnim = "Idle"; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("[AnimationController] Rigidbody component not found!");
        }

        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();

        if (characterCamera != null)
            characterCamera.SetTrackingTarget(transform);
    }

    private void Start()
    {

        //PlayerName = CoherenceBridgePlayerAccount.PlayerName;
        characterName = GameManager.Instance.GetSelectedCharacterData().GetCharacterName();
        playerNameText.color = Color.green;
        PlayerName = GameManager.Instance.PlayerName;
        CharIndex = GameManager.Instance.SelectedCharacterIndex;
        characterData = GameManager.Instance.GetCharacterData(CharIndex);
        if (characterData != null)
        {
            characterData = GameManager.Instance.GetSelectedCharacterData();
            if (characterData != null)
            {
                Hp = characterData.Health;
                Atk = characterData.AttackPower;
            }
        }
    }

    

    void Update()
    {
        if (CharacterCamera.Instance != null)
        {
            CharacterCamera.Instance.SetTrackingTarget(this.transform);
        }
        HandleInput();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(horizontal, 0f, vertical).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        targetSpeed = (isRunning ? runSpeed : moveSpeed) * movementInput.magnitude;

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetAnimation("Wave");
            Debug.Log("[AnimationController] Wave animation triggered.");
        }
    }

    private void HandleMovement()
    {
        if (rb == null) return;

        Vector3 velocity = new Vector3(movementInput.x * targetSpeed, rb.linearVelocity.y, movementInput.z * targetSpeed);
        rb.linearVelocity = velocity;

        if (movementInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            characterModel.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(characterModel.GetComponent<Rigidbody>().rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void HandleAnimation()
    {
        if (characterAnimator == null) return;

        string newAnim;
        Debug.Log("[AnimationController] Movement Input Magnitude: " + movementInput.magnitude + ", Target Speed: " + targetSpeed);
        if (movementInput.magnitude == 0)
            if(currentAnim == "Wave")
                return;
            else
                newAnim = "Idle";
        else if (targetSpeed > runSpeed * 0.6f)
            newAnim = "Run";
        else
            newAnim = "Walk";

        if (newAnim != currentAnim)
        {
            SetAnimation(newAnim);
        }
    }

    private void SetAnimation(string anim)
    {
        characterAnimator.SetBool("IsIdle", false);
        characterAnimator.SetBool("IsWalk", false);
        characterAnimator.SetBool("IsRun", false);
        characterAnimator.SetBool("IsWave", false);

        switch (anim)
        {
            case "Idle":
                characterAnimator.SetBool("IsIdle", true);
                break;
            case "Walk":
                characterAnimator.SetBool("IsWalk", true);
                break;
            case "Run":
                characterAnimator.SetBool("IsRun", true);
                break;
            case "Wave":
                characterAnimator.SetBool("IsWave", true);
                break;
        }

        currentAnim = anim;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            IsDead = true;
        }
    }



    [Command]
    public void SetHP(int hp)
    {
        Hp = hp;

    }

}
