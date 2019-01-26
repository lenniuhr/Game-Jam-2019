﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBombScript : MonoBehaviour {

    public GameObject[] Effects;
    public int damage = 100;
    private bool explode = false;
    private Vector3 Pos;

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Attacker"))
        {
            Debug.Log("EnemyHit");
            collider.gameObject.GetComponent<Attacker>().TakeDamage(damage);
            Destroy(gameObject);
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

        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (explode)
        {
            this.GetComponent<Transform>().position = Pos;
        }
    }
}
