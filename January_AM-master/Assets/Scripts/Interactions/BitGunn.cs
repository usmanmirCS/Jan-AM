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

    private void Awake()
    {
        m_bulletSpawnPoint = transform.GetChild(0);
    }

    void Interact()
    {
        GameObject tempBullet = Instantiate(m_prefabBullet, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
        tempBullet.GetComponent<Rigidbody>().AddForce(m_bulletSpawnPoint.forward * m_shootForce);
        Destroy(tempBullet, 5f);
    }
}
