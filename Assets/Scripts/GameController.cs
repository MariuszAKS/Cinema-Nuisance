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

    void Update()
    {
        
    }



    private void SpawnNpcs()
    {
        List<Vector3> seatTriggerPositions = GetListOfSeatTriggerPositions();
        CorrectNpcAmounts(seatTriggerPositions.Count);

        int aggressiveNpcsLeft = aggressiveNpcAmount;

        for (int i = 0; i < npcAmount; i++) {
            int seatTriggerPositionIndex = Random.Range(0, seatTriggerPositions.Count);

            GameObject npc = Instantiate(NpcPrefab, seatTriggerPositions[seatTriggerPositionIndex], Quaternion.identity);

            if (aggressiveNpcsLeft > 0) {
                npc.GetComponent<NpcController>().SetAggressive(true);
                npc.GetComponent<NpcController>().SetSpoilersAmount((ushort)Random.Range(1, 4));
                aggressiveNpcsLeft--;
            }
            
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
