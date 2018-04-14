﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AudioHelm {
    public class Spawner : MonoBehaviour
    {
        [AddComponentMenu("")]
        public GameObject[] enemies;
        public Vector3 spawnValues;
        public float spawnWait;
        public float spawnMostWait;
        public float spawnLeastWait;
        public int startWait;
        public bool stop;

        int randEnemy;

        void Start()
        {

            //StartCoroutine(waitSpawner());
        }


        void Update()
        {

            spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        }

        //   IEnumerator waitSpawner(){

        //	yield return new WaitForSeconds(startWait);

        //	while (!stop){

        //		randEnemy = Random.Range (0, 2);
        //		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 1, Random.Range (-spawnValues.z, spawnValues.z));

        //		Instantiate (enemies[randEnemy], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

        //		yield return new WaitForSeconds (spawnWait);
        //	}
        //}

        public int startingNote = 20;
        public int[] scale = { 0, 2, 4, 5, 7, 9, 11 };

        int GetNoteIndex(int note)
        {
            int noteAdjusted = (note - startingNote + Utils.kMidiSize) % Utils.kMidiSize;
            int octave = noteAdjusted / Utils.kNotesPerOctave;
            int noteInOctave = noteAdjusted - Utils.kNotesPerOctave * octave;

            for (int scaleNote = 0; scaleNote < scale.Length; ++scaleNote)
            {
                if (scale[scaleNote] >= noteInOctave)
                    return octave * scale.Length + scaleNote;
            }
            return octave;
        }

        public void spawnNote(Note note)
        {
            randEnemy = Random.Range(0, 2);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, 0);

            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
        }
    }
}

