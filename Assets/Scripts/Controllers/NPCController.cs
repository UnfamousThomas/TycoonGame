using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

// Source: https://discussions.unity.com/t/making-npcs-wander-in-2d/697084/2

public class NPCController : MonoBehaviour
{
    internal Transform thisTransform;
    private Animator _animator;
    private SpriteRenderer _renderer;

    // The movement speed of the object
    public float moveSpeed = 0.2f;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left,Vector3.zero, Vector3.zero };
    internal int currentMoveDirection;

    // Use this for initialization
    void Start()
    {
        // Cache the transform for quicker access
        thisTransform = this.transform;
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }
  
    // Update is called once per frame
    void Update()
    {
        if (moveDirections[currentMoveDirection] == Vector3.left)
        {
            _renderer.flipX = true;
            _animator.Play("Hedgehog" + "Move");
        }
        else if (moveDirections[currentMoveDirection] == Vector3.zero)
        {
            _animator.Play("Hedgehog" + "Idle");
        }
        else
        {
            _renderer.flipX = false;
            _animator.Play("Hedgehog" + "Move");
        }
        // Move the object in the chosen direction at the set speed
        thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
    }
}

