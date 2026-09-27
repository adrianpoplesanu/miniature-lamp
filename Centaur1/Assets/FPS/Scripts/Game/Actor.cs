using UnityEngine;

public class Actor : MonoBehaviour
{
    [Tooltip("Represents the affiliation (or team) of the actor. Actors of the same affiliation are friendly to each other")]
    public int Affiliation;

    [Tooltip("Represents point where other actors will aim when they attack this actor")]
    public Transform AimPoint;

    ActorsManager m_ActorsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_ActorsManager = GameObject.FindAnyObjectByType<ActorsManager>();

        if (!m_ActorsManager.Actors.Contains(this))
        {
            m_ActorsManager.Actors.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
