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

	void OnGUI()
	{
		GUILayout.BeginArea( new Rect( 0, 0, Screen.width, Screen.height ) );
		GUILayout.BeginHorizontal();
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		occlusion.enabled = GUILayout.Toggle( occlusion.enabled, " Amplify Occlusion Enabled" );
		GUILayout.Space( 5 );
		occlusion.ApplyMethod = GUILayout.Toggle( ( occlusion.ApplyMethod == POST ), " " + "Standard Post-effect" ) ? POST : occlusion.ApplyMethod;
		occlusion.ApplyMethod = GUILayout.Toggle( ( occlusion.ApplyMethod == DEFERRED ), " " + "Deferred Injection" ) ? DEFERRED : occlusion.ApplyMethod;
		occlusion.ApplyMethod = GUILayout.Toggle( ( occlusion.ApplyMethod == DEBUG ), " " + "Debug Mode" ) ? DEBUG : occlusion.ApplyMethod;
		GUILayout.EndVertical();

		GUILayout.FlexibleSpace();

		GUILayout.BeginVertical();
		GUILayout.Space( 5 );

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "Intensity     " );
		GUILayout.EndVertical();
		occlusion.Intensity = GUILayout.HorizontalSlider( occlusion.Intensity, 0.0f, 1.0f, GUILayout.Width( 100 ) );
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.Intensity.ToString( "0.00" ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "Power Exp. " );
		GUILayout.EndVertical();
		occlusion.PowerExponent = GUILayout.HorizontalSlider( occlusion.PowerExponent, 0.0001f, 6.0f, GUILayout.Width( 100 ) );
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.PowerExponent.ToString( "0.00" ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "Radius        " );
		GUILayout.EndVertical();
		occlusion.Radius = GUILayout.HorizontalSlider( occlusion.Radius, 0.1f, 10.0f, GUILayout.Width( 100 ) );
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( " " + occlusion.Radius.ToString( "0.00" ) );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "Quality        " );
		GUILayout.EndVertical();
		occlusion.SampleCount = ( SampleCountLevel ) ( ( int ) GUILayout.HorizontalSlider( ( float ) occlusion.SampleCount, 0.0f, 3.0f, GUILayout.Width( 100 ) ) );
		GUILayout.Space( 5 );
		GUILayout.BeginVertical();
		GUILayout.Space( -3 );
		GUILayout.Label( "        " );
		GUILayout.EndVertical();
		GUILayout.Space( 5 );
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
