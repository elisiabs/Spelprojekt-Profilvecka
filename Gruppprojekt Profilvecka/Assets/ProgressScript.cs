using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform player;
    private float levelDistance;

    // Start is called before the first frame update
    void Start()
    {
        levelDistance = CalculateDistance("start");
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = (CalculateDistance("player"));

        slider.value = playerDistance / levelDistance;
    }

    private float CalculateDistance(string whichDistance)
    {
        if(whichDistance == "start")
        {
            return Vector2.Distance(start.position, end.position);
        }
        else if(whichDistance == "player")
        {
            return Vector2.Distance(end.position, player.position);
        }
        else
        {
            Debug.LogWarning("Distance not chosen for distance calculation.");
            return 0;
        }
    }
}
