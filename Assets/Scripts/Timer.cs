// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class Timer : MonoBehaviour
// {
//     // Start is called before the first frame update
//     private int initialTime = 120;
//     // Update is called once per frame
//     [SerializeField] TextMeshProUGUI timerText;
//     float ellapsedTime;
//     void Update()
//     {
//         ellapsedTime += Time.deltaTime;
//         int remainingTime = initialTime - (int)ellapsedTime;
//         int minutes = remainingTime / 60;
//         int seconds = remainingTime % 60;
//         if (remainingTime <= 0)
//         {
//             timerText.text = "00:00";
//             return;
//         }
//         timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
//     }
// }
