using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum spawn_type {horizontal, vertical }



[System.Serializable]
public class Wave
{
    public GameObject obj;

    public float enemy_number;

    public spawn_type sp_type;

    public float time_nextWave;

    public float spawn_of_enemy_time;
}

[System.Serializable]
public class Endless
{
    public GameObject[] enemies;

    public int enemy_number_min;

    public int enemy_number_max;

    public spawn_type sp_type;

    public float time_nextWave_min;

    public float time_nextWave_max;

    public float spawn_of_enemy_time_min;

    public float spawn_of_enemy_time_max;
}



public class EnemyWaveSpawner : MonoBehaviour
{
    public enum _gameMode { Endless, Normal }
    public _gameMode gameMode;

    public GameObject enemy;

    public List<Wave> m_Waves = new List<Wave>();

    public int current_wave;

    public float yAdjuster;

    public bool isWavesFinish;

    public Endless EndlessModeVariables;

    public Wave EndlessWave;

    void Start ()
    {
        if (gameMode == _gameMode.Normal)
        {
            isWavesFinish = true;
            StartCoroutine(SpawnWave());
        }

        if(gameMode == _gameMode.Endless)
        {
            CreateNumbers();
            StartCoroutine(EndlessMode());
        }

    }
	
    IEnumerator EndlessMode()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(EndlessWave.time_nextWave);

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + Vector3.up * yAdjuster;


            float pos_x = Random.Range(min.x, max.x);
            float pos_y = max.y;



            for (int i = 0; i < EndlessWave.enemy_number; i++)
            {
                GameObject obj = Instantiate(EndlessWave.obj);
                obj.transform.position = new Vector2(pos_x, pos_y);
                yield return new WaitForSeconds(EndlessWave.spawn_of_enemy_time);
            }


            current_wave++;

            CreateNumbers();


        }


    }
    

    void CreateNumbers()
    {
        int rngNumb = Random.Range(0, EndlessModeVariables.enemies.Length);
        EndlessWave.obj = EndlessModeVariables.enemies[rngNumb];

        EndlessWave.enemy_number = Random.Range(EndlessModeVariables.enemy_number_min, EndlessModeVariables.enemy_number_max);

        //sp_type NOT IMPLEMENTED YET

        EndlessWave.time_nextWave = Random.Range(EndlessModeVariables.time_nextWave_min, EndlessModeVariables.time_nextWave_max);

        EndlessWave.spawn_of_enemy_time = Random.Range(EndlessModeVariables.spawn_of_enemy_time_min, EndlessModeVariables.spawn_of_enemy_time_max);
    }

    //dichiarazione variabili minime e massime per ogni caratteristica di gioco Endless
    


    IEnumerator SpawnWave()
    {

        while (isWavesFinish)
        {
            if (current_wave > m_Waves.Capacity - 1)
            {
                isWavesFinish = false;

            }

            if (isWavesFinish)
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
                    yield return new WaitForSeconds(m_Waves[current_wave].spawn_of_enemy_time);
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
}
