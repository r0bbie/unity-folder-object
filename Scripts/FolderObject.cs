using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace R0bbie.FolderObject
{
	/// <summary>
	/// Attach this component to a GameObject to hide the transform component and force values to defaults, essentially have the object behave as a "folder" for organisation, without any relative impact on the transforms of child objects 
	/// </summary>
	[ExecuteInEditMode]
	[AddComponentMenu("Miscellaneous/Folder Object")]
	public class FolderObject : MonoBehaviour
	{
		
		/// <summary>
		/// On Start, ensure transform hasn't been accidentally manipulated somehow
		/// </summary>
		void Start()
		{
			ResetTransform();
		}


		/// <summary>
		/// Forces transform reset
		/// </summary>
		/// <returns></returns>
		public void ResetTransform()
		{
			gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}

		
		/// <summary>
		/// Ensure FolderObject behaviour is removed when component itself is removed
		/// </summary>
		void OnDestroy()
		{
#if UNITY_EDITOR
			gameObject.GetComponent<Transform>().hideFlags = HideFlags.None;
#endif
		}
		

	}
}