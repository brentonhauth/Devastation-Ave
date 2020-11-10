using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler01 : MonoBehaviour {

    public Transform EscapePoint;

    private readonly string[] Dialog = {
        "You've been asleep for several hours",
        "You want to find your friends...",
        "But you should escape instead..."
    };
    private int DialogCount = 0;
    private bool TriggeredFinish = false;
    private Transform Player;

    void Start() {
        print("STARTED"); 
        Invoke("CallNext", .5f);
        Utils.FindPlayer(ref Player);
    }

    void Update() {
        if (Player && VectorHelper.WithinRange(Player.position, EscapePoint.position, 3f)) {
            if (!TriggeredFinish) {
                TriggeredFinish = true;
                Finish();
            }
        }
    }

    private void CallNext() {
        if (DialogCount == Dialog.Length) {
            HudHandler.Self.PromptText.text = string.Empty;
        } else {
            HudHandler.Self.PromptText.text = Dialog[DialogCount];
        }

        DialogCount++;
        if (DialogCount <= Dialog.Length) {
            Invoke("CallNext", 3f);
        }
    }

    private void Finish() {
        const string FinishText = "You've escaped the school";
        HudHandler.Self.PromptText.text = FinishText;
        Invoke("TriggerClose", 2f);
    }

    private void TriggerClose() {
        SceneManager.LoadScene("Main Menu");
    }
}
