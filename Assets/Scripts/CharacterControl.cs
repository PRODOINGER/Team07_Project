﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supercyan.FreeSample
{
    public class SimpleSampleCharacterControl : MonoBehaviour
    {
        [SerializeField] private float blinkDuration = 1f; // 깜빡임 효과의 총 지속 시간
        [SerializeField] private float blinkInterval = 0.1f; // 깜빡이는 간격
        [SerializeField] private string exceptionTag = "NoBlink"; // 깜빡임 예외 처리를 위한 태그

        private Renderer[] renderers; // 캐릭터와 자식 오브젝트의 모든 Renderer 컴포넌트들
        private bool isBlinking = false; // 현재 깜빡이는 중인지 확인

        [SerializeField] private float m_moveSpeed = 2; // 캐릭터 이동 속도
        [SerializeField] private float m_jumpForce = 4; // 캐릭터 점프 힘
        [SerializeField] private float m_sideJumpForce = 5; // 좌우 이동 시 점프 힘

        [SerializeField] private Animator m_animator = null; // 애니메이터 컴포넌트 참조
        [SerializeField] private Rigidbody m_rigidBody = null; // Rigidbody 컴포넌트 참조

        private float m_jumpTimeStamp = 0; // 점프 간격을 체크하는 시간 스탬프
        private float m_minJumpInterval = 0.25f; // 최소 점프 간격
        private bool m_jumpInput = false; // 점프 입력을 받았는지 확인
        private bool m_isGrounded; // 캐릭터가 바닥에 닿아 있는지 여부 확인

        private bool isScaledDown = false; //현재 크기가 줄어든 상태인지 확인

        private List<Collider> m_collisions = new List<Collider>(); // 충돌 중인 콜라이더를 추적하는 리스트

        private int currentLane = 1; // 현재 트랙 위치 (0: 왼쪽, 1: 중앙, 2: 오른쪽)
        private float laneDistance = 5f; // 트랙 간의 거리

        private void Awake()
        {
            // 필요한 컴포넌트를 초기화하고 확인
            if (!m_animator) { m_animator = gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { m_rigidBody = gameObject.GetComponent<Rigidbody>(); }
            renderers = GetComponentsInChildren<Renderer>(); // 자식 포함 모든 Renderer 컴포넌트 가져오기

        }

        private void Start()
        {
            // 게임 시작 시 캐릭터를 이동 상태로 설정
            if (m_rigidBody != null)
            {
                m_rigidBody.velocity = new Vector3(0, 0, m_moveSpeed);
            }
        }

        private void Update()
        {
            // Shift 버튼을 눌렀을 때 크기를 절반으로 줄이는 코루틴 실행
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isScaledDown)
            {
                StartCoroutine(ScaleDownCoroutine());
            }

            if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
            {
                m_jumpInput = true;
            }

            // 점프 입력이 발생하면 m_jumpInput을 true로 설정
            if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
            {
                m_jumpInput = true;
            }

            // 좌우 이동 입력 처리
            if (m_isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
                {
                    currentLane--;
                    MoveToLane(-1); // 왼쪽으로 이동
                }
                else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
                {
                    currentLane++;
                    MoveToLane(1); // 오른쪽으로 이동
                }
            }
        }

        private void MoveToLane(int direction)
        {
            // 방향에 따라 점프력을 주어 캐릭터를 이동
            m_rigidBody.AddForce(new Vector3(direction * m_sideJumpForce, m_jumpForce, 0), ForceMode.Impulse);

            // 목표 위치 설정 후 부드럽게 이동 시작
            Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMoveToLane(targetPosition));
        }

        private IEnumerator SmoothMoveToLane(Vector3 targetPosition)
        {
            // 목표 위치에 부드럽게 도달할 때까지 이동
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, m_moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPosition; // 정확한 목표 위치로 고정
        }

        private void FixedUpdate()
        {
            // 애니메이터에 바닥에 닿아 있음을 설정
            m_animator.SetBool("Grounded", m_isGrounded);

            // Z 방향으로 일정한 속도를 유지하여 이동
            Vector3 forwardVelocity = new Vector3(0, m_rigidBody.velocity.y, m_moveSpeed);
            m_rigidBody.velocity = forwardVelocity;

            if (m_animator)
            {
                m_animator.SetFloat("MoveSpeed", 1);
            }

            // 점프 및 착지 상태 처리
            JumpingAndLanding();
        }

        private void JumpingAndLanding()
        {
            // 최소 점프 간격을 확인하여 점프 실행
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                m_isGrounded = false;
                m_jumpInput = false;
            }

            if (m_animator)
            {
                m_animator.SetBool("Grounded", m_isGrounded);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 충돌한 표면이 바닥인지 확인하여 착지 상태 업데이트
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }

                // 충돌한 오브젝트가 예외 태그를 가졌다면 깜빡임 생략
                if (collision.gameObject.CompareTag(exceptionTag))
                {
                    return;
                }

                // 예외 태그가 아닐 경우 깜빡임 시작
                if (!isBlinking)
                {
                    StartCoroutine(BlinkEffect());
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            // 바닥과 계속 충돌 중인 상태를 확인하여 착지 상태 유지
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            // 충돌이 끝난 콜라이더를 리스트에서 제거하고 착지 상태 갱신
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }

        private IEnumerator BlinkEffect()
        {
            isBlinking = true; // 깜빡임 시작
            float endTime = Time.time + blinkDuration;

            while (Time.time < endTime)
            {
                // 모든 Renderer를 반복적으로 껐다 켜서 깜빡임 효과
                foreach (Renderer rend in renderers)
                {
                    rend.enabled = !rend.enabled;
                }
                yield return new WaitForSeconds(blinkInterval);
            }

            // 깜빡임이 끝나면 모든 Renderer를 켜진 상태로 복원
            foreach (Renderer rend in renderers)
            {
                rend.enabled = true;
            }
            isBlinking = false; // 깜빡임 종료
        }

        private IEnumerator ScaleDownCoroutine()
        {
            isScaledDown = true;

            // 원래 크기를 저장하고 절반 크기로 변경
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 0.5f;

            yield return new WaitForSeconds(1f); // 1초 대기

            // 크기를 원래대로 복구
            transform.localScale = originalScale;
            isScaledDown = false;
        }

    }
}
