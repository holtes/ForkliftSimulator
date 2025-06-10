namespace _Project.Develop.Runtime.Domain.Models
{
    public class PalleteModel
    {
        private bool IsGrabbed;
        private bool IsUnloaded;

        public void SetGrabState(bool state)
        {
            IsGrabbed = state;
        }

        public void SetUnloadState(bool state)
        {
            IsUnloaded = state;
        }

        public bool IsAvailableToDisappear()
        {
            return !IsGrabbed && IsUnloaded;
        }
    }
}