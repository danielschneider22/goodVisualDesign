using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDownToRhythm : MonoBehaviour
{
	public float heightTarget = 100;
	public enum Direction { up, down };
	public float duration = 1.0f;
	public Direction direction;
	private Vector2 topPos;
	private Vector2 bottomPos;
	private bool shouldMove;
	private float elapsedTime = 0f;

	private void Awake()
	{
		float heightTargetCoor = heightTarget / 100f;
		topPos = new Vector2(transform.position.x, transform.position.y - heightTargetCoor);
		bottomPos = new Vector2(transform.position.x, transform.position.y + heightTargetCoor);		
	}

	void toggleDirection()
	{
		direction = direction == Direction.up ? Direction.down : Direction.up;
	}

	void FixedUpdate()
	{
		if (shouldMove)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= duration)
			{
				toggleDirection();
				shouldMove = false;
			} else
            {
				float t = Mathf.Min(elapsedTime / duration, 1);
				transform.position = direction == Direction.up ?
					new Vector2(transform.position.x, Mathf.SmoothStep(topPos.y, bottomPos.y, t)) :
					new Vector2(transform.position.x, Mathf.SmoothStep(bottomPos.y, topPos.y, t));
			}
			
		}


	}

	public void DoMoveUpOrDown()
	{
		shouldMove = true;
		elapsedTime = 0f;
	}
}
