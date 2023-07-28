using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject SeatSet;
    [SerializeField] private GameObject NpcPrefab;

    [SerializeField] private ushort npcAmount;
    [SerializeField] private ushort aggressiveNpcAmount;

    [HideInInspector] public ushort NpcAmount { get { return npcAmount; } }
    [HideInInspector] public ushort AggressiveNpcAmount { get { return aggressiveNpcAmount; } }

    private ushort points;



    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else Destroy(gameObject);

        SpawnNpcs();

        Time.timeScale = 1;
    }

    void Update()
    {
        
    }



    private void SpawnNpcs()
    {
        List<Vector3> seatTriggerPositions = GetListOfSeatTriggerPositions();
        CorrectNpcAmounts((ushort)seatTriggerPositions.Count);

        ushort aggressiveNpcsLeft = aggressiveNpcAmount;

        for (ushort i = 0; i < npcAmount; i++) {
            ushort seatTriggerPositionIndex = (ushort)Random.Range(0, seatTriggerPositions.Count);

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

    private void CorrectNpcAmounts(ushort seatTriggerPositionsCount)
    {
        npcAmount = (ushort)Mathf.Min(npcAmount, seatTriggerPositionsCount);
        aggressiveNpcAmount = (ushort)Mathf.Min(aggressiveNpcAmount, npcAmount);
    }

    

    public void NpcExitsCinema(GameObject npc)
    {
        points++;
        UIController.instance.UpdatePoints(points, npcAmount);

        Destroy(npc);

        if (points == npcAmount) {
            Time.timeScale = 0;
            UIController.instance.SetActive_WinnerScreen(true);
        }
    }

    public void NpcCatchesPlayer()
    {
        Time.timeScale = 0;
        UIController.instance.SetActive_GameOverScreen(true);
    }
}
