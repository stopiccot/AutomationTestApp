using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class DeviceDisplay : MonoBehaviour {

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern float _scaleFactor();

	[DllImport ("__Internal")]
	private static extern int _statusBarHeightInPixels();
#endif

	/// <summary>
	/// Sets the status bar height in pixels.
	/// </summary>
	/// <value>The status bar height in pixels.</value>
	public static int statusBarHeightInPixels {
		get {
			var result = 0;

			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				result = _statusBarHeightInPixels();
			}
			#endif

			return result;
		}
	}

	/// <summary>
	/// Gets the scale factor.
	/// </summary>
	/// <value>The scale factor.</value>
	public static float scaleFactor {
		get {
			float result = 1.0f;

			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				result = _scaleFactor();
			}
			#endif

			#if UNITY_ANDROID
			result = Screen.dpi / 160.0f;
			#endif

			return result;
		}
	}
}
