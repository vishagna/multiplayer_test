using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DavidJalbert.LowPolyPeople
{
    [RequireComponent(typeof(Rigidbody))]
    public class AnimationController : MonoBehaviour
    {
        [Header("Character Settings")]
        [Tooltip("Danh sách các Animator của nhân vật.")]
        public Animator[] characters;

        [Tooltip("Text hiển thị trạng thái animation (nếu có).")]
        public Text label;

        [Tooltip("Các bảng màu có thể thay đổi.")]
        public Material[] palettes;

        [Header("Camera Settings")]
        public Camera[] cameras;
        private int currentCamera = 0;

        [Header("Movement Settings")]
        [Tooltip("Tốc độ di chuyển cơ bản của nhân vật.")]
        public float moveSpeed = 3f;

        [Tooltip("Ngưỡng tốc độ để chuyển sang animation chạy.")]
        public float runThreshold = 4f;

        private Rigidbody rb;
        private float currentSpeed;
        private Vector3 movementInput;

        private string currentAnim = "idle"; // Lưu animation hiện tại để tránh gọi trùng Trigger

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("[AnimationController] Rigidbody component not found!");
            }
        }

        void Update()
        {
            HandleInput();
        }

        // Xử lý Input
        private void HandleInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            movementInput = new Vector3(horizontal, 0f, vertical).normalized;
            currentSpeed = movementInput.magnitude * moveSpeed;

            HandleMovement();
            HandleAnimation();

            if (Input.GetKeyDown(KeyCode.R)) RandomizePalette();
            if (Input.GetKeyDown(KeyCode.Alpha4)) SetAnimation("wave");
        }

        // Di chuyển nhân vật
        private void HandleMovement()
        {
            if (rb == null) return;

            // Giữ nguyên trục Y
            Vector3 velocity = new Vector3(movementInput.x * moveSpeed, rb.linearVelocity.y, movementInput.z * moveSpeed);
            rb.linearVelocity = velocity;

            // Xoay nhân vật theo hướng di chuyển
            if (movementInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        // Xử lý animation dựa trên tốc độ
        private void HandleAnimation()
        {
            string newAnim;

            if (movementInput == Vector3.zero)
            {
                newAnim = "idle";
            }
            else if (currentSpeed > runThreshold)
            {
                newAnim = "run";
            }
            else
            {
                newAnim = "walk";
            }

            if (newAnim != currentAnim)
            {
                SetAnimation(newAnim);
                currentAnim = newAnim;
            }
        }

        // Kích hoạt animation trigger cho tất cả nhân vật
        public void SetAnimation(string tag)
        {
            if (characters == null || characters.Length == 0) return;

            //label?.SetText($"Animation: {tag}");

            foreach (Animator animator in characters)
            {
                if (animator == null) continue;

                animator.ResetTrigger("idle");
                animator.ResetTrigger("walk");
                animator.ResetTrigger("run");
                animator.ResetTrigger("wave");
                animator.SetTrigger(tag);
            }
        }

        // Random bảng màu nhân vật
        public void RandomizePalette()
        {
            if (palettes == null || palettes.Length == 0) return;

            Material randomMat = palettes[Random.Range(0, palettes.Length)];
            foreach (Animator animator in characters)
            {
                if (animator == null) continue;

                Renderer renderer = animator.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    renderer.material = randomMat;
                }
            }
        }

        // Đổi camera (nếu có nhiều camera)
        public void ChangeCamera()
        {
            if (cameras == null || cameras.Length == 0) return;

            cameras[currentCamera].enabled = false;
            currentCamera = (currentCamera + 1) % cameras.Length;
            cameras[currentCamera].enabled = true;
        }
    }
}
