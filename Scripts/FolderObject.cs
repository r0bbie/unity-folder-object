using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace R0bbie.FolderObject
{

	[ExecuteInEditMode]
	[AddComponentMenu("Miscellaneous/Folder Object")]
	public class FolderObject : MonoBehaviour
	{

		public bool lockFolder;
		public bool hideChildrenInHierarchy;

		
		/// <summary>
		/// On Start, ensure transform hasn't been accidentally manipulated somehow
		/// </summary>
		void Start()
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

			if (lockFolder)
			{
				lockFolder = false;

				gameObject.hideFlags &= ~HideFlags.NotEditable;

				foreach (Component component in GetComponents(typeof(Component)))
					component.hideFlags &= ~HideFlags.NotEditable;

				Tools.current = Tool.Move;

				EditorUtility.SetDirty(this);
			}

			if (hideChildrenInHierarchy)
			{
				hideChildrenInHierarchy = false;

				foreach (Transform child in transform)
					child.hideFlags &= ~HideFlags.HideInHierarchy;

				EditorApplication.RepaintHierarchyWindow();
			}
#endif
		}
		

	}
}