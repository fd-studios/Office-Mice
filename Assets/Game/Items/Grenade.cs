using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Items
{
    public class Grenade : Projectile
    {
        Rigidbody2D _rigidbody;

        public float Speed = 20f;
        public int Damage = 100;
        public int Value = 5;

        public override int Cost { get { return Value; } }

        void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.right * Speed;
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude < 1f)
            {
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Mouse":
                    var mouse = collision.GetComponent<Mouse>();
                    if (mouse != null)
                    {
                        mouse.TakeDamage(Damage);
                    }
                    break;
                case "Wall":
                case "Decor":
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
