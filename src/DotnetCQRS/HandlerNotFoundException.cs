using System;

namespace DotnetCQRS
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(Type expectedTarget) : base($"Could not find handler for {expectedTarget.Name}")
        {
        }
    }
}