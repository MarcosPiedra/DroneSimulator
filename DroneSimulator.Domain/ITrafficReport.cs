using System;

namespace DroneSimulator.Domain
{
    public interface ITrafficReport
    {
        void Report(int id, DateTime dateTime, float speed, TrafficCondition condition);
    }
}