using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteraction : MonoBehaviour {

	public UnityEvent OnTrigger;
	public UnityEvent OnExit;
	public UnityEvent OnUse;

	private bool playerIsTrigger;
	private bool used;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (used)
		{
			return;
		}

		if (Input.GetButtonDown("Interact") && playerIsTrigger)
		{
			used = true;
			OnUse.Invoke();
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (used)
			return;

		if (other.CompareTag("Player"))
		{
			playerIsTrigger = true;
			OnTrigger.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerIsTrigger = false;
			OnExit.Invoke();
		}
	}
}
