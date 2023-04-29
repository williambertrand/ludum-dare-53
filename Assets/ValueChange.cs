public class ValueChange
{
    public float value;
    public float maxValue;

    public ValueChange(float currentValue, float maxValue)
    {
        this.value = currentValue;
        this.maxValue = maxValue;
    }

    public float GetPercentage()
    {
        return value / maxValue;
    }
}
