using System.Collections.Generic;

namespace FruitStealer
{
    public interface IGenerator<T> where T : class
    {
        Queue<T> Initialize();

        T GenerateNext();
    }
}