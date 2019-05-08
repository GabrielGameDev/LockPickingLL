using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPickingManager : MonoBehaviour {

	public static LockPickingManager instance;

	public GameObject lockPickPanel;
	public Transform cursorJoint;
	public GameObject cursor;
	public Animator lockAnimator;

	public Image[] unlocks;

	private Color unlockDefaultColor;

	private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

		unlockDefaultColor = unlocks[0].color;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateCursorRotation(float total)
	{
		cursorJoint.rotation = Quaternion.Euler(new Vector3(0, 0, total * 60));
	}

	public void Unlock(int index)
	{
		unlocks[index].color = Color.white;
		Vector2 newPos = unlocks[index].transform.localPosition;
		newPos.y += 5;
		unlocks[index].transform.localPosition = newPos;
	}

	public void ResetUnlocks()
	{
		for (int i = 0; i < unlocks.Length; i++)
		{
			unlocks[i].color = unlockDefaultColor;
			Vector2 newPos = unlocks[i].transform.localPosition;
			newPos.y -= 5;
			unlocks[i].transform.localPosition = newPos;
		}
	}
}
