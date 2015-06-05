using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
    public Texture backgroundTexture;
    
    public void Start() {
        Branch.initSession(delegate(System.Collections.Generic.Dictionary<string, object> parameters, string error) {
            if (error != null) {
                Debug.Log("An error occurred: " + error);
            }
            else {
                Debug.Log("InitSession succeeded with params: " + MiniJSON.Json.Serialize(parameters));
            }
        });

        _labelStyle = new GUIStyle();
        _labelStyle.normal.textColor = Color.black;
        _labelStyle.fontSize = 36;
    }

    public void OnGUI() {
        GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

        _UpdateUrlLabelIfPresent();
        _AddRefreshShortUrlButton();
    }

    private void _AddRefreshShortUrlButton() {
        Rect buttonRect = new Rect(Screen.width * 0.125f, Screen.height * 0.25f, Screen.width * 0.75f, Screen.height * 0.1f);
        if (GUI.Button(buttonRect, "Refresh Short URL")) {
            var feature = "invite";
            var channel = "facebook";
            var stage = "2";
            var requestParams = new Dictionary<string, object> {
                { "key1", "test_object" },
                { "key2", "here is another object!!" },
                { "$og_title", "Kindred" },
                { "$og_image_url", "https://s3-us-west-1.amazonaws.com/branchhost/mosaic_og.png" }
            };

            var tags = new List<string> { "tag1", "tag2" };

            Branch.getShortURLWithTags(requestParams, tags, channel, feature, stage, delegate(string url, string error) {
                if (error != null) {
                    Debug.Log("Failed to retreive short url: " + error);
                }
                else {
                    Debug.Log("Retrieved url: " + url);
                    _url = url;
                }
           });
        }
    }

    private void _UpdateUrlLabelIfPresent() {
        if (_url != null) {
            GUI.Label(new Rect(Screen.width * 0.125f, Screen.height * 0.125f, Screen.width * 0.75f, Screen.height * 0.1f), _url, _labelStyle);
        }
    }

    private string _url;
    private GUIStyle _labelStyle;
}
