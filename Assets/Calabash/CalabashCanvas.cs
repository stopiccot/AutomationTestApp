using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using Calabash;

[Serializable]
public struct CalabashMatchedObject {
	public string label;
	public Calabash.Rect rect;
}

public class CalabashCanvas : MonoBehaviour {

	private Calabash.HttpServer httpServer = null;

	public static CalabashCanvas Instance { get; private set; }

	void Awake() {
		#if CALABASH_UNITY
		Instance = this;
		httpServer = new Calabash.HttpServer();
		#endif
	}

	void Start() {
		#if CALABASH_UNITY
		httpServer.Start();
		#endif
	}

	void Destroy() {
		#if CALABASH_UNITY
		httpServer.Destroy();
		httpServer = null;
		#endif
	}

	public Calabash.Rect GetUIRect(GameObject gameObject) {
		var rectTransform = gameObject.GetComponent<RectTransform>();
		Vector3[] v = new Vector3[4];
		rectTransform.GetWorldCorners(v);

		FileLog.Log(v[0].x + " " + v[0].y + " " + v[0].z);
		FileLog.Log(v[1].x + " " + v[1].y + " " + v[1].z);
		FileLog.Log(v[2].x + " " + v[2].y + " " + v[2].z);
		FileLog.Log(v[3].x + " " + v[3].y + " " + v[3].z);

		const float scale = 0.5f;

		return new Calabash.Rect { 
			x = scale * v[0].x,
			y = scale * v[0].y, 
			width = scale * (v[2].x - v[0].x),
			height = scale * (v[1].y - v[0].y),
			center_x = scale * 0.5f * (v[2].x + v[0].x),
			center_y = scale * 0.5f * (v[1].y + v[0].y),
		};
	}

	public List<CalabashMatchedObject> Query(string queryString) {
		List<CalabashMatchedObject> results = new List<CalabashMatchedObject>();

		Regex regex = new Regex("(.*?) (.*?) ?'(.*?)'");

		Match match = regex.Match(queryString);

		if (!match.Success) {
			throw new CalabashException("Failed to parse query \"" + queryString + "\"");
		}

		var viewType = match.Groups[1].Value;
		var queryVerb = match.Groups[2].Value;
		var queryParameter = match.Groups[3].Value;

		FileLog.Log("PARSED QUERY: \"" + queryString + "\"");
		FileLog.Log(viewType);
		FileLog.Log(queryVerb);
		FileLog.Log(queryParameter);

		if (viewType == "view") {
			FileLog.Log("CHECKING VIEWS");

			var views = GetComponentsInChildren<CanvasRenderer>();

			foreach (var view in views) {
				FileLog.Log("CHECKING VIEW \"" + view.gameObject.name + "\"");
				var textComponent = view.GetComponent<UnityEngine.UI.Text>();
				if (textComponent != null) {
					var viewText = textComponent.text;

					// Unity labels often have nasty newline in the end
					if (viewText.EndsWith("\n")) {
						viewText = viewText.Substring(0, viewText.Length - 1);
					}

					if (viewText == queryParameter) {
						FileLog.Log("ITS THE ONE");
						results.Add(new CalabashMatchedObject {
							label = viewText,
							rect = GetUIRect(view.gameObject)
						});
					}
				}
			}
		} else if (viewType == "button") {
			var buttons = GetComponentsInChildren<UnityEngine.UI.Button>();

			foreach (var button in buttons) {
				FileLog.Log("CHECKING BUTTON");
				var buttonText = button.GetComponentInChildren<UnityEngine.UI.Text>().text;

				// Unity labels often have nasty newline in the end
				if (buttonText.EndsWith("\n")) {
					buttonText = buttonText.Substring(0, buttonText.Length - 1);
				}

				FileLog.Log("\"" + buttonText + "\" - " + buttonText.Length.ToString());
				FileLog.Log("\"" + queryParameter + "\" - " + queryParameter.Length.ToString());

				if (buttonText == queryParameter) {
					FileLog.Log("ITS THE ONE");
					results.Add(new CalabashMatchedObject {
						label = buttonText,
						rect = GetUIRect(button.gameObject)
					});
				}
			}
		} else {
			FileLog.Log("UNKNOWN VIEW TYPE \"" + viewType + "\"");
			return null;
		}

		return results;
	}
}
