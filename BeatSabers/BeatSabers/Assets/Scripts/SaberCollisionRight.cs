﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollisionRight : MonoBehaviour
{
    SteamVR_Controller.Device device { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;
    GameObject tracked;
    public float distance = 0.0f;
    public int score = 0;
    private int controllerIndex;
    public static GameObject DifficultyManager;
    private bool inReactionArea, collided;
    // Use this for initialization
    void Start()
    {
        tracked = GameObject.Find("Controller (right)");
        trackedObj = tracked.GetComponent<SteamVR_TrackedObject>();
        controllerIndex = tracked.GetComponent<SteamVR_TrackedObject>().GetDeviceIndex();
        DifficultyManager = GameObject.Find("Spawner");
        inReactionArea = false;
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inReactionArea && collided)
        {
            if (device.velocity.sqrMagnitude > 1)
            {
                //determine distance and send to DifficultyManager
                string hitbox = "Reaction";
                GameObject hitBox = GameObject.FindGameObjectWithTag(hitbox);
                distance = transform.position.z - hitBox.transform.position.z;
                DifficultyManager.GetComponent<DifficultyManager>().IncrementNotesHit(distance);

                //provide feedback
                SteamVR_Controller.Input(controllerIndex).TriggerHapticPulse(3999);
                FindObjectOfType<AudioManager>().Play("OrbHit");

                //Destroy orb
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRight")
        {
            collided = true;
        }

        if (other.tag == "Respawn")
        {
            GetComponent<ParticleSystem>().startColor = new Color(255, 255, 255, 255);
        }

        if (other.tag == "Reaction")
        {
            GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 255);
            inReactionArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Reaction")
        {
            DifficultyManager.GetComponent<DifficultyManager>().BreakStreak();
            inReactionArea = false;
        }
        else if (other.tag == "PlayerRight")
        {
            collided = false;
        }
    }
}
