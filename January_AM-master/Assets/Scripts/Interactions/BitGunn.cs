using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitGunn : MonoBehaviour
{
    Transform m_bulletSpawnPoint;

    [SerializeField]
    GameObject m_prefabBullet;

    [SerializeField]
    float m_shootForce;

    int m_ammo = 9;
    bool m_reloadStarted;

    private void Awake()
    {
        m_bulletSpawnPoint = transform.GetChild(0);
    }

    void Interact()
    {
        if(m_ammo > 0)
        {
            m_reloadStarted = true;
            GameObject tempBullet = Instantiate(m_prefabBullet, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
            tempBullet.GetComponent<Rigidbody>().AddForce(m_bulletSpawnPoint.forward * m_shootForce);
            Destroy(tempBullet, 5f);

            m_ammo--;
        }
    }

    void AltInteract()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        if(m_reloadStarted == false)
        {
            m_reloadStarted = true;
            yield return new WaitForSeconds(1);
            m_ammo = 9;
        }
        yield return new WaitForSeconds(0);
    }
}
