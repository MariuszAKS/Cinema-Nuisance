using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject SeatSet;
    [SerializeField] private GameObject NpcPrefab;

    [SerializeField] private int npcAmount;
    [SerializeField] private int aggressiveNpcAmount;



    void Awake()
    {
        SpawnNpcs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void SpawnNpcs()
    {
        List<Vector3> seatTriggerPositions = GetListOfSeatTriggerPositions();
        CorrectNpcAmounts(seatTriggerPositions.Count);

        for (int i = 0; i < npcAmount; i++) {
            int seatTriggerPositionIndex = Random.Range(0, seatTriggerPositions.Count);

            Instantiate(NpcPrefab, seatTriggerPositions[seatTriggerPositionIndex], Quaternion.identity);
            seatTriggerPositions.RemoveAt(seatTriggerPositionIndex);
        }
    }

    private List<Vector3> GetListOfSeatTriggerPositions()
    {
        List<Vector3> seatTriggerPositions = new List<Vector3>();
        GameObject[] seatTriggers = GameObject.FindGameObjectsWithTag("Seat Trigger");

        foreach (GameObject seatTrigger in seatTriggers)
            seatTriggerPositions.Add(seatTrigger.transform.position);
        
        return seatTriggerPositions;
    }

    private void CorrectNpcAmounts(int seatTriggerPositionsCount)
    {
        npcAmount = Mathf.Min(npcAmount, seatTriggerPositionsCount);
        aggressiveNpcAmount = Mathf.Min(aggressiveNpcAmount, npcAmount);
    }
}
