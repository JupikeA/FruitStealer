using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {

	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			animator.SetBool("jump", true);
			Invoke("stopjump", 3.5f);
		}
	}
	
	void stopjump()
	{
	animator.SetBool("jump", false);
	}
}
