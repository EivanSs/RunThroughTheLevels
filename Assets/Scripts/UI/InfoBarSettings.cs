using System.Collections.Generic;

namespace UI
{
    public struct InfoBarSettings
    {
        public bool withParameters;
        
        public List<InfoBarParameter> parameters;
    }

    public struct InfoBarParameter
    {
        public InfoBarParameterType parameterType;
        public float normalizedValue;
        public string textValue;
    }

    public enum InfoBarParameterType
    {
        Damage,
        Accuracy,
        RateOfFire
    }
}
