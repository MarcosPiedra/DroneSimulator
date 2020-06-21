namespace DroneSimulator.CSVDataAccess
{
    public interface ILineParser<T> where T : class
    {
        T Parse(string line);
    }
}