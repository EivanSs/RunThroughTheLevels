using System;

public class FloatVector
{
    public float x;
    public float y;

    public FloatVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
        
    public double Distance(FloatVector pos)
    {
        return Math.Sqrt(Math.Pow(x - pos.x, 2) + Math.Pow(y - pos.y, 2));
    }

    public double Angle(FloatVector pos)
    {
        return Math.Atan2(y - pos.y, x - pos.x);
    }

    public IntVector ToInt()
    {
        return new IntVector((int)x, (int)y);
    }
    
    #region Operators

    public static FloatVector operator + (FloatVector a, FloatVector b)
    {
        return new FloatVector(a.x + b.x, a.y + b.y);
    }

    public static FloatVector operator - (FloatVector a, FloatVector b)
    {
        return new FloatVector(a.x - b.x, a.y - b.y);
    }

    public static FloatVector operator / (FloatVector a, float v)
    {
        return new FloatVector(a.x / v, a.y / v);
    }

    public static FloatVector operator * (FloatVector a, float v)
    {
        return new FloatVector(a.x * v, a.y * v);
    }

    #endregion
}
