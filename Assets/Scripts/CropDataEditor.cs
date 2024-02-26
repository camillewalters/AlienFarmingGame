using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CropData))]
public class CropDataEditor : Editor
{
	private static HashSet<string> uniqueNames = new HashSet<string>();
	//CropData cropData;
	public override void OnInspectorGUI()
	{
		CropData cropData = (CropData)target;

		// Check for unique name
		if (!IsNameUnique(cropData.cropName))
		{
			EditorGUILayout.HelpBox("Crop name is not unique. Please choose a unique name.", MessageType.Error);
		}

		// Draw the default inspector GUI
		DrawDefaultInspector();
	}

	private bool IsNameUnique(string name)
    {
		return uniqueNames.Add(name);
	}
}