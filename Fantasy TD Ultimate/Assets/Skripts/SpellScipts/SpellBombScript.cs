using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBombScript : MonoBehaviour {

    public GameObject[] Effects;
    public int damage = 100;

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("EnemyHit");
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Destroy(gameObject);
            Effects[0].SetActive(true);
            Effects[1].SetActive(true);
            Effects[2].SetActive(true);
            Effects[3].SetActive(true);
            Effects[4].SetActive(true);
            Effects[5].SetActive(true);
            //EffectInstance.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            //StartCoroutine(DeleteEffect(EffectInstance));

        }
        if (collider.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GroundHit");
            Effects[0].SetActive(true);
            Effects[1].SetActive(true);
            Effects[2].SetActive(true);
            Effects[3].SetActive(true);
            Effects[4].SetActive(true);
            Effects[5].SetActive(true);
 
            StartCoroutine(DeleteEffect());
        }
    }

    public IEnumerator DeleteEffect()
    {

        yield return new WaitForSeconds(2f);
        // Destroy(Effect);
    }
}
