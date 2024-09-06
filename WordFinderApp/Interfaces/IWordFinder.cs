namespace WordFinderApp.Interfaces
{
    public interface IWordFinder
    {
        public IEnumerable<string> Find(IEnumerable<string> wordstream, string method);
    }
}
