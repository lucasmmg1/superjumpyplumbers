using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
        private Rigidbody2D rb;
    
        [SerializeField]
        private float movementForce = 5.0f;

        private int direction;
        int initialDirection;

        int[] directions = {-1, 1};

        public int pointValue;

        [SerializeField] private Game GeneralGameSettings;
    #endregion

    #region Unity Methods
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            initialDirection = directions[Random.Range(0, 2)];

            GeneralGameSettings = FindObjectOfType<Game>();
        }

        private void Start()
        {
            direction = initialDirection;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                Flip();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Die();
            }
        }
    #endregion

    #region Personalized Methods
        void Move()
        {
            rb.velocity = new Vector2(direction * movementForce * Time.deltaTime, rb.velocity.y);
        }
    
        void Flip()
        {
            direction *= -1;
        }
    
        void Die()
        {
            Destroy(this.gameObject);
        }
    
    #endregion
}
