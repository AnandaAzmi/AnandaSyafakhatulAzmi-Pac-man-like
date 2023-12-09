using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
        enemy.Animator.SetTrigger("PatrolState");
    }
    public void UpdateState(Enemy enemy)
    {
        if(Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        if (!_isMoving)
        {
            _isMoving = true;
            //random index waypoint dari 0 sampai 6
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
            _destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            if(Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
            
        }

    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
