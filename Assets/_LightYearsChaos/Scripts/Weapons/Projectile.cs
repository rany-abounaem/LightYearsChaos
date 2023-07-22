using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class Projectile : MonoBehaviour
    {
        private float damage;
        private Unit target;

        public void SendProjectile(Unit target, float damage, float speed)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            var dir = (target.transform.position - transform.position).normalized;
            rigidbody.velocity = dir * speed;
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Unit unit) && unit == target)
            {
                target.Health.RemoveHealth(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
