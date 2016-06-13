using System;
using UnityEngine;

namespace LineFollower
{
	public abstract class InputFactory
	{
		public abstract Vector3 GetInput ();
		public abstract bool IsInput ();

		private static InputFactory instance;
		
		private InputFactory ()
		{
		}

		public static void init()
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer) 
			{
				instance = new MouseInput ();
			} else if (Application.platform == RuntimePlatform.Android) 
			{
				instance = new AndroidTouchInput();
			} else if (Application.platform == RuntimePlatform.WindowsEditor) 
			{
				instance = new MouseInput();
			}
		}

		public static InputFactory GetInputInstance()
		{
			return instance;
		}

		class MouseInput : InputFactory
		{
			override
			public Vector3 GetInput()
			{
				return Input.mousePosition;
			}

			override
			public bool IsInput()
			{
				return Input.GetMouseButton(0);
			}
		}

		class AndroidTouchInput : InputFactory
		{
			override
			public Vector3 GetInput()
			{
				return new Vector3 (Input.touches [0].position.x, Input.touches [0].position.y, 0);
			}

			override
			public bool IsInput()
			{
				return Input.touches.Length > 0;
			}
		}
	}


}

