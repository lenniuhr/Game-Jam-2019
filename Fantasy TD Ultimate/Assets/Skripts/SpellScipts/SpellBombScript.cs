using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBombScript : MonoBehaviour {

    public GameObject[] Effects;
    private float radius = 16f;
    public int damage = 100;
    private bool explode = false;
    private Vector3 Pos;
    public AudioSource Audio;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("EnemyHit");
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Effects[0].SetActive(true);
            explode = true;
            Pos = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y +1, this.GetComponent<Transform>().position.z);
            //EffectInstance.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            //StartCoroutine(DeleteEffect(EffectInstance));
        }
        if (collider.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GroundHit");
            Effects[0].SetActive(true);
            explode = true;
            Pos = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y + 1, this.GetComponent<Transform>().position.z);
            StartCoroutine(DeleteEffect());
        }
        
    }




    public IEnumerator DeleteEffect()
    {
        Audio.Play();
        yield return new WaitForSeconds(1.5f);
        Collider[] hitColliders = Physics.OverlapSphere(Pos, radius);
        int i = 0;
        Debug.Log(hitColliders);
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i]);
            if (hitColliders[i].gameObject.CompareTag("Attacker"))
            {
                hitColliders[i].gameObject.GetComponent<Attacker>().TakeDamage(damage);
            }
            i++;
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (explode)
        {
            this.GetComponent<Transform>().position = Pos;
        }
    }
}
