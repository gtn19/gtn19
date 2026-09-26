using UnityEngine;
public class BallShape : SpellShape
{
    public float radius;
    public GameObject ballPrefab;

    private GameObject ball;
    public override void Execute(GameObject caster, GameObject target, List<SpellEffect> effects)
    {
        ball = Instantiate(ballPrefab);
    }

    private void Update()
    {

    }
}
