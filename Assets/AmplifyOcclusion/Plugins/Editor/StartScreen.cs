// Amplify Occlusion 2 - Robust Ambient Occlusion for Unity
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Networking;
using System.Collections;

namespace AmplifyOcclusion
{
	public class Preferences
	{
		public static readonly string PrefStartUp = "AOLastSession" + Application.productName;
		public static readonly string PrefForceUpdate = "AOForceUpdate" + Application.productName;
	}

	public class StartScreen : EditorWindow
	{
		[MenuItem( "Window/Amplify Occlusion/Start Screen", false, 19 )]
		public static void Init()
		{
			StartScreen window = (StartScreen)GetWindow( typeof( StartScreen ), true, "Amplify Occlusion Start Screen" );
			window.minSize = new Vector2( 650, 500 );
			window.maxSize = new Vector2( 650, 500 );
			window.Show();
		}

		private static readonly string RefID = "Ref_Occlusion";

		private static readonly string IconGUID = "13ffadb74e0cf6248ac0430224ad2090";
		private static readonly string BannerGUID = "a4949af6361c47942902cebcc0089340";

		public static readonly string BannerInfoURL = "http://amplify.pt/Banner/AOInfo.json";
		public static readonly string PackageRefURL = "http://amplify.pt/Banner/PackageRef.json";

		private static readonly string WikiURL = "http://wiki.amplify.pt/index.php?title=Unity_Products:Amplify_Occlusion";

		private static readonly string DiscordURL = "https://discordapp.com/invite/EdrVAP5";
		private static readonly string ForumURL = "https://forum.unity.com/threads/free-introducing-amplify-occlusion-2-ground-truth-ambient-occlusion-up-to-2x-faster.399022/";

		private static readonly string SiteURL = "http://amplify.pt/download/";
		private static readonly string StoreURL = "https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-occlusion-56739?aid=1011lPwI&pubref=" + RefID;

		private static readonly GUIContent ResourcesTitle = new GUIContent( "Learning Resources" );
		private static readonly GUIContent CommunityTitle = new GUIContent( "Community", "Need help? Reach us through our discord server or the offitial support Unity forum" );
		private static readonly GUIContent UpdateTitle = new GUIContent( "Latest News" );
		private static readonly GUIContent TitleSTR = new GUIContent( "Amplify Occlusion" );

		Vector2 m_scrollPosition = Vector2.zero;
		bool m_startup = false;

		[NonSerialized]
		Texture textIcon = null;
		[NonSerialized]
		Texture webIcon = null;

		GUIContent WikiButton = null;
		GUIContent DiscordButton = null;
		GUIContent ForumButton = null;

		GUIContent Icon = null;
		RenderTexture rt;

		[NonSerialized]
		GUIStyle m_buttonStyle = null;
		[NonSerialized]
		GUIStyle m_labelStyle = null;
		[NonSerialized]
		GUIStyle m_linkStyle = null;

		Texture2D m_newsImage = null;
		private BannerInfo m_bannerInfo;
		private PackageRef m_packageRef;
		private bool m_infoDownloaded = false;
		private string m_newVersion = string.Empty;

		private void OnEnable()
		{
			rt = new RenderTexture( 16, 16, 0 );
			rt.Create();

			m_startup = EditorPrefs.GetBool( Preferences.PrefStartUp, true );

			if( m_newsImage == null )
				m_newsImage = AssetDatabase.LoadAssetAtPath<Texture2D>( AssetDatabase.GUIDToAssetPath( BannerGUID ) );

			if( textIcon == null )
			{
				Texture icon = EditorGUIUtility.IconContent( "TextAsset Icon" ).image;
				var cache = RenderTexture.active;
				RenderTexture.active = rt;
				Graphics.Blit( icon, rt );
				RenderTexture.active = cache;
				textIcon = rt;

				WikiButton = new GUIContent( " Official Wiki", textIcon );
			}

			if( webIcon == null )
			{
				webIcon = EditorGUIUtility.IconContent( "BuildSettings.Web.Small" ).image;
				DiscordButton = new GUIContent( " Discord", webIcon );
				ForumButton = new GUIContent( " Unity Forum", webIcon );
			}

			if( m_bannerInfo == null )
			{
				m_bannerInfo = new BannerInfo( VersionInfo.FullNumber, "05/05/2020 12:00:00", "Thank you for trying our products!\n\nWe invite you to learn more about our range of Unity solutions and our many learning resources available via our official Wiki. Be sure to join our growing Discord community, it's a great place to ask quick questions and interact with Amplify users and developers.", "", "https://assetstore.unity.com/publishers/707?aid=1011lPwI&pubref=" + RefID );
			}

			if( m_packageRef == null )
			{
				string[] titles = {
					"Amplify Shader Editor",
					"Amplify Impostors",
					"Amplify Occlusion",
					"Amplify Color",
					"Amplify LUT Pack",
					"Fake Interiors FREE",
					"FXAA",
				};
				string[] urls = {
					"https://assetstore.unity.com/packages/tools/visual-scripting/amplify-shader-editor-68570?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/tools/utilities/amplify-impostors-119877?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-occlusion-56739?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-color-1894?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-lut-pack-50070?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/vfx/shaders/fake-interiors-free-104029?aid=1011lPwI&pubref=" + RefID,
					"https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/fxaa-fast-approximate-anti-aliasing-3590?aid=1011lPwI&pubref=" + RefID,
				};
				m_packageRef = new PackageRef( titles, urls );
			}

			if( Icon == null )
			{
				Icon = new GUIContent( AssetDatabase.LoadAssetAtPath<Texture2D>( AssetDatabase.GUIDToAssetPath( IconGUID ) ) );
			}
		}

		private void OnDisable()
		{
			if( rt != null )
			{
				rt.Release();
				DestroyImmediate( rt );
			}
		}

		public void OnGUI()
		{
			if( !m_infoDownloaded )
			{
				m_infoDownloaded = true;

				// get affiliate links
				StartBackgroundTask( StartRequest( PackageRefURL, ( www ) =>
				{
					var pack = PackageRef.CreateFromJSON( www.downloadHandler.text );
					if( pack != null )
					{
						m_packageRef = pack;
						Repaint();
					}
				} ) );

				// get banner information and texture
				StartBackgroundTask( StartRequest( BannerInfoURL, ( www ) =>
				{
					BannerInfo info = BannerInfo.CreateFromJSON( www.downloadHandler.text );
					if( info != null && !string.IsNullOrEmpty( info.ImageUrl ) )
					{
						StartBackgroundTask( StartTextureRequest( info.ImageUrl, ( www2 ) =>
						{
							Texture2D texture = DownloadHandlerTexture.GetContent( www2 );
							if( texture != null )
								m_newsImage = texture;
						} ) );
					}

					if( info != null && info.Version >= m_bannerInfo.Version )
					{
						m_bannerInfo = info;
					}

					// improve this later
					int major = m_bannerInfo.Version / 100;
					int minor = ( m_bannerInfo.Version / 10 ) - major * 10;
					int release = m_bannerInfo.Version - major * 100 - minor * 10;
					m_newVersion = major + "." + minor + "." + release;
					Repaint();
				} ) );
			}

			if( m_buttonStyle == null )
			{
				m_buttonStyle = new GUIStyle( GUI.skin.button );
				m_buttonStyle.alignment = TextAnchor.MiddleLeft;
			}

			if( m_labelStyle == null )
			{
				m_labelStyle = new GUIStyle( "BoldLabel" );
				m_labelStyle.margin = new RectOffset( 4, 4, 4, 4 );
				m_labelStyle.padding = new RectOffset( 2, 2, 2, 2 );
				m_labelStyle.fontSize = 13;
			}

			if( m_linkStyle == null )
			{
				var inv = AssetDatabase.LoadAssetAtPath<Texture2D>( AssetDatabase.GUIDToAssetPath( "1004d06b4b28f5943abdf2313a22790a" ) ); // find a better solution for transparent buttons
				m_linkStyle = new GUIStyle();
				m_linkStyle.normal.textColor = new Color( 0.2980392f, 0.4901961f, 1f );
				m_linkStyle.hover.textColor = Color.white;
				m_linkStyle.active.textColor = Color.grey;
				m_linkStyle.margin.top = 3;
				m_linkStyle.margin.bottom = 2;
				m_linkStyle.hover.background = inv;
				m_linkStyle.active.background = inv;
			}

			EditorGUILayout.BeginHorizontal( GUIStyle.none, GUILayout.ExpandWidth( true ) );
			{
				// left column
				EditorGUILayout.BeginVertical( GUILayout.Width( 175 ) );
				{
					GUILayout.Label( ResourcesTitle, m_labelStyle );
					if( GUILayout.Button( WikiButton, m_buttonStyle ) )
						Application.OpenURL( WikiURL );

					GUILayout.Space( 10 );

					GUILayout.Label( "Amplify Products", m_labelStyle );

					if( m_packageRef.Links != null )
					{
						var webIcon = EditorGUIUtility.IconContent( "BuildSettings.Web.Small" ).image;
						for( int i = 0; i < m_packageRef.Links.Length; i++ )
						{
							var gc = new GUIContent( " " + m_packageRef.Links[ i ].Title, webIcon );
							if( GUILayout.Button( gc, m_buttonStyle ) )
								Application.OpenURL( m_packageRef.Links[ i ].Url + RefID );
						}
					}

					GUILayout.Label( "* Affiliate Links", "minilabel" );
				}
				EditorGUILayout.EndVertical();

				// right column
				EditorGUILayout.BeginVertical( GUILayout.Width( 650 - 175 - 9 ), GUILayout.ExpandHeight( true ) );
				{
					GUILayout.Label( CommunityTitle, m_labelStyle );
					EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
					{
						if( GUILayout.Button( DiscordButton, GUILayout.ExpandWidth( true ) ) )
						{
							Application.OpenURL( DiscordURL );
						}
						if( GUILayout.Button( ForumButton, GUILayout.ExpandWidth( true ) ) )
						{
							Application.OpenURL( ForumURL );
						}
					}
					EditorGUILayout.EndHorizontal();
					GUILayout.Label( UpdateTitle, m_labelStyle );

					if( m_newsImage != null )
					{
						var gc = new GUIContent( m_newsImage );
						int width = 650 - 175 - 9 - 8;
						width = Mathf.Min( m_newsImage.width, width );
						int height = m_newsImage.height;
						height = (int)( ( width + 8 ) * ( (float)m_newsImage.height / (float)m_newsImage.width ) );


						Rect buttonRect = EditorGUILayout.GetControlRect( false, height );
						EditorGUIUtility.AddCursorRect( buttonRect, MouseCursor.Link );
						if( GUI.Button( buttonRect, gc, m_linkStyle ) )
						{
							Application.OpenURL( m_bannerInfo.LinkUrl );
						}
					}

					m_scrollPosition = GUILayout.BeginScrollView( m_scrollPosition, GUILayout.ExpandHeight( true ), GUILayout.ExpandWidth( true ) );
					GUILayout.Label( m_bannerInfo.NewsText, "WordWrappedMiniLabel", GUILayout.ExpandHeight( true ) );
					GUILayout.EndScrollView();

					EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
					{
						EditorGUILayout.BeginVertical();
						GUILayout.Label( TitleSTR, m_labelStyle );

						GUILayout.Label( "Installed Version: " + VersionInfo.StaticToString() );

						if( m_bannerInfo.Version > VersionInfo.FullNumber )
						{
							var cache = GUI.color;
							GUI.color = Color.red;
							GUILayout.Label( "New version available: " + m_newVersion, "BoldLabel" );
							GUI.color = cache;
						}
						else
						{
							var cache = GUI.color;
							GUI.color = Color.green;
							GUILayout.Label( "You are using the latest version", "BoldLabel" );
							GUI.color = cache;
						}

						EditorGUILayout.BeginHorizontal();
						GUILayout.Label( "Download links:" );
						if( GUILayout.Button( "Amplify", m_linkStyle ) )
							Application.OpenURL( SiteURL );
						GUILayout.Label( "-" );
						if( GUILayout.Button( "Asset Store", m_linkStyle ) )
							Application.OpenURL( StoreURL );
						EditorGUILayout.EndHorizontal();
						GUILayout.Space( 7 );
						EditorGUILayout.EndVertical();

						GUILayout.FlexibleSpace();
						EditorGUILayout.BeginVertical();
						GUILayout.Space( 7 );
						GUILayout.Label( Icon );
						EditorGUILayout.EndVertical();
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal( "ProjectBrowserBottomBarBg", GUILayout.ExpandWidth( true ), GUILayout.Height( 22 ) );
			{
				GUILayout.FlexibleSpace();
				EditorGUI.BeginChangeCheck();
				var cache = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 100;
				m_startup = EditorGUILayout.ToggleLeft( "Show At Startup", m_startup, GUILayout.Width( 120 ) );
				EditorGUIUtility.labelWidth = cache;
				if( EditorGUI.EndChangeCheck() )
				{
					EditorPrefs.SetBool( Preferences.PrefStartUp, m_startup );
				}
			}
			EditorGUILayout.EndHorizontal();

			// Find a better way to update link buttons without repainting the window
			Repaint();
		}

		public delegate void SuccessCall( UnityWebRequest www );

		public static IEnumerator StartRequest( string url, SuccessCall success = null )
		{
			using( var www = UnityWebRequest.Get( url ) )
			{
#if UNITY_2017_2_OR_NEWER
				yield return www.SendWebRequest();
#else
				yield return www.Send();
#endif

				while( www.isDone == false )
					yield return null;

				if( success != null )
					success( www );
			}
		}

		public static IEnumerator StartTextureRequest( string url, SuccessCall success = null )
		{
			using( UnityWebRequest www = UnityWebRequestTexture.GetTexture( url ) )
			{
#if UNITY_2017_2_OR_NEWER
				yield return www.SendWebRequest();
#else
				yield return www.Send();
#endif

				while( www.isDone == false )
					yield return null;

				if( success != null )
					success( www );
			}
		}

		public static void StartBackgroundTask( IEnumerator update, Action end = null )
		{
			EditorApplication.CallbackFunction closureCallback = null;

			closureCallback = () =>
			{
				try
				{
					if( update.MoveNext() == false )
					{
						if( end != null )
							end();
						EditorApplication.update -= closureCallback;
					}
				}
				catch( Exception ex )
				{
					if( end != null )
						end();
					Debug.LogException( ex );
					EditorApplication.update -= closureCallback;
				}
			};

			EditorApplication.update += closureCallback;
		}
	}

	[Serializable]
	internal class BannerInfo
	{
		public int Version;
		public string ShowBefore;
		public string NewsText;
		public string ImageUrl;
		public string LinkUrl;

		public static BannerInfo CreateFromJSON( string jsonString )
		{
			return JsonUtility.FromJson<BannerInfo>( jsonString );
		}

		public BannerInfo( int version, string showBefore, string newsText, string imageUrl, string linkUrl )
		{
			Version = version;
			ShowBefore = showBefore;
			NewsText = newsText;
			ImageUrl = imageUrl;
			LinkUrl = linkUrl;
		}
	}

	[Serializable]
	internal class PackageRef
	{
		[Serializable]
		internal class LinkRef
		{
			public string Title;
			public string Url;

			public LinkRef( string title, string url )
			{
				Title = title;
				Url = url;
			}
		}

		public LinkRef[] Links;

		public static PackageRef CreateFromJSON( string jsonString )
		{
			return JsonUtility.FromJson<PackageRef>( jsonString );
		}

		public PackageRef( string[] titles, string[] urls )
		{
			Links = new LinkRef[ titles.Length ];
			for( int i = 0; i < titles.Length; i++ )
			{
				Links[ i ] = new LinkRef( titles[ i ], urls[ i ] );
			}
		}
	}

	[InitializeOnLoad]
	public class StartScreenLoader
	{
		static StartScreenLoader()
		{
			EditorApplication.update += Update;
		}

		static void Update()
		{
			EditorApplication.update -= Update;

			if( !EditorApplication.isPlayingOrWillChangePlaymode )
			{
				bool show = false;
				if( !EditorPrefs.HasKey( Preferences.PrefStartUp ) )
				{
					show = true;
					EditorPrefs.SetBool( Preferences.PrefStartUp, true );
				}
				else
				{
					if( Time.realtimeSinceStartup < 10 )
					{
						show = EditorPrefs.GetBool( Preferences.PrefStartUp, true );

						if( !show )
						{
							StartScreen.StartBackgroundTask( StartScreen.StartRequest( StartScreen.BannerInfoURL, ( www ) =>
							{
								BannerInfo info = BannerInfo.CreateFromJSON( www.downloadHandler.text );
								if( info != null )
								{
									if( DateTime.Now < DateTime.Parse( info.ShowBefore ) && !EditorPrefs.GetBool( Preferences.PrefForceUpdate, false ) )
									{
										EditorPrefs.SetBool( Preferences.PrefForceUpdate, true );
										EditorPrefs.SetBool( Preferences.PrefStartUp, true );
										StartScreen.Init();
									}
									else if( DateTime.Now > DateTime.Parse( info.ShowBefore ) )
									{
										EditorPrefs.SetBool( Preferences.PrefForceUpdate, false );
									}
								}
							} ) );
						}
					}
				}

				if( show )
					StartScreen.Init();
			}
		}
	}
}
