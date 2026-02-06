using UnityEngine;
using System.Collections;
using AmplifyOcclusion;

[ExecuteInEditMode]
public class Controls : MonoBehaviour
{
	public AmplifyOcclusionEffect occlusion;

	const AmplifyOcclusionEffect.ApplicationMethod POST = AmplifyOcclusionEffect.ApplicationMethod.PostEffect;
	const AmplifyOcclusionEffect.ApplicationMethod DEFERRED = AmplifyOcclusionEffect.ApplicationMethod.Deferred;
	const AmplifyOcclusionEffect.ApplicationMethod DEBUG = AmplifyOcclusionEffect.ApplicationMethod.Debug;

	// Shadow settings
	Color shadowColor = new Color( 0, 0, 0, 0.7f );
	Vector2 shadowOffset = new Vector2( 1.5f, 1.5f );

	void ShadowLabel( string text, params GUILayoutOption[] options )
	{
		var style = GUI.skin.label;

		// Layout
		GUILayout.Label( text, style, options );

		// Draw shadow + main text
		Rect r = GUILayoutUtility.GetLastRect();

		Color oldColor = GUI.color;

		GUI.color = shadowColor;
		Rect rs = r;
		rs.x += shadowOffset.x;
		rs.y += shadowOffset.y;
		GUI.Label( rs, text, style );

		GUI.color = Color.white;
		GUI.Label( r, text, style );

		GUI.color = oldColor;
	}

	bool ShadowToggle( bool value, string text, params GUILayoutOption[] options )
	{
		var style = GUI.skin.toggle;

		// Layout
		GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, 0 );
		bool newValue = GUILayout.Toggle( value, text, style, options );
		GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, 1 );

		Rect r = GUILayoutUtility.GetLastRect();

		float checkSize = style.CalcSize( new GUIContent( " " ) ).y; // decent approximation

		Rect textRect = r;
		textRect.xMin += checkSize + 6f;
		textRect.width += 3f;
		textRect.height += 3f;

		Color oldColor = GUI.color;

		GUI.Toggle( r, value, GUIContent.none, style );

		// Shadow
		GUI.color = shadowColor;
		Rect rs = textRect;
		rs.x += shadowOffset.x;
		rs.y += shadowOffset.y;
		GUI.Label( rs, text, GUI.skin.label );

		// Main
		GUI.color = Color.white;
		GUI.Label( textRect, text, GUI.skin.label );

		GUI.color = oldColor;

		return newValue;
	}

	void OnGUI()
	{
		float scale = 2.0f;
		var oldMatrix = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * scale);
		GUILayout.BeginArea(new Rect(0, 0, Screen.width / scale, Screen.height / scale));

		GUILayout.BeginHorizontal();
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		occlusion.enabled = ShadowToggle( occlusion.enabled, "AO Enabled" );
		GUILayout.Space( 10 );
		occlusion.ApplyMethod = ShadowToggle( ( occlusion.ApplyMethod == POST ), "Post Effect" ) ? POST : occlusion.ApplyMethod;
		occlusion.ApplyMethod = ShadowToggle( ( occlusion.ApplyMethod == DEFERRED ), "Deferred Injection" ) ? DEFERRED : occlusion.ApplyMethod;
		occlusion.ApplyMethod = ShadowToggle( ( occlusion.ApplyMethod == DEBUG ), "Debug" ) ? DEBUG : occlusion.ApplyMethod;
		GUILayout.EndVertical();

		GUILayout.FlexibleSpace();

		GUILayout.BeginVertical();
		GUILayout.Space( 5 );

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		ShadowLabel( "Intensity", GUILayout.Width( 55 ) );
		GUILayout.EndVertical();
		occlusion.Intensity = GUILayout.HorizontalSlider( occlusion.Intensity, 0.0f, 1.0f, GUILayout.Width( 60 ) );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.Intensity.ToString( "0.00" ), GUILayout.Width( 30 ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		ShadowLabel( "Power", GUILayout.Width( 55 ) );
		GUILayout.EndVertical();
		occlusion.PowerExponent = GUILayout.HorizontalSlider( occlusion.PowerExponent, 0.0001f, 6.0f, GUILayout.Width( 60 ) );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.PowerExponent.ToString( "0.00" ), GUILayout.Width( 30 ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		ShadowLabel( "Radius", GUILayout.Width( 55 ) );
		GUILayout.EndVertical();
		occlusion.Radius = GUILayout.HorizontalSlider( occlusion.Radius, 0.1f, 10.0f, GUILayout.Width( 60 ) );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.Radius.ToString( "0.00" ), GUILayout.Width( 30 ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		ShadowLabel( "Quality", GUILayout.Width( 55 ) );
		GUILayout.EndVertical();
		occlusion.SampleCount = ( SampleCountLevel ) ( ( int ) GUILayout.HorizontalSlider( ( float ) occlusion.SampleCount, 0.0f, 3.0f, GUILayout.Width( 60 ) ) );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "   " + ( ( int )occlusion.SampleCount + 1 ), GUILayout.Width( 30 ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		GUILayout.EndArea();
		GUI.matrix = oldMatrix;
	}
}
