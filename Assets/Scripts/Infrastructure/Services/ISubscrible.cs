namespace Infrastructure.Services
{
    public interface ISubscrible
    {
        public void Subscribe(object subscriber);

        public void Unsubscribe();
    }
}