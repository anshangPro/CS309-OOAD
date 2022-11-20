namespace Units.AI.Evaluator
{
    public abstract class AbstractEvaluator
    {
        public abstract float Evaluate(Unit self, Unit target);
    }
}