using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBombScript : MonoBehaviour {

    public GameObject[] Effects;

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("EnemyHit");
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Destroy(gameObject);
            EffectInstance.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            //StartCoroutine(DeleteEffect(EffectInstance));

        }
        if (collider.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GroundHit");
            GameObject EffectInstance = Instantiate(Effekt[0]);
            EffectInstance.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            StartCoroutine(DeleteEffect(EffectInstance));
        }
    }

    public IEnumerator DeleteEffect(GameObject Effect)
    {
        Debug.Log("Specal Effect");
        yield return new WaitForSeconds(2f);
        // Destroy(Effect);
    }
}
