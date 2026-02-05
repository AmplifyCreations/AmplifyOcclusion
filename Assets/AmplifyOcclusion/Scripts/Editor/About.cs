// Amplify Occlusion 2 - Robust Ambient Occlusion for Unity
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using UnityEngine;
using UnityEditor;


namespace AmplifyOcclusion
{
public class About : EditorWindow
{
	private const string AboutImageGUID = "01d9f923d6f8ee6499b20e69fe1c53be";

	private Vector2 m_scrollPosition = Vector2.zero;

	private Texture2D m_aboutImage;

	[MenuItem( "Window/Amplify Occlusion/About...", false, 20 )]
	static void Init()
	{
		About window = (About)GetWindow( typeof( About ), true, "About Amplify Occlusion" );

		window.minSize = new Vector2( 502, 290 );
		window.maxSize = new Vector2( 502, 290 );

		window.Show();
	}


	public void OnFocus()
	{
		string path = AssetDatabase.GUIDToAssetPath( AboutImageGUID );
		if ( !string.IsNullOrEmpty( path ) )
		{
			TextureImporter importer = AssetImporter.GetAtPath( path ) as TextureImporter;
			if ( importer != null )
			{
				if( importer.textureType != TextureImporterType.GUI )
				{
					importer.textureType = TextureImporterType.GUI;
					AssetDatabase.ImportAsset( path );
				}
				m_aboutImage = AssetDatabase.LoadAssetAtPath( path, typeof( Texture2D ) ) as Texture2D;
			}
		}
	}


	public void OnGUI()
	{
		m_scrollPosition = GUILayout.BeginScrollView( m_scrollPosition );

		GUILayout.BeginVertical();

		GUILayout.Space( 10 );

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Box( m_aboutImage, GUIStyle.none );

		if( Event.current.type == EventType.MouseUp &&
				GUILayoutUtility.GetLastRect().Contains( Event.current.mousePosition ) )
		{
			Application.OpenURL( "http://www.amplify.pt" );
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUIStyle labelStyle = new GUIStyle( EditorStyles.label );

		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.wordWrap = true;

		GUILayout.Label( "\nAmplify Occlusion " + VersionInfo.StaticToString(), labelStyle, GUILayout.ExpandWidth( true ) );

		GUILayout.Label( "\nCopyright (c) Amplify Creations, Lda. All rights reserved.\n", labelStyle, GUILayout.ExpandWidth( true ) );

		GUILayout.EndVertical();

		GUILayout.EndScrollView();
	}
}
}
