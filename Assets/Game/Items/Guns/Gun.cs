using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Items.Guns
{
    [Serializable]
    public class Gun : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Transform _firePoint;

        public float FiringRate;
        public AudioSource FiringSound;
        public Sprite Sprite;
        public GameObject FiringAnimation;

        void OnEnable()
        {
            var _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if(_spriteRenderer != null)
                _spriteRenderer.sprite = Sprite;
        }

        public virtual void SetFirePoint(Transform firePoint)
        {
            _firePoint = firePoint;
        }

        public virtual void Fire()
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation;
                bullet.SetActive(true);
            }
            if (FiringSound != null) FiringSound.Play();
        }
    }
}
