using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using Luminosity.IO;
using UnityEngine.Events;

public class LockPicking : MonoBehaviour {

	public Transform player;

	public Transform playerPosition;

	public float speed = 5;	

	public UnityEvent OnUnlock;

	private float[] chosenValues;

	private float timer;
	private int index;

	private bool canUnlock = false;

	private void Start()
	{

		chosenValues = new float[3];

		for (int i = 0; i < chosenValues.Length; i++)
		{
			chosenValues[i] = Random.Range(10, 350f);
		}
	}

	private void Update()
	{


		if (!canUnlock)
			return;

		float h = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");

		if((h < 0.004 && h > 0) && (y < 0.004 && y > 0))
		{
			LockPickingManager.instance.cursor.SetActive(false);
			timer = 0;
		}
		else
		{
			LockPickingManager.instance.cursor.SetActive(true);
		}

		float total = Mathf.Atan2(h, y) + 3;

		total = Mathf.Clamp(total, 0, 6);

		LockPickingManager.instance.UpdateCursorRotation(total);

		if((total * 60 > (chosenValues[index] - 10)) && (total * 60 < (chosenValues[index] + 10)))
		{
			timer += Time.deltaTime;
			LockPickingManager.instance.lockAnimator.SetBool("Unlocking", true);
			GamePad.SetVibration(PlayerIndex.One, 1, 1);
			if (timer > 2)
			{
				LockPickingManager.instance.lockAnimator.SetTrigger("Unlocked");			
				timer = 0;
				index++;
				canUnlock = false;
				Invoke("SetCanUnlock", 1f);
				
				
			}
		}
		else
		{
			LockPickingManager.instance.lockAnimator.SetBool("Unlocking", false);
			timer = 0;
			GamePad.SetVibration(PlayerIndex.One, 0, 0);
		}


	}

	void SetCanUnlock()
	{

		LockPickingManager.instance.Unlock(index - 1);
		GamePad.SetVibration(PlayerIndex.One, 0, 0);
		if (index < 3)
		{
			canUnlock = true;
		}
		else
		{
			LockPickingManager.instance.lockAnimator.SetBool("Unlocking", false);
			Invoke("DisablePanel", 1f);

		}
	}

	void DisablePanel()
	{
		LockPickingManager.instance.ResetUnlocks();
		OnUnlock.Invoke();
	}

	public void StartMinigame()
	{
		canUnlock = true;
		LockPickingManager.instance.cursor.SetActive(false);
		SetPlayerPositionAndRotation();
		LockPickingManager.instance.lockPickPanel.SetActive(true);
	}

	void SetPlayerPositionAndRotation()
	{
		StartCoroutine(PositioningPlayer());
	}

	IEnumerator PositioningPlayer()
	{
		float t = 0;

		while (t < 1)
		{
			player.position = Vector3.Lerp(player.position, playerPosition.position, Time.deltaTime * speed);
			player.rotation = Quaternion.Lerp(player.rotation, playerPosition.rotation, Time.deltaTime * speed);
			t += Time.deltaTime;
			yield return null;
		}
	}
}
