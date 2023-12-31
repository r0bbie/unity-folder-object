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

		private bool lockFolder;
		private bool hideChildrenInHierarchy;

		Tool lastTool = Tool.None;

		
		void OnEnable()
		{

		}
		

		/// <summary>
		/// Unity OnDisable function - called when associated object is deselected in the hierarchy
		/// </summary>
		void OnDisable()
		{
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

			lockFolder = false;
			hideChildrenInHierarchy = false;

			if (folderObject.lockFolder != lockFolder)
			{
				folderObject.gameObject.hideFlags &= ~HideFlags.NotEditable;

				foreach (Component component in folderObject.GetComponents(typeof(Component)))
					component.hideFlags &= ~HideFlags.NotEditable;

				Tools.current = Tool.Move;

				EditorUtility.SetDirty(folderObject);

				folderObject.lockFolder = lockFolder;
			}

			if (folderObject.hideChildrenInHierarchy != hideChildrenInHierarchy)
			{
				foreach (Transform child in folderObject.transform)
					child.hideFlags &= ~HideFlags.HideInHierarchy;

				EditorApplication.RepaintHierarchyWindow();

				folderObject.hideChildrenInHierarchy = hideChildrenInHierarchy;
			}

		}


		/// <summary>
		/// Called while object is selected in the inspector view
		/// </summary>
		public override void OnInspectorGUI()
		{
			if (folderObject == null)
				folderObject = target as FolderObject;

			if (folderObject.gameObject.GetComponent<Transform>().hideFlags != HideFlags.HideInInspector)
				folderObject.gameObject.GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;

			lockFolder = folderObject.lockFolder;
			hideChildrenInHierarchy = folderObject.hideChildrenInHierarchy;

			GUILayout.Space(15);

			EditorGUILayout.HelpBox("Folder object activated. Transform forced to defaults.\nRemove FolderObject component to bring back Transform window.", MessageType.Info);

			GUILayout.Space(20);

			if (GUI.changed)
			{
				if (folderObject.lockFolder != lockFolder)
				{
					if (lockFolder)
					{
						folderObject.gameObject.hideFlags |= HideFlags.NotEditable;

						foreach (Component component in folderObject.GetComponents(typeof(Component)))
						{
							if (component == folderObject)
							{
								component.hideFlags &= ~HideFlags.NotEditable;
							}
							else
							{
								component.hideFlags |= HideFlags.NotEditable;
							}
						}

						EditorUtility.SetDirty(folderObject);
					}
					else
					{
						folderObject.gameObject.hideFlags &= ~HideFlags.NotEditable;

						foreach (Component component in folderObject.GetComponents(typeof(Component)))
						{
							component.hideFlags &= ~HideFlags.NotEditable;
						}

						Tools.current = Tool.Move;

						EditorUtility.SetDirty(folderObject);
					}

					folderObject.lockFolder = lockFolder;
				}

				if (folderObject.hideChildrenInHierarchy != hideChildrenInHierarchy)
				{
					if (hideChildrenInHierarchy)
					{
						foreach (Transform child in folderObject.transform)
							child.hideFlags |= HideFlags.HideInHierarchy;
					}
					else
					{
						foreach (Transform child in folderObject.transform)
							child.hideFlags &= ~HideFlags.HideInHierarchy;
					}

					EditorApplication.RepaintHierarchyWindow();
					folderObject.hideChildrenInHierarchy = hideChildrenInHierarchy;
				}

				EditorUtility.SetDirty(target);
			}
		}
		
		
	}
}

#endif
