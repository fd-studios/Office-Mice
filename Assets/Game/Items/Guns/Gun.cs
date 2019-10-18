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
        Projectile _projectile;
        AudioSource _firingSound;

        public float FiringRate;
        public string AudioTag;
        public Sprite Sprite;
        public GameObject FiringAnimation;
        public GameObject ProjectileType;

        public string ToastTitle;
        public float ToastPrice;
        public Sprite ToastImage;

        public Projectile Projectile
        {
            get
            {
                if (_projectile == null)
                    _projectile = ProjectileType.GetComponent<Projectile>();
                return _projectile;
            }
        }

        void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (_spriteRenderer != null)
                _spriteRenderer.sprite = Sprite;
        }

        public virtual void SetFirePoint(Transform firePoint)
        {
            _firePoint = firePoint;
        }

        public virtual void Fire()
        {
            GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(ProjectileType?.tag);
            if (projectile != null)
            {
                projectile.transform.position = _firePoint.position;
                projectile.transform.rotation = _firePoint.rotation;
                projectile.SetActive(true);
            }
            if (!string.IsNullOrEmpty(AudioTag))
            {
                _firingSound = GameObject.FindGameObjectWithTag(AudioTag).GetComponent<AudioSource>();
            }
            if (_firingSound != null)
            {
                _firingSound.Play();
            }
        }
    }
}
