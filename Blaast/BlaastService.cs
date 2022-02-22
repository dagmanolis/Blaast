namespace Blaast
{
    public class BlaastService
    {
        public event Action? HasChanged;
        internal void HasChangedInvoker()
        {
            HasChanged?.Invoke();
        }
    }
}