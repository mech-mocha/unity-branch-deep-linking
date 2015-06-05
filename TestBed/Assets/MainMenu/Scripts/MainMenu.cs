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
        _labelStyle.fontSize = 32;
        
        _buttonStyle = new GUIStyle();
        _buttonStyle.normal.textColor = Color.white;
        _buttonStyle.fontSize = 36;
    }

    public void OnGUI() {
        GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

        _UpdateUrlLabelIfPresent();
        _AddRefreshShortUrlButton();
        _UpdateRewardCreditLabel();
        _UpdateInstallTotalLabel();
        _UpdateInstallUniquesLabel();
        _UpdateBuyTotalLabel();
        _UpdateBuyUniquesLabel();
//        _AddRefreshCountsButton();
//        _AddSetIdentityButton();
//        _AddRefreshRewardsButton();
//        _AddLogoutButton();
//        _AddRedeem5Button();
//        _AddPrintInstallParamsButton();
//        _AddExecuteBuyButton();
//        _AddBuyWithStateButton();
//        _AddCreditHistoryButton();
//        _AddReferralCodeButton();
//        _AddShareSheetButton();
    }
    
    private void _UpdateUrlLabelIfPresent() {
        if (_url != null) {
            GUI.Label(new Rect(Screen.width * 0.125f, Screen.height * 0.1f, Screen.width * 0.75f, Screen.height * 0.1f), _url, _labelStyle);
        }
    }

    private void _AddRefreshShortUrlButton() {
        Rect buttonRect = new Rect(Screen.width * 0.125f, Screen.height * 0.15f, Screen.width * 0.75f, Screen.height * 0.1f);
//        if (GUI.Button(buttonRect, "Refresh Short URL", _buttonStyle)) { // TODO figure out how to style button
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
    
    private void _UpdateRewardCreditLabel() {
        string displayString = "reward credits = ";
        if (_rewardCredits != -1) {
            displayString += _rewardCredits;
        }
        
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.25f, Screen.height * 0.1f), displayString, _labelStyle);
    }
    
    private void _UpdateInstallTotalLabel() {
        string displayString = "install total = ";
        if (_installTotal != -1) {
            displayString += _installTotal;
        }
        
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.35f, Screen.width * 0.25f, Screen.height * 0.1f), displayString, _labelStyle);
    }
    
    private void _UpdateInstallUniquesLabel() {
        string displayString = "uniques = ";
        if (_installUniques != -1) {
            displayString += _installUniques;
        }
        
        GUI.Label(new Rect(Screen.width * 0.55f, Screen.height * 0.35f, Screen.width * 0.25f, Screen.height * 0.1f), displayString, _labelStyle);
    }
    
    private void _UpdateBuyTotalLabel() {
        string displayString = "buy total = ";
        if (_buyTotal != -1) {
            displayString += _buyTotal;
        }
        
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.4f, Screen.width * 0.25f, Screen.height * 0.1f), displayString, _labelStyle);
    }
    
    private void _UpdateBuyUniquesLabel() {
        string displayString = "uniques = ";
        if (_buyUniques != -1) {
            displayString += _buyUniques;
        }
        
        GUI.Label(new Rect(Screen.width * 0.55f, Screen.height * 0.4f, Screen.width * 0.25f, Screen.height * 0.1f), displayString, _labelStyle);
    }

    private string _url;
    private int _rewardCredits = -1;
    private int _installTotal = -1;
    private int _installUniques = -1;
    private int _buyTotal = -1;
    private int _buyUniques = -1;
    private GUIStyle _labelStyle;
    private GUIStyle _buttonStyle;
}
