using _Project.Develop.Runtime.Data.Configs;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class ForkModel
    {
        public readonly float ForkLiftSpeed;

        public ForkModel(ForkliftConfig forkliftConfig)
        {
            ForkLiftSpeed = forkliftConfig.ForkLiftSpeed;
        }

        public float CalculateLiftSpeed(float forkInput)
        {
            return forkInput * ForkLiftSpeed;
        }
    }
}