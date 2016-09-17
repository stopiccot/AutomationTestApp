using System;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Calabash
{
	public class CalabashRequestBody {
		public string query;
	}

	public class MapRequestResponse {
		public string outcome;
		public string reason;
		public CalabashMatchedObject[] results;
	}

	public class MapRoute : Route {
		public override string HandleRequest(System.Net.HttpListenerRequest request) {
	      /*QUERY: --->
	
	      {
				:method=>:post, 
				:path=>"map", 
				:uri=>#<URI::HTTP http://127.0.0.1:37265/map>, 
				:body=>"{\"query\":\"view text:'My media'\",\"operation\":{\"method_name\":\"query\",\"arguments\":[]}}"
		    }
	
			{
				"status_bar_orientation": "down",
				"results": [{
					"id": null,
					"visible": 1,
					"label": "My media",
					"frame": {
						"y": 0,
						"width": 79.5,
						"x": 0,
						"height": 21
					},
					"enabled": true,
					"description": "<UILabel: 0x7f83b820; frame = (0 0; 79.5 21); text = 'My media'; userInteractionEnabled = NO; layer = <_UILabelLayer: 0x7f83b930>>",
					"text": "My media",
					"alpha": 1,
					"value": "My media",
					"rect": {
						"y": 339,
						"width": 79.5,
						"center_x": 65.75,
						"x": 26,
						"center_y": 349.5,
						"height": 21
					},
					"accessibilityElement": true,
					"class": "UILabel"
				}],
				"outcome": "SUCCESS"
			}*/

			string text;
			using (var reader = new StreamReader(request.InputStream, request.ContentEncoding)) {
				text = reader.ReadToEnd();
			}

			FileLog.Log("~~~~~~~~~~~~~~");
			FileLog.Log(text);
			FileLog.Log("~~~~~~~~~~~~~~");

			string queryString = null;

			if (!String.IsNullOrEmpty(text)) {
				var v = JsonUtility.FromJson<CalabashRequestBody>(text);

				FileLog.Log("~~~~~~~~~~~~~~");
				FileLog.Log(v.query);
				FileLog.Log("~~~~~~~~~~~~~~");

				queryString = v.query;
			}

//			queryString = "button marked: 'OK'";

			FileLog.Log("11111111111");

			string failureReason = null;

			var task = Task.Factory.StartNew<List<CalabashMatchedObject>>(() => {
				try {
					return CalabashCanvas.Instance.Query(queryString);
				} catch (Exception e) {
					FileLog.Log("EXCEPTION: " + e.ToString());
					failureReason = "Horrible exception during CalabashCanvas.Instance.Query";
					return null;
				}
			}, CancellationToken.None, TaskCreationOptions.None, UnityScheduler.UpdateScheduler);

			FileLog.Log("22222222222");

			task.Wait();

			FileLog.Log("33333333333");

			var matchedObjects = task.Result;

			var responseObject = new MapRequestResponse {
				outcome = "SUCCESS",
				results = new CalabashMatchedObject[] {}
			};

			if (matchedObjects != null) {
				responseObject.results = matchedObjects.ToArray();
			} else {
				responseObject.outcome = "FAILURE";
				responseObject.reason = failureReason != null ? failureReason : "NO FUCKING REASON";
			}

			return JsonUtility.ToJson(responseObject);
		}
	}
}

