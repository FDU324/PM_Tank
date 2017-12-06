using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon;
public class FlyAndBoom : PunBehaviour
{
	public GameObject boomEffectPrefab;
	public AudioClip boomAudio;
	public float damageRadius;
	public int damage;
	public float range;
    public float v0;
	private float distance = 0.0f;
	// Use this for initialization
	void Start ()
	{
        damageRadius = 50;
        damage = 20;
        range = 500;
        v0 = 200;
    }
	
	// Update is called once per frame
	void Update ()
	{
		distance += v0 * Time.deltaTime;
        //Debug.Log(gameObject.transform.position);
		gameObject.transform.Translate (transform.forward * v0 * Time.deltaTime, Space.World);
		if (distance >= range) {
			boom ();
			Destroy (gameObject);
			Debug.Log ("Loss");
			return;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
        Debug.Log(collision.collider.gameObject.name);
		boom ();
		Destroy (gameObject);
	}

	void boom ()
	{
		boomEffect ();
		Collider[] others = Physics.OverlapSphere (gameObject.transform.position, damageRadius);
        List<string> damagedTanks = new List<string>();
		if (others.Length == 0)
			return;
		for (int i = 0; i < others.Length; i++) {
            //Debug.Log (others[i].gameObject.name);
            TankModel tankModel = others[i].gameObject.GetComponentInParent<TankModel>();

            if (others [i].gameObject.tag == "TankPart" && !damagedTanks.Contains(tankModel.gameObject.name)) {
                damagedTanks.Add(tankModel.gameObject.name);
                tankModel.TakeDamage (damage);
			}
		}
		
	}

	void boomEffect ()
	{
		GameObject effect = Instantiate(boomEffectPrefab) as GameObject;
		effect.transform.position = gameObject.transform.position;
		AudioSource.PlayClipAtPoint (boomAudio, gameObject.transform.position);
		Destroy (effect, 2);
	}
		
}
