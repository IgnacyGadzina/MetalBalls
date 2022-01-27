using UnityEngine;
/// <summary>
/// This script is responsible for the artiffical intelligence of the enemys. 
/// An enemy can decide if he wants to attack a player or to charge from a battery.
/// </summary>
public class AI : MonoBehaviour
{
    public GameObject player;
    public GameObject closestBattery;
    GameObject[] batteries;
    Action[] actions;
    Biology biology;

    private void Start()
    {
        biology = GetComponent<Biology>();
        player = GameObject.FindGameObjectWithTag("Player");
        batteries = GameObject.FindGameObjectsWithTag("Battery");
        actions = new Action[] { new GoingToPlayer(), new Waiting(), new GoingToBattery() };
    }

    private void Update()
    {
        //Finding the closest battery
        closestBattery = batteries[0];
        for (int i = 0; i < batteries.Length; i++)
        {
            if (Distance(closestBattery.transform.position) > Distance(batteries[i].transform.position))
            {
                closestBattery = batteries[i];
            }

        }

        actions[0].PotentialPleasure =  biology.energy / DistanceFromPlayer;
        actions[1].PotentialPleasure = 0;
        actions[2].PotentialPleasure = 1 / DistanceFromBattery * biology.energy;

        TheMostSatisfingAction.Execute();
        if (actions[0].isActing)
        {
            actions[0].isActing = false;
            biology.GoTo(player.transform.position);
        }
        else if (actions[2].isActing)
        {
            actions[2].isActing = false;
            biology.GoTo(closestBattery.transform.position);
        }
    }

    float Distance(Vector3 position)
    {
        if (position == null)
        {
            return 10000000;
        }
        return Vector3.Distance(transform.position, position);
    }

    public abstract class Action : AI
    {
        public bool isActing;
        public abstract void Execute();
        float potentialPleasure;
        public float PotentialPleasure
        {
            get => potentialPleasure;
            set => potentialPleasure = value;
        }
    }
    private class GoingToPlayer : Action
    {
        public override void Execute()
        {
            isActing = true;
        }
    }
    float DistanceFromPlayer
    {
        get
        {
            if (player == null)
            {
                return 10000000000000;
            }
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
    class GoingToBattery : Action
    {
        public override void Execute()
        {
            isActing = true;
        }
    }
    float DistanceFromBattery
    {
        get
        {
            if (closestBattery == null)
            {
                return 10000000000;
            }

            return Vector3.Distance(closestBattery.transform.position, transform.position);
        }
    }

    class Waiting : Action
    {
        public override void Execute()
        {
            //Just do nothing
        }
    }

    /// <summary>
    /// Get the action with the biggest potential pleasure.
    /// </summary>
    private Action TheMostSatisfingAction
    {
        get
        {
            int indexOfTheBestAction = 0;
            for (int i = 1; i < actions.Length; i++)
            {
                //print(actions[i].GetPotentialPleasure());
                if (actions[i].PotentialPleasure > actions[indexOfTheBestAction].PotentialPleasure)
                {
                    indexOfTheBestAction = i;
                }
            }
            return actions[indexOfTheBestAction];
        }
    }
}