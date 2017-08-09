using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum spawn_type {horizontal, vertical_left, vertical_right}



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

    private GameObject[] areaSpawner;

    public List<Wave> m_Waves = new List<Wave>();

    public int current_wave;

    public float yAdjuster;

    public bool isWavesFinish;

    public Endless EndlessModeVariables;

    public Wave EndlessWave;


    //used as defaults to calculate ships spawn pos and orientation
    float pos_x = 0;
    float pos_y = 0;

    Vector3 shipRotation = Vector3.zero;



    void Start ()
    {
        if (gameMode == _gameMode.Normal)
        {
            isWavesFinish = true;
            StartCoroutine(NormalMode());
        }

        if(gameMode == _gameMode.Endless)
        {
            CreateNumbers();
            StartCoroutine(EndlessMode());
        }

        areaSpawner = GameObject.FindGameObjectsWithTag("SpawnZone");

    }

    IEnumerator EndlessMode()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(EndlessWave.time_nextWave);


            //calcola posizione di spawn in base a enum
            if (EndlessWave.sp_type == spawn_type.horizontal)
            {
                for (int h = 0; h < areaSpawner.Length; h++)
                {
                    if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Horizontal")
                    {
                        //posizione spawn astronave
                        float spawnerXPos = areaSpawner[h].transform.position.x;
                        float spawnerHalfWidth = areaSpawner[h].transform.localScale.x / 2;
                        pos_x = Random.Range(spawnerXPos - spawnerHalfWidth, spawnerXPos + spawnerHalfWidth);
                        pos_y = areaSpawner[h].transform.position.y;

                        //rotazione che astronave deve avere
                        shipRotation = new Vector3(0, 0, 180);

                        break;
                    }
                }

            }
            else if (EndlessWave.sp_type == spawn_type.vertical_left)
            {
                for (int h = 0; h < areaSpawner.Length; h++)
                {
                    if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Vertical_Left")
                    {
                        //posizione spawn astronave
                        float spawnerYPos = areaSpawner[h].transform.position.y;
                        float spawnerHalfWidth = areaSpawner[h].transform.localScale.y / 2;

                        pos_y = Random.Range(spawnerYPos - spawnerHalfWidth, spawnerYPos + spawnerHalfWidth);

                        pos_x = areaSpawner[h].transform.position.x;

                        //rotazione che astronave deve avere
                        shipRotation = new Vector3(0, 0, 270);

                        break;
                    }
                }
            }
            else if (EndlessWave.sp_type == spawn_type.vertical_right)
            {
                for (int h = 0; h < areaSpawner.Length; h++)
                {
                    if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Vertical_Right")
                    {
                        //posizione spawn astronave
                        float spawnerYPos = areaSpawner[h].transform.position.y;
                        float spawnerHalfWidth = areaSpawner[h].transform.localScale.y / 2;

                        pos_y = Random.Range(spawnerYPos - spawnerHalfWidth, spawnerYPos + spawnerHalfWidth);

                        pos_x = areaSpawner[h].transform.position.x;

                        //rotazione che astronave deve avere
                        shipRotation = new Vector3(0, 0, 90);

                        break;
                    }
                }
            }



            for (int i = 0; i < EndlessWave.enemy_number; i++)
            {
                //spawna la nave
                GameObject obj = Instantiate(EndlessWave.obj);

                //spostala nella relativa posizione e ruotala nella giusta direzione
                obj.transform.position = new Vector2(pos_x, pos_y);
                obj.transform.rotation = Quaternion.Euler(shipRotation);

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
        int rngNumbSpawnType = Random.Range(0, 2);

        if(rngNumbSpawnType == 0)
        {
            EndlessWave.sp_type = spawn_type.horizontal;
        }
        else if (rngNumbSpawnType == 1)
        {
            EndlessWave.sp_type = spawn_type.vertical_left;
        }
        else if (rngNumbSpawnType == 2)
        {
            EndlessWave.sp_type = spawn_type.vertical_right;
        }

        EndlessWave.time_nextWave = Random.Range(EndlessModeVariables.time_nextWave_min, EndlessModeVariables.time_nextWave_max);

        EndlessWave.spawn_of_enemy_time = Random.Range(EndlessModeVariables.spawn_of_enemy_time_min, EndlessModeVariables.spawn_of_enemy_time_max);
    }
    

    IEnumerator NormalMode()
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


                //calcola posizione di spawn in base a enum
                if (m_Waves[current_wave].sp_type == spawn_type.horizontal)
                {
                    for (int h = 0; h < areaSpawner.Length; h++)
                    {
                        if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Horizontal")
                        {
                            //posizione spawn astronave
                            float spawnerXPos = areaSpawner[h].transform.position.x;
                            float spawnerHalfWidth = areaSpawner[h].transform.localScale.x / 2;
                            pos_x = Random.Range(spawnerXPos - spawnerHalfWidth, spawnerXPos + spawnerHalfWidth);
                            pos_y = areaSpawner[h].transform.position.y;

                            //rotazione che astronave deve avere
                            shipRotation = new Vector3(0, 0, 180);

                            break;
                        }
                    }

                }
                else if (m_Waves[current_wave].sp_type == spawn_type.vertical_left)
                {
                    for (int h = 0; h < areaSpawner.Length; h++)
                    {
                        if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Vertical_Left")
                        {
                            //posizione spawn astronave
                            float spawnerYPos = areaSpawner[h].transform.position.y;
                            float spawnerHalfWidth = areaSpawner[h].transform.localScale.y / 2;

                            pos_y = Random.Range(spawnerYPos - spawnerHalfWidth, spawnerYPos + spawnerHalfWidth);

                            pos_x = areaSpawner[h].transform.position.x;

                            //rotazione che astronave deve avere
                            shipRotation = new Vector3(0, 0, 270);

                            break;
                        }
                    }
                }
                else if (m_Waves[current_wave].sp_type == spawn_type.vertical_right)
                {
                    for (int h = 0; h < areaSpawner.Length; h++)
                    {
                        if (areaSpawner[h].gameObject.name == "EnemySpawner_Trigger_Vertical_Right")
                        {
                            //posizione spawn astronave
                            float spawnerYPos = areaSpawner[h].transform.position.y;
                            float spawnerHalfWidth = areaSpawner[h].transform.localScale.y / 2;

                            pos_y = Random.Range(spawnerYPos - spawnerHalfWidth, spawnerYPos + spawnerHalfWidth);

                            pos_x = areaSpawner[h].transform.position.x;

                            //rotazione che astronave deve avere
                            shipRotation = new Vector3(0, 0, 90);

                            break;
                        }
                    }
                }

                for (int i = 0; i < m_Waves[current_wave].enemy_number; i++)
                {
                    //spawna nave
                    GameObject obj = Instantiate(m_Waves[current_wave].obj);
                    
                    //spostala nella relativa posizione e ruotala nella giusta direzione
                    obj.transform.position = new Vector2(pos_x, pos_y);
                    obj.transform.rotation = Quaternion.Euler(shipRotation);

                    //timer
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
