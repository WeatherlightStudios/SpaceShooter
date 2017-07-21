using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum spown_type {horizontal, vertialc }


[System.Serializable]
public class Wave
{
    public GameObject obj;

    public float enemy_number;

    spown_type sp_type;

    public float time_nextWave;

    public float spown_of_enemy_time;
}



public class EnemyWaveSpawner : MonoBehaviour
{


    public GameObject enemy;

    public List<Wave> m_Waves = new List<Wave>();

    public int current_wave;

    public float yAdjuster;

    public bool isWavesFinish;

    void Start ()
    {
        isWavesFinish = true;
        StartCoroutine(SpawnWave());

    }
	
	

	void Update ()
    {
        if (current_wave > m_Waves.Capacity - 1)
        {
            isWavesFinish = false;

        }
    }




    IEnumerator SpawnWave()
    {

        while (isWavesFinish)
        {
            yield return new WaitForSeconds(m_Waves[current_wave].time_nextWave);

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + Vector3.up * yAdjuster;


            float pos_x = Random.Range(min.x, max.x);
            float pos_y = max.y;



            for (int i = 0; i < m_Waves[current_wave].enemy_number; i++)
            {
                GameObject obj = Instantiate(m_Waves[current_wave].obj);
                obj.transform.position = new Vector2(pos_x, pos_y);
                yield return new WaitForSeconds(m_Waves[current_wave].spown_of_enemy_time);
            }

            if (current_wave > m_Waves.Capacity - 1)
            {
                isWavesFinish = false;

            }
            else
            {

             current_wave++;
            }



        }

    }
}
