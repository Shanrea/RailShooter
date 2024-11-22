using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunProjectile m_projectilePrefab;
    [SerializeField] private Transform m_shootingSpot;

    //[Header("Values")]

    private GunProjectile m_currentProjectile;
    private bool m_isCharging = false;  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }
        if(m_isCharging && m_currentProjectile)
        {
            float newChargeTime = m_currentProjectile.CurrentChargeTime + Time.deltaTime;
            m_currentProjectile.SetCharge(newChargeTime);
        }
    }

    private void OnMouseButtonDown()
    {
        m_isCharging=true;
        m_currentProjectile = InstantiateProjectile();
    }

    private void OnMouseButtonUp()
    {
        m_isCharging = false;
        ShootProjectile(m_currentProjectile, m_shootingSpot.forward);
        m_currentProjectile = null;


    }

    private GunProjectile InstantiateProjectile()
    {
        GunProjectile projectile = Instantiate(m_projectilePrefab, m_shootingSpot);
        projectile.transform.localPosition = Vector3.zero;
        return projectile;
    }

    private void ShootProjectile(GunProjectile projectile, Vector3 dir)
    {
        projectile.Shoot(dir);
    }
}
