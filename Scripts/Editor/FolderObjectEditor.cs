using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace R0bbie.FolderObject.Editor
{
	[CustomEditor(typeof(FolderObject))]
	[CanEditMultipleObjects]
	public class FolderObjectEditor : UnityEditor.Editor
	{
		private FolderObject folderObject;

		Tool lastTool = Tool.None;

		
		/// <summary>
		/// On object select
		/// </summary>
		/// <returns></returns>
		void OnEnable()
		{
			if (folderObject == null)
				folderObject = target as FolderObject;
			
			// Zero out transform
			folderObject.ResetTransform();
			
			// No tool selected on the folder object
			lastTool = Tools.current;
			Tools.current = Tool.None;
			
			// TODO: Remove any child objects before resetting transform, so that they remain consistent?
			
			// Make sure the folder object component is the first component on the object
			if (folderObject.GetComponents<MonoBehaviour>()[0] != folderObject)
				UnityEditorInternal.ComponentUtility.MoveComponentUp(folderObject);
		}
		

		/// <summary>
		/// Unity OnDisable function - called when associated object is deselected in the hierarchy
		/// </summary>
		void OnDisable()
		{
			// On unselect, return to last editor tool
			Tools.current = lastTool;
		}


		/// <summary>
		/// When FolderObject component is removed, disable its functionality, and ensure the transform component is shown again
		/// </summary>
		public void FolderComponentRemoved()
		{
			if (folderObject == null)
				folderObject = target as FolderObject;

			folderObject.gameObject.GetComponent<Transform>().hideFlags = HideFlags.None;
		}


		/// <summary>
		/// Called while object is selected in the inspector view to render custom inspector
		/// </summary>
		public override void OnInspectorGUI()
		{
			if (folderObject == null)
				folderObject = target as FolderObject;
			
			// Ensure move/rotation tools etc remain disabled on this object
			Tools.current = Tool.None;

			if (folderObject.gameObject.GetComponent<Transform>().hideFlags != HideFlags.HideInInspector)
				folderObject.gameObject.GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Folder object activated. Transform forced to defaults.\nRemove FolderObject component to bring back Transform window.", MessageType.Info);

			GUILayout.Space(20);
		}
		
		
	}
}

#endif
